using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

    public float Hp;
    public float startHp = 100;
    public float damage;

    // Use this for initialization
    void Start () {
        Hp = startHp;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //老窝销毁
    //TODO: 爆炸特效和音效
    private void Die()
    {
        SoundManager.Instance.PlaySound("HeartExplosion");
        SceneManager1.Instance.isDefeat = true;
        Hp = 0;
        
    }

    //老窝伤害

    private void Damage()
    {
        SoundManager.Instance.PlaySound("Warning");    //受到攻击发出警报
        if (Hp - damage <= 0)
        {
            Die();
        }
        else
        {
            Hp -= damage;
        }
        Debug.Log(Hp);
    }
}
