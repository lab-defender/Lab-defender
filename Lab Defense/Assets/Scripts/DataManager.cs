using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static int resource = 500;
    public static int health = 10;

    public Text resourceText;
    public Text healthText;

    private void Start()
    {
        Time.timeScale=1;
        DataUpdate();
    }
    void Update()
    {
        if(health<=0)
        {
            //Application.Quit();
        }
    }
    public void DataUpdate()
    {
        resourceText.text = resource.ToString();
        healthText.text = health.ToString();
    }
}
