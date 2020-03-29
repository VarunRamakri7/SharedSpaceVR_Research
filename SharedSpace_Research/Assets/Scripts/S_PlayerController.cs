using UnityEngine;
using UnityEngine.AI;

public class S_PlayerController : MonoBehaviour
{
    public Camera camera;
    public NavMeshAgent agent;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            // Check if ray hits anything
            if (Physics.Raycast(ray, out hit))
            {
                // Move agent
                agent.SetDestination(hit.point);
            }
        }
    }
}
