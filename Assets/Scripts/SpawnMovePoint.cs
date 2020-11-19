using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMovePoint : MonoBehaviour
{
    public Vector3 boundary;// границы мира

    public GameObject kraken;
    private Vector3 targetPoint;

    private void Start()
    {
        SpawnPoint();
    }

    private void Update()
    {
        targetPoint = transform.position;

        if (transform.position.x < boundary.x || transform.position.z < boundary.x ||
            transform.position.x > boundary.z || transform.position.z > boundary.z)
        {
            transform.position = new Vector3(0, 0, 0); // услоие при котором точка спавнится  0, если она вышла за границы мира
        }
        
        // рисуем линии о вьюпорте
        Debug.DrawRay(transform.position, transform.forward * 4f, Color.yellow);
        Debug.DrawRay(transform.position, -transform.forward * 4f, Color.yellow);
        Debug.DrawRay(transform.position, transform.right * 4f, Color.yellow);
        Debug.DrawRay(transform.position, -transform.right * 4f, Color.yellow);
        Debug.DrawRay(transform.position, -transform.up * 2f, Color.yellow);

        RaycastHit hit; 

        // проверка на то есть ли рядом с лучем остров, сделано для отступа
        if (Physics.Raycast(transform.position, transform.forward, out hit, 4f) ||
            Physics.Raycast(transform.position, -transform.forward, out hit, 4f) ||
            Physics.Raycast(transform.position, transform.right, out hit, 4f) ||
            Physics.Raycast(transform.position, -transform.right, out hit, 4f))
        {
            if (hit.collider.tag == "Island" || hit.collider.tag == "Port")
                transform.position += new Vector3(4, 0, 4);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Island")
        {
            SpawnPoint();
        }
    }

    public void SpawnPoint()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(kraken.transform.position.x - 15, kraken.transform.position.x + 15), 0, 
                                            Random.Range(kraken.transform.position.z - 15, kraken.transform.position.z + 15));

        transform.position = spawnPosition;
    }
}
