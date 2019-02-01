using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private float fireTimer = 0;
    private int counter = 0;
    private GameObject[] enemies;

    [Header("Variable")]
    public float range = 6f;
    public float rotateSpeed = 10f;
    public float fireInterval = 0.5f;

    [Header("Intrinsic Setting")]
    public string enemyTag = "Enemy";
    public Transform pivot;
    public Transform firePoint;
    public GameObject bulletPrefab;
    
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update()
    {
        if (target == null)
            return;

        //Target Lock
        Vector3 diretion = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(diretion);
        Vector3 rotation = Quaternion.Lerp(pivot.rotation,targetRotation,Time.deltaTime * rotateSpeed).eulerAngles;
        pivot.rotation = Quaternion.Euler(0, rotation.y, 0f);
        
        if (fireTimer > fireInterval)
        {
            fireTimer = 0;
            Shoot();
        }

        fireTimer += Time.deltaTime;
    }

    /*
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;
        
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    */

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
}
