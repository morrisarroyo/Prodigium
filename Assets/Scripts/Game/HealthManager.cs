using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    public static float health;
    public Text healthText;
    Image healthFill;

    // Use this for initialization, Awake happens before Start
    void Awake()
    {
        health = 1000f;
        healthFill = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthFill.fillAmount = health / 1000;
        healthText.text = health + "/ 1000";
    }
}
