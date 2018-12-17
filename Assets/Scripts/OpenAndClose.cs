using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndClose : MonoBehaviour {
    private bool isClosed;

    public GameObject open;
    public GameObject close;
   
    public GameObject pole;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        
        if(isClosed|| GameObject.Find("Main Camera").GetComponent<SceneManager3>().loadAndHaveBoss)
        {
            return;
        }
        switch(other.tag)
        {
            case "player":
                Close();
                break;
        }
    }

    private void Close()
    {
        isClosed = true;
        pole.transform.Rotate(Vector3.forward * -90 );
    }

    public void Open()
    {
        
        open.SetActive(true);
        Invoke("HideOpen", 2.5f);
    }

    private void HideOpen()
    {
        open.SetActive(false);
    }
    
}
