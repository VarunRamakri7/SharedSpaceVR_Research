using System;
using UnityEngine;
using UnityEngine.AI;

public class S_MoveAgent : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform goal;
    GameObject path;
    Vector3 oldPosition;

    void Update()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.destination = goal.position;

        spawnObject();
    }

    private void spawnObject()
    {

        Instantiate(path, new Vector3(agent.transform.position.x, 0, agent.transform.position.z), Quaternion.identity);
    }
}