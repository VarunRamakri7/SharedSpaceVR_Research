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
    }
}