using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUIHP : MonoBehaviour {

    private float maxHealth;
    public float currentHealth;

    private GameObject boss;
    // Use this for initialization
    void Start () {
        boss = GameObject.FindWithTag("boss");
        maxHealth = boss.GetComponent<Boss>().startHp;
        currentHealth = maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
        UIChanged();
    }

    private void UIChanged()
    {
        currentHealth = boss.GetComponent<Boss>().Hp;
        GetComponent<Slider>().value = currentHealth / maxHealth;
        if (currentHealth<=0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
