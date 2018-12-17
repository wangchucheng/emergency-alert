using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEffect : MonoBehaviour {
    
    public List<GameObject> buffEffects = new List<GameObject>();
    public float time = 10f;
    

    private void Recover()
    {
        CreateBuffEffect(buffEffects[0], transform.position, transform.rotation,1f);
        GetComponent<Player>().Hp += 30;
        if (GetComponent<Player>().Hp >= 150)
        {
            GetComponent<Player>().Hp = 150;
        }
    }

	private void Accelerate()
    {
        CreateBuffEffect(buffEffects[1], transform.position, Quaternion.Euler(-90,0,0), 5f);
        if (GetComponent<Player>().moveSpeed < 380)
        {
            GetComponent<Player>().moveSpeed += 50;
        }
        if(GetComponent<Player>().rotateSpeed < 10)
        {

            GetComponent<Player>().rotateSpeed += 2;
        }
        Invoke("DelAccelerate", 5f);      
    }

    private void DelAccelerate()
    {
        if(GetComponent<Player>().moveSpeed > 230)
        {
            GetComponent<Player>().moveSpeed -= 50;
        }
        if(GetComponent<Player>().rotateSpeed > 4)
        {
            GetComponent<Player>().rotateSpeed -= 2;
        }
        
    }

    private void Defend()
    {
        CreateBuffEffect(buffEffects[2], transform.position, transform.rotation, time);
        GetComponent<Player>().isInvincible = true;
        Invoke("DelDefend", time);
    }

    private void DelDefend()
    {
        GetComponent<Player>().isInvincible = false;
    }


    private void Stronger()
    {
        CreateBuffEffect(buffEffects[3], transform.position, transform.rotation, 1f);
        GameObject.FindWithTag("player").SendMessage("BossDamageAdd");
    }

    

    private void CreateBuffEffect(GameObject buffeffect,Vector3 createPos,Quaternion createRot,float time)
    {
        GameObject effect = Instantiate(buffeffect, transform.position, createRot);
        effect.transform.SetParent(gameObject.transform);
        Destroy(effect, time);
        
    }
}
