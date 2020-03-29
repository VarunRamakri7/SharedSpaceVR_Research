using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EvalGrid : MonoBehaviour
{
    public bool canPlace = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision " + other.tag);
        if(other.CompareTag("obstacle") || other.CompareTag("Path"))
        {
            Debug.Log("Cannot Place");
            canPlace = false;
        }
        else
        {
            canPlace = true;
        }

        if(!canPlace && other.CompareTag("Path"))
        {
            S_GeneratePath path = other.GetComponent<S_GeneratePath>();
            path.triggerUpdated = true;

            // Change colliding object's location
            int randomLocation = Random.Range(0, 4); // Select Up(0)/Down(1)/Left(2)/Right(3)

            // Updating location
            Debug.Log("Updating location from collision...");
            other.transform.position = path.PlaceCube(randomLocation, other.transform);
        }
    }
}