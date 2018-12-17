using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("DestoryMe", 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void DestoryMe()
    {
        Destroy(gameObject);
    }
}
