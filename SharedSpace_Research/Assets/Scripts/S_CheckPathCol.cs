using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CheckPathCol : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if ((collision.gameObject.tag == "obstacle") || (collision.gameObject.tag == "Path"))
        {
            Debug.Log("Collision Detected: " + collision.gameObject.name);
            this.gameObject.transform.position = new Vector3(Random.Range(-21.0f, 21.0f), 2.6f, Random.Range(-21.0f, 21.0f));
        }
    }
}