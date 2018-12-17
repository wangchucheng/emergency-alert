using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierPole : MonoBehaviour {

    public GameObject Pole;

    //玩家坦克进入
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            Pole.transform.Rotate(Vector3.forward * 90);
        }

    }

    //玩家坦克出去
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "player")
        {
            Pole.transform.Rotate(Vector3.forward * -90);
        }
    }
}
