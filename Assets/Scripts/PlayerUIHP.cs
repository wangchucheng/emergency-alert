using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHP : MonoBehaviour {

    private float maxHealth;
    public float currentHealth;

    private GameObject player;
    // Use this for initialization
    void Start()
    {

        player = GameObject.FindWithTag("player");
        maxHealth = player.GetComponent<Player>().startHp;
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        UIChanged();
    }

    private void UIChanged()
    {
        currentHealth = player.GetComponent<Player>().Hp;
        GetComponent<Slider>().value = currentHealth / maxHealth;
    }
}
