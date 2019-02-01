using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static GameObject towerToBuild = null;

    public GameObject[] towerPrefab;

    public void SetTowerToBuild(int index)
    {
        towerToBuild = towerPrefab[index];
    }
}
