using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public void PurchaseWater()
    {
        GameObject.Find("Game Controller").SendMessage("SetTowerToBuild", 0);
    }

    public void PurchaseCarbolicAcid()
    {
        GameObject.Find("Game Controller").SendMessage("SetTowerToBuild", 1);
    }
}
