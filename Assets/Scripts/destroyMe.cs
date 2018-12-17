using UnityEngine;
using System.Collections;

public class destroyMe : MonoBehaviour{

	// Use this for initialization
	void Start()
    {
        Invoke("Destroy(this.gameObject)", 1f);
    }
	
}
