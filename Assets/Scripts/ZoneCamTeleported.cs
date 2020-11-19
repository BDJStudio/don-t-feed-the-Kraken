using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCamTeleported : MonoBehaviour
{
    public GameObject[] TpPoints;
    public float camSize = 100;
    public GameManager gameManager;
    public GameObject[] buttns;
    public void ClickZone(int zone)
    {
        GetComponent<Camera>().orthographicSize = camSize;
        for (int i = 1; i <= zone; i++)
        {
            if (i == zone)
            {
                transform.position = new Vector3(TpPoints[i - 1].transform.position.x, 80, TpPoints[i - 1].transform.position.z);
                buttns[i - 1].SetActive(false);
                gameManager.i = 1;
            }
        }
    }
}
