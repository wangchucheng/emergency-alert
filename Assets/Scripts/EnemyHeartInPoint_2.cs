using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHeartInPoint_2 : MonoBehaviour {
    public GameObject HeartAware;        //生成的收集奖励；
    public GameObject Explosion;        //心脏爆炸特效
    public Slider HpSlider;
    public float HeartHp;             //敌人实时心脏生命
    private float startHp=30;
    private float damage = 10;
	// Use this for initialization
	void Start () {
        HeartHp = startHp;
        HpSlider.GetComponent<Slider>().value = HeartHp / startHp;
	}
	
	// Update is called once per frame
	void Update () {

        HpUpdate();
        
    }

    private void Damage()
    {
        Debug.Log(HeartHp);
        HeartHp -= damage;
        if (HeartHp<=0)
        {
            Die();
        }
    }

    private void HpUpdate()
    {
        HpSlider.GetComponent<Slider>().value = HeartHp / startHp;
    }

    private void Die()
    {
        SoundManager.Instance.PlaySound("EnemyHeartExplosion");
        GameObject explosion = Instantiate(Explosion, transform.position, Quaternion.identity);
        Instantiate(HeartAware, transform.position, Quaternion.identity);
        Destroy(explosion, 1f);
        this.gameObject.SetActive(false);
        Destroy(this.gameObject, 1.1f);
    }
}
