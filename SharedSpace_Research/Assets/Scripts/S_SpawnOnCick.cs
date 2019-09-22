using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SpawnOnCick : MonoBehaviour
{
    public new GameObject gameObject;
    private GameObject cube; // Random cube
    private int numberCube = 0;
    private Vector3 oldPosition;

    Ray ray; // Initialise the ray
    RaycastHit hit; // Initialise the hit

    void Start()
    {
        oldPosition = new Vector3(Random.Range(-21.0f, 21.0f), 2.5f, Random.Range(-21.0f, 21.0f));
    }

    // Update is called once per frame
    void Update()
    {
        // Generate 10 cubes
        if(numberCube < 10)
        {
            generateCube();
            numberCube++;
        }

        // Get the mouse-click location
        ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray will go from center of main camera to mouse
        
        // Get all info if the ray hits something
        if(Physics.Raycast(ray, out hit))
        {
            if(Input.GetMouseButtonDown(0))
            {
                // Place a new object at the hit point
                Instantiate(gameObject, new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z), Quaternion.identity); // Instantiate prefab on point of hit
                Debug.Log("Object placed at: " + hit.point);
            }
        }
    }

    // Generate a cube at a random position
    void generateCube()
    {
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.tag = "Path";
        cube.AddComponent<S_CheckGrid>();
        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Rigidbody>().isKinematic = false;
        cube.transform.position = oldPosition;
        cube.transform.localScale = new Vector3(2, 2, 2);

        float randFloat = Random.Range(1.6f, 2.5f); // Random increment for the X-Coordinate

        // Change the position for the next cube
        if (cube.transform.position.x - randFloat > -21)
        {
            oldPosition = new Vector3(cube.transform.position.x - randFloat, 2.5f, Random.Range(-21.0f, 21.0f));
        }
        else if (cube.transform.position.x + randFloat < 21)
        {
            oldPosition = new Vector3(cube.transform.position.x + randFloat, 2.5f, Random.Range(-21.0f, 21.0f));
        }

        Debug.Log("Cube generated");
    }
}
