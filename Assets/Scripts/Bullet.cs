using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float moveSpeed;         //子弹移动速度
    public int harm = 10;           //子弹伤害
    // Use this for initialization
    void Start () {
       
	}

    // Update is called once per frame
    private void FixedUpdate()
    {
        //子弹开始飞
        transform.Translate(Vector3.up * moveSpeed * Time.fixedDeltaTime);
    }

    //TODO: 可能要改变碰撞判定方式，添加爆炸特效
    private void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "player":
                other.SendMessage("Damage");
                Explode();
                break;
            case "building":
                Explode();
                break;
            case "enemy":
                Explode();
                other.SendMessage("Die");
                break;
            case "bullet":
                Explode();
                break;
            case "turret":
                Explode();
                other.SendMessage("Damage");
                break;
            case "tree":
                Explode();
                other.SendMessage("Die");
                break;
        }
    }

    //TODO:添加爆炸特效和音效
    private void Explode()
    {
        Destroy(gameObject);

    }
}
