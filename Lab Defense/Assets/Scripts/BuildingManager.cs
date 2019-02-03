using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public static GameObject towerToBuild = null;

    public bool[] slotEmpty;
    public GameObject[] towerButton;
    public GameObject[] towerPrefab;
    public int[] towerCost;

    private int index;

    void Update()
    {

        for(int i = 0; i < towerButton.Length; i++)
        {
            if (DataManager.resource < towerCost[i])
            {
                towerButton[i].GetComponent<Button>().interactable = false;
                if (towerToBuild == towerPrefab[i])
                    towerToBuild = null;
            }
            else
                towerButton[i].GetComponent<Button>().interactable = true;
        }
    }

    public void SetTowerToBuild(int towerIndex)
    {
        towerToBuild = towerPrefab[towerIndex];
        index = towerIndex;
    }

    public void BuyTower()
    {
        DataManager.resource -= towerCost[index];
        GameObject.Find("Game Controller").SendMessage("DataUpdate");
        towerToBuild = null;
    }

    public void SellTower(int refund)
    {
        DataManager.resource += refund;
        GameObject.Find("Game Controller").SendMessage("DataUpdate");
    }
}
