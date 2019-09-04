using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SpawnOnCick : MonoBehaviour
{
    public GameObject Sphere;
    //public Transform Spawn;

    Ray ray; // Initialise the ray
    RaycastHit hit; // Initialise the hit

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray will go from center of main camera to mouse
        
        // Get all info if the ray hits something
        if(Physics.Raycast(ray, out hit))
        {
            if(Input.GetMouseButtonDown(0))
            {
                //hit.point.y = new Vector3(0, hit.point.y + 2, 0);
                Instantiate(Sphere, hit.point, Quaternion.identity); // Instantiate prefab on point of hit
                Debug.Log(hit.point);
            }
        }
    }
}
