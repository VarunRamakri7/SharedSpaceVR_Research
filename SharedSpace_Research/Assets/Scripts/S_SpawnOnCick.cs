using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum DIRECTION { UP, DOWN, LEFT, RIGHT };

// Class to keep track of the properties of a path object
public class Path
{
    private GameObject cube;
    private bool[] isFilled = new bool[4]; // Whether Up, Down, Left, Right are filled

    public Path()
    {
        cube = new GameObject();
        isFilled[0] = isFilled[1] = isFilled[2] = isFilled[3] = false;
    }

    public Path(GameObject _cube)
    {
        cube = _cube;
        isFilled[0] = isFilled[1] = isFilled[2] = isFilled[3] = false;
    }

    public void SetCube(GameObject _cube)
    {
        cube = _cube;
    }

    public void SetFilled(int index, bool value)
    {
        isFilled[index] = value;
    }

    public GameObject GetCube()
    {
        return cube;
    }

    public bool GetFilled(int index)
    {
        return isFilled[index];
    }
}

public class S_SpawnOnCick : MonoBehaviour
{
    const int NUM_CUBES = 50;

    public new GameObject gameObject;
    private GameObject cube; // Random cube
    private GameObject lastCube; // LAst placed cube
    private int numberCube = 1;
    private Vector3 cubePosition;


    // Generate Button
    public Button genButton;
    private bool canGenerate = false;

    Ray ray; // Initialise the ray
    RaycastHit hit; // Initialise the hit

    void OnClick()
    {
        canGenerate = true;
    }

    void Start()
    {
        genButton.onClick.AddListener(OnClick);

        cubePosition = new Vector3(Random.Range(-21.0f, 21.0f), 2.5f, Random.Range(-21.0f, 21.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (canGenerate && numberCube < NUM_CUBES)
        {
            generateCube();
            System.Threading.Thread.Sleep(100);
            numberCube++;
        }
        //makePath();
    }

    // Generate a cube at a random position
    void generateCube()
    {
        // Make the cube
        cube = getNewCube();

        float randFloat = Random.Range(0.1f, 1.2f); // Random increment for the X-Coordinate

        // Change the position for the next cube
        if (cube.transform.position.x - randFloat > -21)
        {
            cubePosition = new Vector3(cube.transform.position.x - randFloat, 2.5f, Random.Range(-21.0f, 21.0f));
        }
        else if (cube.transform.position.x + randFloat < 21)
        {
            cubePosition = new Vector3(cube.transform.position.x + randFloat, 2.5f, Random.Range(-21.0f, 21.0f));
        }

        // Update last cube
        lastCube = cube;

        Debug.Log("Cube generated");
    }
    
    // Place object on mouse down
    private void OnMouseDown()
    {
        // Get the mouse-click location
        ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray will go from center of main camera to mouse

        // Get all info if the ray hits something
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Place a new object at the hit point
                Instantiate(gameObject, new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z), Quaternion.identity); // Instantiate prefab on point of hit
                gameObject.transform.localScale = new Vector3(2.0f, 1.0f, 2.0f);
                gameObject.tag = "obstacle";
                Debug.Log("Object placed at: " + hit.point);
            }
        }
    }

    // Make a new primitive cube
    private GameObject getNewCube()
    {
        // Make the cube
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.tag = "Path";

        cube.AddComponent<S_CheckPathCol>();

        // Add physics components
        cube.GetComponent<BoxCollider>().isTrigger = true;
        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Rigidbody>().useGravity = false;
        cube.GetComponent<Rigidbody>().isKinematic = true;

        cube.transform.position = cubePosition;
        cube.transform.localScale = new Vector3(3, 1, 2);

        return cube;
    }
}