using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeartRotation : MonoBehaviour {
    public Transform Heart;
    public float RotSpeed;
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(Heart.position, Vector3.forward, RotSpeed * Time.deltaTime);
	}
}
