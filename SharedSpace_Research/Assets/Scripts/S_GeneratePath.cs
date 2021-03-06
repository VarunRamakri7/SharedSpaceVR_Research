﻿using UnityEngine;
using UnityEngine.AI;

public class S_GeneratePath : MonoBehaviour
{
    public Collider[] colliders;
    public Transform startPos;
    public bool triggerUpdated = false;

    private const int TOTAL_CUBES = 70;
    private int numCubes = 0;
    private GameObject firstCube;
    private Transform prevCube;

    // NavMesh Components
    //public NavMeshSurface surface;

    private void Start()
    {   
        // Create the first cube to start the path
        firstCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        firstCube.transform.position = startPos.position;
        firstCube.transform.localScale = new Vector3(6.0f, 1.0f, 6.0f);

        // Save transformation of the last cube
        prevCube = firstCube.transform;

        GenerateLevel();

        // Update NavMesh
        //surface.BuildNavMesh();
    }

    private void GenerateLevel()
    {
        while (numCubes < TOTAL_CUBES)
        {
            GeneratePath();
            numCubes++;
        }
    }

    // Update is called once per frame
    /*void Update()
    {
        if (numCubes < TOTAL_CUBES)
        {
            GeneratePath();
            numCubes++;
        }
    }*/

    // Generate a path by starting from a random location
    private void GeneratePath()
    {
        // Run loop until the cube has been placed successfully
        int randomLocation = Random.Range(0, 4); // Select Up(0)/Down(1)/Left(2)/Right(3)
        GameObject tempCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tempCube.transform.position = prevCube.position;
        tempCube.transform.localScale = new Vector3(6.0f, 1.0f, 6.0f);
        tempCube.tag = "Untagged";

        // Add collider to cube and make it a trigger
        tempCube.AddComponent<BoxCollider>();
        tempCube.GetComponent<BoxCollider>().isTrigger = true;

        // Add script to check collision
        tempCube.AddComponent<S_EvalGrid>();

        // Get new position of cube
        Vector3 newPosition = PlaceCube(randomLocation, tempCube.transform);

        // Check if cube can be placed
        bool canPlace = CheckGridBounds(newPosition);
        if (canPlace)
        {
            Debug.Log("Placing new cube");

            // Assign new position since it is valid
            tempCube.transform.position = newPosition;
        }
        else
        {
            if (!triggerUpdated)
            {
                Debug.Log("Changing position");

                // Recheck position
                int tempLoc = randomLocation;

                while (!canPlace)
                {
                    //Debug.Log("Fixing.... Can place: " + canPlace);
                    if (randomLocation != tempLoc)
                    {
                        // Check placement of the new position
                        canPlace = CheckGridBounds(PlaceCube(randomLocation, tempCube.transform));
                    }
                    else
                    {
                        // Update direction of placement
                        randomLocation = Random.Range(0, 4);
                    }
                }
            }
        }

        // Update previous cube
        prevCube = tempCube.transform;
        tempCube.tag = "Path";

        if (tempCube.GetComponent<S_EvalGrid>().canPlace)
        {
            Debug.Log("Unsuccessful placement");
            Destroy(tempCube);
        }
    }

    public Vector3 PlaceCube(int next, Transform prevCube)
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

    private bool CheckGridBounds(Vector3 location)
    {
        bool isSuccessful = true;

        // Check if cube is within bounds of grid and if cube collides with obstacle
        if((location.x > 20.0f || location.x < -20.0f) || (location.z > 20.0f || location.z < -20.0f))
        {
            Debug.Log("Replacing...");
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
