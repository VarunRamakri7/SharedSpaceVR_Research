using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EvalGrid : MonoBehaviour
{
    public Grid grid;
    public GameObject colliderParent;

    private Vector3[] pointsOfContact = new Vector3[10];

    void checkGridCell()
    {
        //grid.c
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("obstacle"))
        {
            pointsOfContact[pointsOfContact.Length] = collision.GetContact(0).point;

            Debug.Log("Contact found");
        }
    }
}