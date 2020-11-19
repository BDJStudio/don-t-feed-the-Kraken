using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBasePanels : MonoBehaviour
{
    public List<ItemPanels> itemsPanel = new List<ItemPanels>();
}


    [System.Serializable]
public class ItemPanels
{
    public int id;
    public Text namePanel;

    public Slider slider;
    public Image filled1, filled2;
    public Text filledText1, filledText2;

    public GlobalTime globalTime;
    public GameManager gm;

}
