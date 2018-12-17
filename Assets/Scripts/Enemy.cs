using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Enemy : MonoBehaviour {
    public enum EnemyState
    {
        Patrol,    //巡逻
        Chase,      //追踪
        AttackPlayer,       //攻击玩家
        AttackHome      //攻击基地
    }

    public GameObject bullet;
    public GameObject bulletPos;            //用于确定炮弹生成位置
    public float stopDistance;              //停止距离、攻击范围
    public float maxDistance;               //逃逸距离，如果玩家与敌人距离超过这个值，则敌人放弃追踪玩家。
    public GameObject tankExplosion;

    private EnemyState currentState = EnemyState.Patrol;
    private Vector3 bornPoint;

    //移动速度在Nav Mesh Agent组件中设置

    private float attackCD = 2f;            //攻击CD

    private GameObject player;
    private GameObject home;
    private GameObject[] turret;
    private Transform target;
    private int SceneID = 1;
    float timeVal = 6f;

    UnityEngine.AI.NavMeshAgent nav;

    // Use this for initialization
    void Start() {
        player = GameObject.FindWithTag("player");
        //turret = GameObject.FindGameObjectsWithTag("turret");
        home = GameObject.FindWithTag("home");
        if (home == null)
            SceneID = 2;

        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        bornPoint = transform.position;
        //stopDistance = nav.stoppingDistance;

    }

    //敌人AI主要逻辑，敌人向我方基地前进，如果玩家进入攻击范围，则优先攻击玩家。
    //如果攻击范围内只有我方基地，则攻击基地。
    private void FixedUpdate()
    {
        if (SceneID == 1)
            EnemyAIDefend();
        else
            EnemyAIAttack();
       

    }

    public void EnemyAIDefend()
    {
        UnityEngine.AI.NavMeshHit hit;
        bool hasObstacle = nav.Raycast(player.transform.position, out hit);
        float distancePAE = Vector3.Distance(player.transform.position, transform.position);

        switch (currentState)
        {
            case EnemyState.Patrol:
                if (distancePAE < maxDistance)
                {
                    currentState = EnemyState.Chase;
                }
                else if (Vector3.Distance(home.transform.position, transform.position) < stopDistance)
                {
                    currentState = EnemyState.AttackHome;
                }
                else if (distancePAE < stopDistance  && !hasObstacle)
                {
                    currentState = EnemyState.AttackPlayer;
                }
                else
                {
                    nav.SetDestination(home.transform.position);
                }
                break;
            case EnemyState.Chase:
                if (distancePAE < stopDistance && !hasObstacle)
                {
                    currentState = EnemyState.AttackPlayer;
                }
                else if (distancePAE > maxDistance)
                {
                    currentState = EnemyState.Patrol;
                }
                else
                {
                    nav.SetDestination(player.transform.position);
                }
                break;
            case EnemyState.AttackPlayer:
                if (distancePAE > stopDistance || hasObstacle)
                {
                    currentState = EnemyState.Chase;
                    nav.isStopped = false;
                }
                else
                {
                    nav.isStopped = true;
                    transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        Quaternion.LookRotation(player.transform.position - transform.position),
                        3.5f * Time.fixedDeltaTime
                    );
                    Attack();
                }
                break;
            case EnemyState.AttackHome:
                nav.isStopped = true;
                transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        Quaternion.LookRotation(home.transform.position - transform.position),
                        3.5f * Time.fixedDeltaTime
                    );
                Attack();
                break;
        }
        //Debug.Log(Vector3.Distance(player.transform.position, transform.position));
        //if(hasObstacle)
        //{
        //    Debug.Log( hit.hit);
        //}
        //Debug.Log(currentState);
    }

    public void EnemyAIAttack()
    {
        UnityEngine.AI.NavMeshHit hit;
        bool hasObstacle = nav.Raycast(player.transform.position, out hit);
        float distancePAE = Vector3.Distance(player.transform.position, transform.position);

        switch (currentState)
        {
            case EnemyState.Patrol:
                if (distancePAE < maxDistance)
                {
                    currentState = EnemyState.Chase;
                }
                else if (distancePAE < stopDistance && !hasObstacle)
                {
                    currentState = EnemyState.AttackPlayer;
                }
                else
                {
                    if (timeVal >= 6f)
                    {
                        timeVal = 0;
                        float x = Random.Range(-120, 120);
                        float z = Random.Range(-120, 120);
                        nav.SetDestination(this.bornPoint + new Vector3(x, 0, z));
                    }
                    else
                    {
                        timeVal += Time.fixedDeltaTime;
                    }
                }
                break;
            case EnemyState.Chase:
                if (distancePAE < stopDistance && !hasObstacle)
                {
                    currentState = EnemyState.AttackPlayer;
                }
                else if (distancePAE > maxDistance)
                {
                    currentState = EnemyState.Patrol;
                }
                else
                {
                    nav.SetDestination(player.transform.position);
                }
                break;
            case EnemyState.AttackPlayer:
                if (distancePAE > stopDistance || hasObstacle)
                {
                    currentState = EnemyState.Chase;
                    nav.isStopped = false;
                }
                else
                {
                    nav.isStopped = true;
                    transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        Quaternion.LookRotation(player.transform.position - transform.position),
                        3.5f * Time.fixedDeltaTime
                    );
                    Attack();
                }
                break;
        }
    }

    //TODO：攻击策略优化
    public void Attack()
    {
        if(attackCD >= 2f)
        {
            SoundManager.Instance.PlaySound("开火");
            Instantiate(bullet, bulletPos.transform.position, bulletPos.transform.rotation);
            attackCD = 0;
        }
        else
        {
            attackCD += Time.fixedDeltaTime;
        }
    }
    

    //TODO：特效、音效
    public void Die()
    {
        Scene curScene = SceneManager.GetActiveScene();
        switch (curScene.buildIndex)
        {
            case 1:
                SceneManager1.NumDecrease();
                break;
            case 2:
                SceneManager2.NumDecrease();
                break;
            case 3:
                SceneManager3.NumDecrease();
                break;
        }
        SoundManager.Instance.PlaySound("TankExplosion");
        Instantiate(tankExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
