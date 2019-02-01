using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSlot : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 offset;

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

        GameObject towerToBuild = BuildingManager.towerToBuild;
        turret = (GameObject)Instantiate(towerToBuild, transform.position + offset, transform.rotation);
    }

    void OnMouseEnter()
    {
        if (BuildingManager.towerToBuild == null)
            return;

        render.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        render.material.color = startColor;
    }
}
