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

    bool gotheat=false;

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
      
        float heat=0;
        
        if(gotheat)
        {
          heat=number;
        }
    
        health -= number+heat;
        healthBar.GetComponent<Image>().fillAmount = health / originalHealth;
        if (health <= 0)
            Die();
    }
    void OnTriggerEnter(Collider c)
    {
        Debug.Log("onboard");
       if(c.gameObject.tag=="heatboard")
        {
            Debug.Log("onboard");
            gotheat = true;
        }
    }
      void OnTriggerStay(Collider c)
    {
       if(c.gameObject.tag=="heatboard")
       gotheat=true;
    }


     void OnTriggerExit(Collider c)
    {
      if(c.gameObject.tag=="heatboard")
      gotheat=false;
    }


    void Die()
    {
        DataManager.resource += worth;
        GameObject.Find("Game Controller").SendMessage("DataUpdate");
        EnemySpawn.enemylist.Remove(this.gameObject);
        EnemySpawn.totalcount-=1;
        Destroy(gameObject);
    }
}
