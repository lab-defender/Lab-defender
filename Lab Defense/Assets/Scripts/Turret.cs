using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    private Transform target;
    private float fireTimer = 0;
    private int counter = 0;
    private GameObject[] enemies;
    private bool beenUpgrade = false;

    [Header("Variable")]
    public float range = 6f;
    public float rotateSpeed = 10f;
    public float fireInterval = 0.5f;
    public float rangeUp = 3f;
    public int upgradeCost;
    public int[] sellRefund;

    [Header("Don't Mind")]
    public int slotIndex = 0;
    public int tier = 0;

    [Header("Intrinsic Setting")]
    public string enemyTag = "Enemy";
    public Vector3 offset;
    public Transform pivot;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject zone;
    public GameObject menu;
    public GameObject shadow;
    public GameObject destroyEffect;
    public Button upgradeButton;
    public Text upgradeCostText;
    public Text sellText;


    void Start()
    {
        // InvokeRepeating("UpdateTarget", 0f, 0.5f);
        upgradeCostText.text = upgradeCost.ToString();
        sellText.text = sellRefund[0].ToString();
    }

    void Update()
    {
        UpdateTarget();
        if (DataManager.resource < upgradeCost || beenUpgrade)
        {
            upgradeButton.interactable = false;
            shadow.SetActive(true);
        }
        else
        {
            upgradeButton.interactable = true;
            shadow.SetActive(false);
        }

        if (target == null)
            return;

        //Target Lock
        Vector3 diretion = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(diretion);
        Vector3 rotation = Quaternion.Lerp(pivot.rotation, targetRotation, Time.deltaTime * rotateSpeed).eulerAngles;
        pivot.rotation = Quaternion.Euler(0, rotation.y, 0f);

        if (fireTimer > fireInterval)
        {
            fireTimer = 0;
            Shoot();
        }

        fireTimer += Time.deltaTime;
    }

    void UpdateTarget()
    {
        float distance;
        if (target != null)
        {
            distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance > range)
                target = null;
        }
        else
        {
            foreach (GameObject enemy in EnemySpawn.enemylist)
            {
                if (enemy != null)
                {
                    distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < range)
                    {
                        target = enemy.transform;
                        break;
                    }
                }
            }
        }
    }

    void Shoot()
    {
        GameObject bulletPre = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletPre.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void OnMouseDown()
    {
        zone.SetActive(true);
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        zone.SetActive(false);
        menu.SetActive(false);
    }

    public void Sell()
    {
        GameObject.Find("Game Controller").GetComponent<BuildingManager>().slotEmpty[slotIndex] = true;
        GameObject.Find("Game Controller").GetComponent<BuildingManager>().SellTower(sellRefund[tier]);
        menu.SetActive(false);
        zone.SetActive(false);
        GameObject effect = Instantiate(destroyEffect, gameObject.transform.position + offset, gameObject.transform.rotation);
        Destroy(effect, 0.5f);
        Destroy(gameObject);
    }

    public void Upgrade()
    {
        tier++;
        range += rangeUp;
        beenUpgrade = true;
        sellText.text = sellRefund[tier].ToString();
        DataManager.resource -= upgradeCost;
        GameObject.Find("Game Controller").SendMessage("DataUpdate");

        upgradeButton.GetComponent<UpgradeButton>().beenUpgraded = true;

        Vector3 preScale = zone.GetComponent<Transform>().localScale;
        zone.GetComponent<Transform>().localScale = new Vector3(range, preScale.y, range);
    }
}