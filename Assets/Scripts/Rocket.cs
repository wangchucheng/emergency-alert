using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    public float moveSpeed;

    public GameObject shellExplosionProfab;
    

    // Update is called once per frame
    private void FixedUpdate()
    {
        //子弹开始飞
        transform.Translate(Vector3.forward * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject.Instantiate(shellExplosionProfab, transform.position, Quaternion.identity);

        switch (other.tag)
        {
            case "player":
                other.SendMessage("Damage");
                break;
            case "block":
                other.SendMessage("Die");
                break;
        }
        SoundManager.Instance.PlaySound("ShellExplosion");
        GameObject.Destroy(this.gameObject);
    }
}
