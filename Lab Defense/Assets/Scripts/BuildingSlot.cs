using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSlot : MonoBehaviour
{
    public int slotIndex;
    public Color hoverColor;
    public Vector3 offset;
    public GameObject gameController;

    private GameObject turret;
    private Renderer render;
    private Color startColor;

    private void Start()
    {
        render = GetComponent<Renderer>();
        startColor = render.material.color;
    }

    void OnMouseDown()
    {
        if (BuildingManager.towerToBuild == null)
            return;

        if (turret != null)
        {
            return;
        }

        gameController.GetComponent<BuildingManager>().slotEmpty[slotIndex] = false;
        GameObject towerToBuild = BuildingManager.towerToBuild;
        turret = (GameObject)Instantiate(towerToBuild, transform.position + offset, transform.rotation);
        turret.GetComponent<Turret>().slotIndex = slotIndex;
        GameObject.Find("Game Controller").SendMessage("BuyTower");
    }

    void OnMouseEnter()
    {
        if (BuildingManager.towerToBuild == null)
            return;

        if(gameController.GetComponent<BuildingManager>().slotEmpty[slotIndex])
            render.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        render.material.color = startColor;
    }
}
