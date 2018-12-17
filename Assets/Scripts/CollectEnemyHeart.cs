using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectEnemyHeart : MonoBehaviour {
    public GameObject UInum;

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "player")
        {
            SoundManager.Instance.PlaySound("HeartCollect");
            UInum.SendMessage("AddCount");
            Destroy(this.gameObject);
        }
    }
}
