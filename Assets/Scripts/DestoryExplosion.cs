using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryExplosion : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Invoke("DestoryIt", 1f);
    }

    public void DestoryIt()
    {
        Destroy(gameObject);
    }
}
