using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static int resource = 100;
    public static int health = 10;

    public Text resourceText;
    public Text healthText;

    private void Start()
    {
        DataUpdate();
    }

    public void DataUpdate()
    {
        resourceText.text = resource.ToString();
        healthText.text = health.ToString();
    }
}
