using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CheckGrid : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        if ((collision.gameObject.tag == "obstacle") || (collision.gameObject.tag == "Path"))
        {
            Debug.Log("Collision Detected: " + collision.gameObject.name);
            this.gameObject.transform.position = new Vector3(Random.Range(-21.0f, 21.0f), 2.6f, Random.Range(-21.0f, 21.0f));
        }
    }
}
