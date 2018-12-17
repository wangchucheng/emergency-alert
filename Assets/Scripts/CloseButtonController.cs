using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButtonController : MonoBehaviour {

    public GameObject aboutUs;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CloseAboutUs()
    {
        aboutUs.SetActive(false);
    }
}
