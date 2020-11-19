using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EconomyTowns : MonoBehaviour
{
    public int townID;
    public bool doneTrade = false, islandWood, islandStone, islandMetall;
    public GameObject islandPanel, islandBttn, next, prev, plus, minus, run;
    public GameObject panel1, panel2, panel3;
    public Text selestCount, countWood, countStone, countMetal,price;

    private float priceWood, priceStone, priceMetall, wood, stone, metall, selectPrice;
    private int i = 1;
    private float buy, n = 0;
    private GameManager gameManager;
    private GlobalTime glTime;

    public void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        glTime = GameObject.Find("GlobalTime").GetComponent<GlobalTime>();

        panel1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(false);
        islandBttn.SetActive(false);

        wood = gameManager.dataBase[townID].countWood;
        stone = gameManager.dataBase[townID].countStone;
        metall = gameManager.dataBase[townID].countMetal;

        priceMetall = 13;
        priceStone = 7;
        priceWood = 4;
    }

    public void Update()
    {
        countWood.text = "Дерево " + buy.ToString();
        countStone.text = "Камень " + buy.ToString();
        countMetal.text = "Металл " + buy.ToString();
        selestCount.text = n.ToString();
        price.text = (n * selectPrice).ToString();

        if (i == 1)
        {
            panel1.SetActive(true);
            panel2.SetActive(false);
            panel3.SetActive(false);
            buy = gameManager.dataBase[townID].countWood;
            selectPrice = priceWood;
        }
        else if (i == 2)
        {
            panel1.SetActive(false);
            panel2.SetActive(true);
            panel3.SetActive(false);
            buy = gameManager.dataBase[townID].countStone;
            selectPrice = priceStone;
        }
        else if (i == 3)
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
            panel3.SetActive(true);
            buy = gameManager.dataBase[townID].countMetal;
            selectPrice = priceMetall;
        }
        else if (i > 3)
            i = 1;
        else if (i <= 0)
            i = 3;

        if (n <= 0)
        {
            gameManager.dataBase[townID].countWood = buy;
            n = 0;
        }
        else if (n > wood)
        {
            n = wood;
        }
    }

    public void TownEconomy()
    {
        switch (glTime.seazon)
        {
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
        }
    }

    public void Buy()
    {
        gameManager.money -= System.Convert.ToInt32(price.text);
        switch (i)
        {
            case 1:
                wood -= n;
                n = 0;
                break;
            case 2:
                stone -= n;
                break;
            case 3:
                metall -= n;
                break;
        }

    }

    public void Next()
    {
        i++;
        n = 0;
        switch (i)
        {
            case 1:
                buy = wood;
                break;
            case 2:
                buy = stone;
                break;
            case 3:
                buy = metall;
                break;
        }
    }

    public void Prev()
    {
        i--;
        n = 0;
        switch (i)
        {
            case 1:
                buy = wood;
                break;
            case 2:
                buy = stone;
                break;
            case 3:
                buy = metall;
                break;
        }
    }

    public void Plus()
    {
        n += 1;

        switch (i)
        {
            case 1:
                if (buy > 0)
                    gameManager.dataBase[townID].countWood -= 1;
                break;
            case 2:
                if (buy > 0)
                    gameManager.dataBase[townID].countStone -= 1;
                break;
            case 3:
                if (buy > 0)
                    gameManager.dataBase[townID].countMetal -= 1;
                break;
        }
    }
    
    public void Minus()
    {
        n -= 1;

        switch (i)
        {
            case 1:
                if (buy < wood)
                    gameManager.dataBase[townID].countWood += 1;
                break;
            case 2:
                if (buy < stone)
                    gameManager.dataBase[townID].countStone += 1;
                break;
            case 3:
                if (buy < metall)
                    gameManager.dataBase[townID].countMetal += 1;
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "PlayerShip")
        {
            islandBttn.SetActive(true);
            doneTrade = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerShip")
        {
            islandBttn.SetActive(false);
            doneTrade = false;
        }
    }
}
