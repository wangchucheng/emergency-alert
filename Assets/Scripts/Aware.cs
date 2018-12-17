using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aware : MonoBehaviour {
    public GameObject[] awares;      //奖励 0：回血 1：加速 2：无敌 3：增加攻击力
    public GameObject DestoryEffect;   //销毁特效
    private int[] Num = { 0, 0, 0, 1, 1, 1, 2, 3, 3, 0 };

    public void Die()
    {
        int ranNum = Random.Range(0, Num.Length);
        Vector3 awarePos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        Instantiate(awares[Num[ranNum]], awarePos, Quaternion.Euler(-90,0,90));
        GameObject DeGo = Instantiate(DestoryEffect, transform.position, Quaternion.identity);
        Destroy(DeGo, 1f);
        this.gameObject.SetActive(false);
        Destroy(gameObject, 1.1f);
    }

}
