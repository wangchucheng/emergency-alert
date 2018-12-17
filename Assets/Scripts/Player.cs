using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public float Hp;
    public float startHp=150;
    public bool isInvincible;
    public float moveSpeed;             //坦克移动速度
    public float rotateSpeed;           //坦克旋转速度
    public float damage;
    public float toBossDamage=30;           //对boss造成伤害
    public float Bossdamage=10;       //boss造成的伤害
    private float attackCD = 2f;        //坦克攻击CD
    

    public GameObject bullet;
    public GameObject bulletPos;        //用于确定炮弹生成位置
    public GameObject fire;
    public GameObject dust1;
    public GameObject dust2;
    public GameObject flag;
    public GameObject tankExplosion;

    Rigidbody playerRB;
    private Animator animator;
    public AnimationClip idle;
    public AnimationClip run;

    // Use this for initialization
    void Start () {
        Hp = startHp;
        playerRB = GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
    }
	
	//每帧间隔时间相同
    private void FixedUpdate()
    {
        
        Move();
        Attack();
       
    }

    //坦克移动方式，WASD 或 上下左右。
    private void Move()
    {
        float v = Input.GetAxis("Vertical");
        //transform.Translate(Vector3.forward * v * moveSpeed * Time.fixedDeltaTime);

        float h = Input.GetAxis("Horizontal");
        //transform.Rotate(Vector3.up * h * rotateSpeed * Time.fixedDeltaTime);
        playerRB.velocity = transform.forward * moveSpeed * v;
        if(v>=0)
            playerRB.angularVelocity = transform.up * rotateSpeed * h;
        else
            playerRB.angularVelocity = -transform.up * rotateSpeed * h;

        if (v != 0 || h != 0)
        {
            dust1.SetActive(true);
            dust2.SetActive(true);
            animator.SetFloat("Speed", (float)1.0);
        }
        else
        {
            dust1.SetActive(false);
            dust2.SetActive(false);
            animator.SetFloat("Speed", 0);
        }
    }

    //坦克攻击方式，按空格发射炮弹
    private void Attack()
    {
        if(attackCD >= 1f)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SoundManager.Instance.PlaySound("开火");
                Instantiate(bullet, bulletPos.transform.position, bulletPos.transform.rotation);
                Instantiate(fire, bulletPos.transform.position, bulletPos.transform.rotation);
                attackCD = 0;
            }
        }
        else
        {
            attackCD += Time.fixedDeltaTime;
        }
    }

    //坦克销毁
    //TODO: 爆炸特效和音效
    private void Die()
    {
        Hp = 0;
        Scene curScene = SceneManager.GetActiveScene();
        switch (curScene.buildIndex)
        {
            case 1:
                SceneManager1.Instance.isDead = true;
                break;
            case 2:
                SceneManager2.Instance.isDead = true;
                break;
            case 3:
                SceneManager3.Instance.isDead = true;
                break;
        }
        //Instantiate(tankExplosion);
        //this.gameObject.SetActive(false);
        //Destroy(gameObject,1f);

    }

    //TODO：坦克收到伤害后，应该减少生命值。
    private void Damage()
    {
        if (isInvincible)
        {
            return;
        }
        if (Hp-damage<=0)
        {
            Die();
        }
        else
        {
            Hp -= damage;
        }
    }

    private void BossDamage()
    {
        if (isInvincible)
        {
            return;
        }
        if (Hp - Bossdamage <= 0)
        {
            Die();
        }
        else
        {
            Hp -= Bossdamage;
        }
    }

    private void BossDamageAdd()
    {
        toBossDamage += 2;
    }
}
