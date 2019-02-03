using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public static List<GameObject> enemylist = new List<GameObject>();

    public Transform camTrans;
    public Transform trans;
    public GameObject enemyPrefab;
    public float spawnInterval;
    public int number;
    public bool on = true;

    private float timer = 0;
    private int count = 0;

    void Update()
    {
        if (on)
        {
            timer += Time.deltaTime;

            if (timer > spawnInterval)
            {
                timer = 0;
                GameObject ene = Instantiate(enemyPrefab, trans.position, trans.rotation) as GameObject;
                ene.GetComponent<EnemyControl>().cam = camTrans;
                enemylist.Add(ene);
                count++;
            }

            if (count == number)
            {
                on = false;
                timer = 0;
                count = 0;
            }
        }
    }
}
