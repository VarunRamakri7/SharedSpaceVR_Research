using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EvalGrid : MonoBehaviour
{
    private bool canPlace = false;

    void CheckGridCell()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("obstacle"))
        {
            canPlace = false;
        }
        else
        {
            canPlace = true;
        }
    }
}