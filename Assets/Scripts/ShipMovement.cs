using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipMovement : MonoBehaviour
{
    public Transform point;
    public int inventoryCount = 30;
    public string[] inventory;
    public GameObject mapCam;

    public bool button = false;
    public bool isControl = false;
    private Camera cam;
    private NavMeshAgent agent;
    private int i=0;


    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        agent = GetComponent<NavMeshAgent>();
        
        StarShipMoving();
    }


    void Update()
    {
        if (Vector3.Distance(transform.position, point.position) <= 2)
        {
            isControl = false;
        }
        else
            isControl = true;

        if (cam.GetComponent<MovesCamera>().isTarget == true)
        {
            if (Input.GetMouseButtonDown(0) && isControl == true)
            {
                RaycastHit hit;
                if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    agent.SetDestination(hit.point);
                }
            }
        }
        else
            StarShipMoving();

        
    }

    public void Inventory(int countItems)
    {
        if (inventory.Length <= inventoryCount)
        {
            for(int i = 1; i < countItems; i++)
            {

            }
        }
    }

    public void TeleportedToShip()
    {
        cam.GetComponent<MovesCamera>().isTarget = true;
        cam.gameObject.SetActive(true);
        mapCam.SetActive(false);
    }

    // плывет по тем координатам, которые мы зададим
    public void StarShipMoving()
    {
        agent.SetDestination(point.position);
    }
}
