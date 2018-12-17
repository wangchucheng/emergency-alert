using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShell : MonoBehaviour
{

    public float moveSpeed;

    public GameObject shellExplosionProfab;

    private Vector3 shellEulerAngle = new Vector3(0, 0, -90);

    // Update is called once per frame
    private void FixedUpdate()
    {
        //子弹开始飞
        transform.Translate(Vector3.forward * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject.Instantiate(shellExplosionProfab, transform.position, Quaternion.Euler(transform.eulerAngles + shellEulerAngle));

        switch (other.tag)
        {
            case "enemy":
                other.SendMessage("Die");
                SoundManager.Instance.PlaySound("ShellExplosion");
                GameObject.Destroy(this.gameObject);
                break;
            case "block":
                other.SendMessage("Die");
                SoundManager.Instance.PlaySound("ShellExplosion");
                GameObject.Destroy(this.gameObject);
                break;
            case "enemyheart":
                other.SendMessage("Damage");
                SoundManager.Instance.PlaySound("ShellExplosion");
                GameObject.Destroy(this.gameObject);
                break;
            case "boss":
                other.SendMessage("Damage");
                SoundManager.Instance.PlaySound("ShellExplosion");
                GameObject.Destroy(this.gameObject);
                break;
        }
        SoundManager.Instance.PlaySound("ShellExplosion");
        GameObject.Destroy(this.gameObject);
    }
}
