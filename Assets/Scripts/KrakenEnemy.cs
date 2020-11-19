using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KrakenEnemy : MonoBehaviour
{
    public GameObject targetPoint;

    
    private float distance;
    private bool freeMove;
    
    private SpawnMovePoint SpMvPoint;
    private NavMeshAgent agent;
    
    public List<GameObject> ship;

    void Start()
    {
        SpMvPoint = targetPoint.GetComponent<SpawnMovePoint>();
        agent = GetComponent<NavMeshAgent>();
        freeMove = true;
        targetPoint.GetComponent<SpawnMovePoint>().SpawnPoint();
    }

    void Update()
    {
        // определяем дистанцию между кракеном и точкой
        distance = Vector3.Distance(transform.position, targetPoint.GetComponent<Transform>().position);

        Debug.DrawRay(transform.position + Vector3.up, transform.forward * 2f, Color.yellow);

        // пускаем 2 луча вперд и назад, и если дистанция от луча сзади до точки меньне, то мы пересоздаем точку
        // это сделанно для более плавного и реалистичного движения
        RaycastHit hit,hit2;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 4f) &&
            Physics.Raycast(transform.position, -transform.forward, out hit2, 4f))
        {
            if(Vector3.Distance(hit.transform.position, targetPoint.GetComponent<Transform>().position) 
                > Vector3.Distance(hit2.transform.position, targetPoint.GetComponent<Transform>().position))
                SpMvPoint.SpawnPoint();
        }

        // дистанция при которой мы спавним новую точку
        if (distance <=2)
            SpMvPoint.SpawnPoint();

        // зачатки функции для охоты на корабли
        if (freeMove)
            TargetOffset(targetPoint.GetComponent<Transform>().position);
    }

    void TargetOffset(Vector3 transform)
    {
        agent.SetDestination(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerShip")
        {
            freeMove = false;
            TargetOffset(other.gameObject.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerShip")
        {
            if (Vector3.Distance(transform.position, other.gameObject.transform.position) >= 5)
            freeMove = true;
        }
    }
}
