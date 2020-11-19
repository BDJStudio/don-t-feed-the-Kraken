using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoFarming : MonoBehaviour
{
    public string nameFarm;
    public int lvlFarm;
    public int countMax;
    public int nextLvlCount;
    public int count;

    public bool dontNull;
    private int lvlPrev, day;

    private DataBasePanels dataBase;
    private GameManager gm;

    private Image filled1, filled2;
    private Text filledText1, filledText2;
    private Slider slider;


    public void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        dataBase = gm.GetComponent<DataBasePanels>();

        slider = dataBase.itemsPanel[0].slider;
        filled1 = dataBase.itemsPanel[0].filled1;
        filled2 = dataBase.itemsPanel[0].filled2;
        filledText1 = dataBase.itemsPanel[0].filledText1;
        filledText2 = dataBase.itemsPanel[0].filledText2;

        dataBase.itemsPanel[0].namePanel.text = nameFarm + " ур " + lvlFarm;

        lvlPrev = 1;
        countMax = 200;
        nextLvlCount = 200;
        dontNull = false;

        day = dataBase.itemsPanel[0].globalTime.day;
    }

    public void Update()
    {
        if (lvlFarm > lvlPrev)
            countMax += nextLvlCount;

        if (count < countMax)
        {
            if (dataBase.itemsPanel[0].globalTime.day >= day)
            {
                count += 15;
                day++;
            }
        }

        filled1.fillAmount = (1 - (slider.value / count));
        filled2.fillAmount = (slider.value / (slider.maxValue / 100)) / 100;

        filledText1.text = "Загруженность склада: " + Mathf.RoundToInt(filled1.fillAmount * 100 * count / 100);
        filledText2.text = "Загруженность корабля: " + Mathf.RoundToInt(filled2.fillAmount * 100 * slider.maxValue / 100);

        //if (slider.maxValue >= Mathf.RoundToInt(filled1.fillAmount * 100 * count / 100))
    }

    IEnumerator Await()
    {
        yield return new WaitForSeconds(1);
            gm.FarmingIsland("Wood_BuindingZone");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerShip")
        {
            StartCoroutine(Await());
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "PlayerShip")
        {
            dontNull = true;
            slider.maxValue = other.gameObject.GetComponent<ShipMovement>().inventoryCount;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerShip")
        {
            dontNull = false;
            gm.ButtonClick("Close");
            slider.maxValue = 0;
        }
    }
}
