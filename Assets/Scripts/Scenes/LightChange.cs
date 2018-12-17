using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChange : MonoBehaviour {
    private float Speed = 8f;
    private float timer;

    public Light light;
	// Use this for initialization
	void Start () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        transform.Translate(transform.right * -Speed*Time.deltaTime , Space.World);
        if (transform.position.x >= -600 && transform.position.x <= -601)
        {
            Debug.Log(timer);
            
        }
        light.intensity = 1;
	}
}
