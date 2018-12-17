using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUIHP : MonoBehaviour
{
    private float maxHealth;
    public float currentHealth;
    
    private GameObject heart;
    // Use this for initialization
    void Start()
    {

        heart = GameObject.FindWithTag("home");
        maxHealth = heart.GetComponent<Heart>().startHp;
        currentHealth = maxHealth;


    }

    // Update is called once per frame
    void Update()
    {
        UIChanged();
    }

    private void UIChanged()
    {
        currentHealth = heart.GetComponent<Heart>().Hp;
        GetComponent<Slider>().value = currentHealth / maxHealth;
    }
}
