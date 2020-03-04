using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GeneratePath : MonoBehaviour
{
    public Collider[] colliders;
    public Transform startPos;

    private const int TOTAL_CUBES = 50;
    private int numCubes = 0;
    private GameObject firstCube;
    private Transform prevCube;

    private void Start()
    {   
        // Create the first cube to start the path
        firstCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        firstCube.transform.position = startPos.position;
        firstCube.transform.localScale = new Vector3(12.5f, 1.0f, 12.5f);

        // Save transformation of the last cube
        prevCube = firstCube.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (numCubes < TOTAL_CUBES)
        {
            GeneratePath();
            numCubes++;
        }
    }

    // Generate a path by starting from a random location
    private void GeneratePath()
    {
        int randomLocation = Random.Range(0, 4); // Select Up(0)/Down(1)/Left(2)/Right(3)
        GameObject tempCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tempCube.transform.position = prevCube.position;
        tempCube.transform.localScale = new Vector3(6.0f, 1.0f, 6.0f);
        tempCube.tag = "Path";

        // Add collider to cube and make it a trigger
        tempCube.AddComponent<BoxCollider>();
        tempCube.GetComponent<BoxCollider>().isTrigger = true;

        // Add script to check collision
        tempCube.AddComponent<S_EvalGrid>();

        // Get new position of cube
        Vector3 newPosition = PlaceCube(randomLocation, tempCube.transform);

        // Check if cube can be placed
        bool canPlace = CheckPlacement(newPosition) /*&& tempCube.GetComponent<S_EvalGrid>().canPlace)*/;
        if (canPlace)
        {
            Debug.Log("Placing new cube");

            // Assign new position since it is valid
            tempCube.transform.position = newPosition;
        }
        else
        {
            Debug.Log("Changing position");

            // Recheck position
            int tempLoc = randomLocation;
            if (randomLocation != tempLoc)
            {
                // Check placement of the new position
                canPlace = CheckPlacement(PlaceCube(randomLocation, tempCube.transform)) /*&& tempCube.GetComponent<S_EvalGrid>().canPlace)*/;
            }
            else
            {
                randomLocation = Random.Range(0, 4);
            }
        }

        // Update previous cube
        prevCube = tempCube.transform;
    }

    private Vector3 PlaceCube(int next, Transform prevCube)
    {
        if (next == 0)
        {
            // Place above the previous cube
            return new Vector3(prevCube.position.x - (prevCube.localScale.x / 1.0f), prevCube.position.y, prevCube.position.z);
        }
        else if (next == 1)
        {
            // Place below the previous cube
            return new Vector3(prevCube.position.x + (prevCube.localScale.x / 1.0f), prevCube.position.y, prevCube.position.z);
        }
        else if (next == 2)
        {
            // Place to the left of the previous cube
            return new Vector3(prevCube.position.x, prevCube.position.y, prevCube.position.z - (prevCube.localScale.z / 1.0f));
        }
        else
        {
            // Place to the right of the previous cube
            return new Vector3(prevCube.position.x, prevCube.position.y, prevCube.position.z + (prevCube.localScale.z / 1.0f));
        }
    }

    private bool CheckPlacement(Vector3 location)
    {
        bool isSuccessful = true;

        // Check if cube is within bounds of grid and if cube collides with obstacle
        if((location.x > 20.0f || location.x < -20.0f) || (location.z > 20.0f || location.z < -20.0f))
        {
            isSuccessful = false;
        }

        return isSuccessful;
    }

    /**
    * Algorithm to Generate Path
    * 1. While number of cubes is less than total cubes
    *      a. Create cube.
    *      b. Place cube randomly.  (At 00 for now)
    *      c. Randomly pick up/down, left/right.
    *      d. Check position.
    *           if it can be placed
    *               Place cube
    *           else
    *               Recheck Position
    *      e. Assign position to new cube
    */
}
