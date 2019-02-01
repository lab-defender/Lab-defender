using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    public NavMeshAgent agent;
    public Vector3 des;


    void Update()
    {
        agent.SetDestination(des);
    }
}
