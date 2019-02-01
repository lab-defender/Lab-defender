using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    [Header("Spawn")]
    public Transform cam = null;
    public NavMeshAgent agent;
    public GameObject healthBar;
    public GameObject canvas;
    public Vector3 destination;

    [Header("Data")]
    public float health;
    public int worth;

    private float originalHealth;

    private void Start()
    {
        originalHealth = health;
    }

    void Update()
    {
        agent.SetDestination(destination);
        canvas.transform.LookAt(healthBar.transform.position + cam.rotation * Vector3.back, cam.rotation * Vector3.down);
    }

    public void Damage(float number)
    {
        health -= number;
        healthBar.GetComponent<Image>().fillAmount = health / originalHealth;
        if (health <= 0)
            Die();
    }

    void Die()
    {
        DataManager.resource += worth;
        GameObject.Find("Game Controller").SendMessage("DataUpdate");
        Destroy(gameObject);
    }
}
