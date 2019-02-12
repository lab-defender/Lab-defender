using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    public static List<GameObject> enemylist = new List<GameObject>();
    public GameObject[] enemyprefabs;
    public Transform camTrans;
    public Transform trans;
    public float spawnInterval;
    public int number;

    public int currentlevel=0;
    
    int maxlevel=4;
    public bool on = false;

    private float timer = 0;
    private int count = 0;

    public int enemynum;

    public bool gamecontinue=false;

    public Text showenemynum;

    public GameObject formulapanel;

    public GameObject[] formula;

    int formulacount=0;

    bool changeformula=false;

    public static int totalcount=0;

    void Update()
    {
        enemynum=enemylist.Count;
        showenemynum.text=" Wave :"+currentlevel.ToString()+"\n"+" Enemy Left: "+totalcount.ToString();
        if (on)
        {
            timer += Time.deltaTime;

            if (timer > spawnInterval)
            {
                timer = 0;
                int insnum;
                if(currentlevel!=maxlevel)
                insnum=currentlevel-1;
                else
                insnum=Random.Range(0,currentlevel-1);
                GameObject ene = Instantiate(enemyprefabs[insnum], trans.position, trans.rotation) as GameObject;
                ene.GetComponent<EnemyControl>().cam = camTrans;
                enemylist.Add(ene);
                count++;
            }

            if (count == number)
            {
                //formulapanel.SetActive(true);
                on = false;
                timer = 0;
                count = 0;
            }
        }
        if(!on)
        {
            if (totalcount <= 0)
                {
                    if(currentlevel==4)
                    Invoke("quitgame",3);
                    Time.timeScale=0;
                    if(currentlevel<maxlevel)
                    {
                    formulapanel.SetActive(true);
                    if (!changeformula)
                    {
                        changeformula=true;
                        //Debug.Log(formulacount);
                        formula[formulacount].SetActive(false);
                        formulacount += 1;
                        Debug.Log(formulacount);
                        formula[formulacount].SetActive(true);
                    }
                    }
                     if(gamecontinue)
                        {
                         totalcount=number;
                         formulapanel.SetActive(false);
                         changeformula = false;
                         Time.timeScale = 1;
                         if (currentlevel < maxlevel)
                         currentlevel += 1;
                         if(currentlevel==maxlevel)
                         number=100;
                         on = true;
                         gamecontinue = false;
                        }
                }
        }
    }
    public void nextlevel()
    {
        gamecontinue=true;
    }
    public void quitgame()
    {
        Application.Quit();
    }
}
