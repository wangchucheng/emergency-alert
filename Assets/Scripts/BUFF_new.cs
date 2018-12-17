using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BUFF_new : MonoBehaviour {
    public int buffID;
    public GameObject DestroyEffect;        //加成物品消失的特效
	
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            switch (buffID)
            {
                case 0:
                    other.SendMessage("Recover");
                    break;
                case 1:
                    other.SendMessage("Accelerate");
                    break;
                case 2:
                    other.SendMessage("Defend");
                    break;
                case 3:
                    other.SendMessage("Stronger");
                    break;
            }
            SoundManager.Instance.PlaySound("Buff");
            GameObject EGo = GameObject.Instantiate(DestroyEffect, transform.position, Quaternion.identity);
            Destroy(EGo, 0.5f);
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
