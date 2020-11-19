using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipNPC : MonoBehaviour
{
    public GameObject[] point;

    private NavMeshAgent agent;
    private int i,n;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        point = GameObject.FindGameObjectsWithTag("Point");
        i = Random.Range(0, point.Length);
        StarShipMoving();
    }


    void Update()
    {
        if (Vector3.Distance(transform.position, point[i].transform.position) <= 2)
        {
            agent.isStopped = true;
            n = i;
        }
        
        if (agent.isStopped)
                StartCoroutine(RestartShipMoving());
    }

    IEnumerator RestartShipMoving()
    {
        point = GameObject.FindGameObjectsWithTag("Point");

        yield return new WaitForSeconds(3);
        agent.isStopped = false;
        if (i == n)
            i = Random.Range(0, point.Length);
        StarShipMoving();
    }

    // плывет по тем координатам, которые мы зададим
    public void StarShipMoving()
    {
        if (point[i].GetComponent<FreePointPort>().free == true)
            agent.SetDestination(point[i].transform.position);
        else
            RestartShipMoving();
    }
}
