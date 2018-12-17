using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    public enum BossState
    {
        Patrol,    //巡逻
        Chase,      //追踪
        AttackPlayer, 
    }

    public float Hp;
    public float startHp;
    public float damage=30;

    public AnimationClip chasisIdle;
    public AnimationClip chasisRun;
    public GameObject gunFireObject1;
    public GameObject gunFireObject2;
    private Animation ani;
    public GameObject target;
    public GameObject rocket;
    public GameObject rocket_f;
    public GameObject firePosition;

    private BossState currentState = BossState.Patrol;
    private Vector3 bornPoint;
    UnityEngine.AI.NavMeshAgent nav;
    private GameObject player;
    public float stopDistance;              //停止距离、攻击范围
    public float maxDistance;               //逃逸距离，如果玩家与敌人距离超过这个值，则敌人放弃追踪玩家。
    public float attackCD ;
    public float skillCD ;
    private Rigidbody rb;

    // Use this for initialization
   
    void Start ()  {
        Hp = startHp; 
        player = GameObject.FindWithTag("player");
        gunFireObject1.SetActive(false);
        gunFireObject2.SetActive(false);
        ani = GetComponent<Animation>();
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        bornPoint = this.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        BossAI();
        damage = GameObject.FindWithTag("player").GetComponent<Player>().toBossDamage;
    }

    public void BossAI()
    {
        UnityEngine.AI.NavMeshHit hit;
        bool hasObstacle = nav.Raycast(player.transform.position, out hit);
        //Debug.Log(hit.ToString());
        float distancePAE = Vector3.Distance(player.transform.position, transform.position);

        switch (currentState)
        {
            case BossState.Patrol:
                if (distancePAE < maxDistance)
                {
                    currentState = BossState.Chase;
                }
                else if (distancePAE < stopDistance && !hasObstacle)
                {
                    currentState = BossState.AttackPlayer;
                }
                else
                {
                    nav.SetDestination(this.bornPoint);
                    if (Vector3.Distance(transform.position,bornPoint) <=2f)
                    {
                        ani.Play(chasisIdle.name);
                    }
                    else
                    {
                        ani.Play(chasisRun.name);
                    }

                }
                break;
            case BossState.Chase:
                if (distancePAE < stopDistance && !hasObstacle)
                {
                    currentState = BossState.AttackPlayer;
                }
                else if (distancePAE > maxDistance)
                {
                    currentState = BossState.Patrol;
                }
                else
                {
                    RemoteAttack();
                    ani.Play(chasisRun.name);
                    nav.SetDestination(player.transform.position);
                }
                break;
            case BossState.AttackPlayer:
                if (distancePAE > stopDistance || hasObstacle)
                {
                    currentState = BossState.Chase;
                    nav.isStopped = false;
                    gunFireObject1.SetActive(false);
                    gunFireObject2.SetActive(false);
                }
                else
                {
                    RemoteAttack();
                    nav.isStopped = true;
                    nav.velocity = new Vector3(0, 0, 0);
                    //计算夹角，判断是否在扇形攻击范围内
                    Vector3 dirVec = player.transform.position - transform.position;
                    Vector3 norVec = transform.rotation * Vector3.forward;
                    float angle = Mathf.Acos(Vector3.Dot(norVec.normalized, dirVec.normalized)) * Mathf.Rad2Deg;
                    if(angle < 30f)
                    {
                        ani.Play(chasisIdle.name);
                        Attack();
                    }
                    else
                    {
                        ani.Play(chasisRun.name);
                    }
                    transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        Quaternion.LookRotation(player.transform.position - transform.position),
                        3.5f * Time.fixedDeltaTime
                    );
                }
                break;
        }
    }

    public void Attack()
    {
        gunFireObject1.SetActive(true);
        gunFireObject2.SetActive(true);
        if(attackCD >= 0.2f)
        {
            SoundManager.Instance.PlaySound("BossFire");
            player.SendMessage("BossDamage");
            attackCD = 0;
        }
        else
        {
            attackCD += Time.fixedDeltaTime;
        }
    }
    //坦克销毁
    //TODO: 胜利动画
    private void Die()
    {
        SoundManager.Instance.PlaySound("BossDie");
        Hp = 0;
        SceneManager3.Instance.bossDead = true;
        Destroy(gameObject);

    }

    private void Damage()
    {
        if (Hp - damage <= 0)
        {
            Die();
        }
        else
        {
            Hp -= damage;
        }
    }

    private void RemoteAttack()
    {
        if(skillCD>=5)
        {
            Vector3[] Positions = new Vector3[3];
            for(int i = 0;i<3;i++)
            {
                Positions[i] = player.transform.position;
            }
            Positions[0].z -= 14f;
            Positions[1].z += 14.4f;
            Positions[1].x += 14.4f;
            Positions[2].z += 14.4f;
            Positions[2].x -= 14.4f;
            GameObject[] Targets = new GameObject[3];
            for (int i = 0; i < 3; i++)
            {
                Targets[i] = Instantiate(target, Positions[i], player.transform.rotation);
                Destroy(Targets[i], 2f);
                Positions[i].y += 200;
                Instantiate(rocket, Positions[i], Quaternion.Euler(90, 180, 0));
            }
            GameObject Rocket_f = Instantiate(rocket_f, firePosition.transform.position, firePosition.transform.rotation);
            Destroy(Rocket_f, 1f);
            skillCD = 0;
        }
        else
        {
            skillCD += Time.fixedDeltaTime;
        }
        
    }

    
}
