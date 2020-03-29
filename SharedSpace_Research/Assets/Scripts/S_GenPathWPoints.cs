using UnityEngine;
using UnityEngine.AI;

public class S_GenPathWPoints : MonoBehaviour
{
    public NavMeshAgent agent;
    public NavMeshSurface surface;
    public Vector3[] travelPoints;

    private const int TOTAL_CUBES = 70;
    private int numCubes = 0;
    private Transform prevCube;

    // Start is called before the first frame update
    void Start()
    {
        // Update NavMesh
        surface.BuildNavMesh();

        // Move Agent while generating level
        GenerateLevel();

        // Destroy agent
    }

    // Generate path in level
    private void GenerateLevel()
    {
        // Move agent to four points from four points and generate level
        for (int i = 1; i < travelPoints.Length; i++)
        {
            NavMeshPath path = new NavMeshPath();

            // Calculate and store path
            agent.CalculatePath(travelPoints[i], path);

            // Iterate through path and place cubes
            for (int j = 4; j < (path.corners.Length - 4); j += 4)
            {
                Debug.Log("Distance: " + Vector3.Distance(path.corners[j - 4], path.corners[j]));
                GenCubeAt(path.corners[j]);
                numCubes++;
            }
        }
    }

    // Generate a single cube
    private GameObject GenCubeAt(Vector3 spawnLocation)
    {
        GameObject tempCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tempCube.transform.position = spawnLocation;
        tempCube.transform.localScale = new Vector3(2.0f, 1.0f, 2.0f);
        tempCube.tag = "Path";

        // Add collider to cube and make it a trigger
        tempCube.AddComponent<BoxCollider>();
        tempCube.GetComponent<BoxCollider>().isTrigger = true;

        return tempCube;
    }

    /**
     * 
     * Algorithm:
     *  1. Place objects
     *  2. Update NavMesh
     *  3. Move agent to four points
     *      a. Place path cube behind agent.
     * 
     */
}
