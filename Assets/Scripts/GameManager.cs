using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text moneyText, priceText, uplvlText, foodText, woodText, metalText, stoneText, openMapText;
    public GameObject buyBttn, upLvlPanel, map, mapBttn;
    public GameObject mapCamera;
    public Transform canvasMiddle;

    private GameObject cam;
    public int i = 0;

    public float money = 100;
    private float wood = 100, metal = 100, stone = 100, food = 100;
    
    private string isName;
    private bool isActive = true;
    private GameObject isDestroy;
    private Transform buyPos;

    public List<DataBase> dataBase = new List<DataBase>(); //список 

    public void Start()
    {
        cam = GameObject.Find("Main Camera");
        buyBttn.SetActive(false);
        upLvlPanel.SetActive(false);
        map.SetActive(false);

        for (int i = 0; i < mapCamera.GetComponent<ZoneCamTeleported>().buttns.Length; i++)
        {
            mapCamera.GetComponent<ZoneCamTeleported>().buttns[i].SetActive(false);
        }
    }

    public void Update()
    {
        moneyText.text = money.ToString();
        woodText.text = wood.ToString();
        metalText.text = metal.ToString();
        stoneText.text = stone.ToString();
        foodText.text = food.ToString();

        // при нажатие на города мы показывам кнопку
        if (Input.GetMouseButtonDown(0))
        {
            MouseClick();
        }
    }

    public void MouseClick()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit))
        {
            switch (hit.collider.name)
            {
                case "Wood_BuindingZone":
                    if (isActive)
                    {
                        buyBttn.SetActive(true);
                        isActive = false;
                        Time.timeScale = 0;

                        priceText.text = "Монеты " + dataBase[6].price.ToString();
                        isName = hit.collider.name;

                        isDestroy = hit.collider.gameObject;

                        buyPos = hit.collider.transform;
                    }
                    break;
                
                case "Stone_BuindingZone":
                    if (isActive)
                    {
                        buyBttn.SetActive(true);
                        isActive = false;
                        Time.timeScale = 0;

                        priceText.text = "Монеты " + dataBase[6].price.ToString();
                        isName = hit.collider.name;

                        isDestroy = hit.collider.gameObject;

                        buyPos = hit.collider.transform;
                    }
                    break;

                case "BuindingZone_0":
                    if (isActive)
                    {
                        buyBttn.SetActive(true);
                        isActive = false;
                        Time.timeScale = 0;

                        priceText.text = "Монеты " + dataBase[2].price.ToString() + "\n" +
                                         "Дерево " + dataBase[2].countWood.ToString() + "\n" +
                                         "Камень " + dataBase[2].countStone.ToString() + "\n" +
                                         "Металл " + dataBase[2].countMetal.ToString();
                        isName = "storage_Lvl1";

                        isDestroy = hit.collider.gameObject;
                        
                        buyPos = hit.collider.transform;
                    }
                    break;

                case "BuindingZone_1":

                    if (isActive)
                    {
                        buyBttn.SetActive(true);
                        isActive = false;
                        Time.timeScale = 0;

                        priceText.text = "Монеты " + dataBase[3].price.ToString() + "\n" +
                                         "Дерево " + dataBase[3].countWood.ToString() + "\n" +
                                         "Камень " + dataBase[3].countStone.ToString() + "\n" +
                                         "Металл " + dataBase[3].countMetal.ToString();
                        isName = "HousePort_Lvl1";

                        isDestroy = hit.collider.gameObject;

                        buyPos = hit.collider.transform;
                    }
                    break;
                case "BuindingZone_2":

                    if (isActive)
                    {
                        buyBttn.SetActive(true);
                        isActive = false;
                        Time.timeScale = 0;

                        priceText.text = "Монеты " + dataBase[4].price.ToString() + "\n" +
                                         "Дерево " + dataBase[4].countWood.ToString() + "\n" +
                                         "Камень " + dataBase[4].countStone.ToString() + "\n" +
                                         "Металл " + dataBase[4].countMetal.ToString();

                        isName = "lighthouse_Lvl1";
                        isDestroy = hit.collider.gameObject;

                        buyPos = hit.collider.transform;
                    }
                    break;

                case "lighthouse_Lvl1":

                    if (isActive)
                    {
                        upLvlPanel.SetActive(true);
                        isActive = false;
                        Time.timeScale = 0;

                        uplvlText.text = "Монеты " + dataBase[5].price.ToString() + "\n" +
                                         "Дерево " + dataBase[5].countWood.ToString() + "\n" +
                                         "Камень " + dataBase[5].countStone.ToString() + "\n" +
                                         "Металл " + dataBase[5].countMetal.ToString();

                        isName = "lighthouse_Lvl2";
                        isDestroy = hit.collider.gameObject;
                        buyPos = hit.collider.transform;
                    }
                    break;

                case "WoodFarm(Clone)":
                    FarmingIsland("Wood_BuindingZone");
                    break;
                
                case "StoneFarm(Clone)":
                    FarmingIsland("Stone_BuindingZone");
                    break;
            }
        }
    }

    public void ButtonClick(string name)
    {
        switch (name)
        {
            case "Town":
                i++;
                if (i == 1)
                {
                    mapBttn.GetComponent<Button>().interactable = false;
                    Time.timeScale = 0;
                    dataBase[0].panelTown.SetActive(true);
                }
                else if (i > 0)
                {
                    Time.timeScale = 1;
                    ButtonClick("Close");
                    i = 0;
                }
                break;
            
            case "Port":
                Time.timeScale = 0;
                dataBase[1].panelTown.SetActive(true);
                dataBase[0].panelTown.SetActive(false);
                i = 0;
                break;

            case "Close":
                Time.timeScale = 1;
                dataBase[0].button.GetComponent<Button>().interactable = true;
                mapBttn.GetComponent<Button>().interactable = true;
                isActive = true;
                i = 0;
                DisableChilds();
                break;

            case "Buy":
                BuyBuilding();
                break;

            case "CloseBuy":
                Time.timeScale = 1;
                buyBttn.SetActive(false);
                upLvlPanel.SetActive(false);
                isActive = true;
                
                break;

            case "LvLUp":
                UpLvlBuy();
                break;

            case "Menu":
                isActive = false;
                break;

            case "OpenMap":
                i++;
                // условие нужно для того что бы сделать повторное нажатие на кнопку
                if (i == 1)
                {
                    openMapText.text = "Назад";
                    map.SetActive(true);
                    cam.GetComponent<MovesCamera>().isTrigger = false;
                    dataBase[0].button.GetComponent<Button>().interactable = false;
                    isActive = false;

                    for (int i = 0; i < mapCamera.GetComponent<ZoneCamTeleported>().buttns.Length; i++)
                    {
                        mapCamera.GetComponent<ZoneCamTeleported>().buttns[i].SetActive(true);
                    }
                }
                else if (i == 2)
                {
                    mapCamera.GetComponent<Camera>().orthographicSize = 300;
                    mapCamera.transform.position = new Vector3(0, 80, 0);

                    for (int i = 0; i < mapCamera.GetComponent<ZoneCamTeleported>().buttns.Length; i++)
                    {
                        mapCamera.GetComponent<ZoneCamTeleported>().buttns[i].SetActive(true);
                    }

                    if (cam.GetComponent<MovesCamera>().isTarget)
                    {
                        openMapText.text = "Карта";
                        map.SetActive(false);
                        cam.gameObject.SetActive(true);
                        cam.GetComponent<MovesCamera>().isTarget = false;
                        cam.GetComponent<MovesCamera>().isTrigger = true;
                        dataBase[0].button.GetComponent<Button>().interactable = true;
                        isActive = true;
                        i = 0;
                    }
                }
                else if (i > 2)
                {
                    openMapText.text = "Карта";
                    map.SetActive(false);
                    cam.gameObject.SetActive(true);
                    cam.GetComponent<MovesCamera>().isTarget = false;
                    cam.GetComponent<MovesCamera>().isTrigger = true;
                    dataBase[0].button.GetComponent<Button>().interactable = true;
                    isActive = true;
                    i = 0;

                    for (int i = 0; i < mapCamera.GetComponent<ZoneCamTeleported>().buttns.Length; i++)
                    {
                        mapCamera.GetComponent<ZoneCamTeleported>().buttns[i].SetActive(false);
                    }
                }
                break;
        }

        // этот цикл находит кнопку на которую мы нажали, делает ее видимой, а все остальные скрывает
        for (int i = 0; i < dataBase.Count; i++)
        {
            for (int y = 0; y < dataBase[i].bttnsPanels.Length; y++)
            {
                
                if (dataBase[i].bttnsPanels.Length > 0)
                {
                    if (name == dataBase[i].bttnsPanels[y].name)
                    {
                        dataBase[i].bttnsPanels[y].SetActive(true);
                    }
                    else
                        dataBase[i].bttnsPanels[y].SetActive(false);
                }
            }

            
        }
    }

    // функция закрытия окон
    public void DisableChilds()
    {
        for (int i = 0; i < canvasMiddle.childCount; i++)
        {
            canvasMiddle.GetChild(i).gameObject.SetActive(false); // Выключаем или включаем каждого полученного ребёнка по порядку.
        }
    }

    // открывает окно для островов фарма
    public void FarmingIsland(string name)
    {
        for (int i = 0; i < dataBase.Count; i++)
        {
            if (name == dataBase[i].name)
            {
                isActive = false;
                Time.timeScale = 0;
                dataBase[i].panelTown.SetActive(true);
            }

            if (isActive == true)
            {
                if (dataBase[i].button != null)
                    dataBase[i].button.GetComponent<Button>().interactable = false;
                mapBttn.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void BuyBuilding()
    {
        Time.timeScale = 1;
        isActive = true;
        for (int i = 0; i < dataBase.Count; i++)
        {
            if (isName == dataBase[i].name)
            {
                if (money >= dataBase[i].price &&
                    wood >= dataBase[i].countWood &&
                    stone >= dataBase[i].countStone &&
                    metal >= dataBase[i].countMetal)
                {
                    Destroy(isDestroy);

                    Quaternion spawnPos;

                    spawnPos = new Quaternion(0, buyPos.rotation.y, 0, 0);
                    Instantiate(dataBase[i].prifab, buyPos.position, spawnPos,buyPos.parent);

                    money -= dataBase[i].price;
                    wood -= dataBase[i].countWood;
                    stone -= dataBase[i].countStone;
                    metal -= dataBase[i].countMetal;

                    buyBttn.SetActive(false);
                }
                else
                    priceText.text = "Nety deniag";
            }
        }
    }

    public void UpLvlBuy()
    {
        Time.timeScale = 1;
        isActive = true;

        for (int i = 0; i < dataBase.Count; i++)
        {
            if (isName == dataBase[i].name)
            {
                if (money >= dataBase[i].price &&
                    wood >= dataBase[i].countWood &&
                    stone >= dataBase[i].countStone &&
                    metal >= dataBase[i].countMetal)
                {
                    Destroy(isDestroy);

                    Quaternion spawnPos;

                    spawnPos = new Quaternion(0, buyPos.rotation.y, 0, 0);
                    Instantiate(dataBase[i].prifab, buyPos.position - new Vector3(0, buyPos.position.y, 0), spawnPos, buyPos.parent);

                    money -= dataBase[i].price;
                    wood -= dataBase[i].countWood;
                    stone -= dataBase[i].countStone;
                    metal -= dataBase[i].countMetal;

                    upLvlPanel.SetActive(false);
                }
                else
                    uplvlText.text = "Nety deniag";
            }
        }
    }

    
}

[System.Serializable]
    public class DataBase
    {
        public int id;
        public Sprite img;
        public string name;
        public GameObject prifab;
        public float price, countWood, countStone, countMetal;

        public GameObject button;
        public GameObject panelTown;
        public GameObject[] bttnsPanels;
    }