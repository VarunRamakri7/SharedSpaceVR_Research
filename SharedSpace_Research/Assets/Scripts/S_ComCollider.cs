using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ComCollider : MonoBehaviour
{
    public char[] location;
    public static bool canPlace;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("obstacle"))
        {
            canPlace = false;
            S_GeneratePath.CommonTrigger(location);
        }
    }
}
