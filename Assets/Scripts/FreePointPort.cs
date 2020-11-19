using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreePointPort : MonoBehaviour
{
    public bool free;

    private void Start()
    {
        free = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Ship" || other.tag == "PlayerShip")
        {
            free = false;
        }
    }
}
