using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollow : MonoBehaviour
{
    public float speed;
    public Transform target;
    public float posY;

    private void Update()
    {
        transform.position = new Vector3(target.position.x,posY,target.position.z);
    }
    
}
