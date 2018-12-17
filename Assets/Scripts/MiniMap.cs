using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour {

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("player");
    }
    

    private void LateUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 position = player.transform.position;
        position.y = 50;
        transform.position = position;
    }
}
