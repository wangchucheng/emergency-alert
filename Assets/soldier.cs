using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class soldier : MonoBehaviour {
    public enum SoldierState
    {
        Finding,    //找石头
        Placing,      //放置石头
    }
    int i = 0;
    private Vector3 bornPoint;
    public AnimationClip Run;
    public AnimationClip Dead_01;
    private Animation ani;
    private NavMeshAgent nav;
    private GameObject stone;
    private GameObject stone2;
    private Vector3[] place = new Vector3[13];
    private Vector3 placetoPut;
    bool ifChangedestination = true;
    float placez = -319f;
    private SoldierState currentState = SoldierState.Finding;
    public Transform target;
    private float lifetime = 5f;




    private void Start()
    {
        stone = GameObject.FindWithTag("stone");                          //tag改为stone
        stone2 = GameObject.FindWithTag("stone2");
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        ani = GetComponent<Animation>();
        bornPoint = target.position;

        //放石头的位置
        for(int i = 0; i < 14; i++)
        {
            placez += 2.5f;
            place[i] = new Vector3(-500f, 0f, placez);
        }
    }

    void Update()
    {
        /*lifetime -= 1;
        if (lifetime <= 0)                          //销毁，爆炸
        {
            Die();
        }*/
        if (currentState == SoldierState.Finding)
            FindStone();
        else if (currentState == SoldierState.Placing)
        {
            Debug.Log("else if placing");
            placeStone();
        }
            
    }

    public void FindStone()
    {
        ifChangedestination = true;
        NavMeshHit hit;
        bool hasObstacle = nav.Raycast(stone.transform.position, out hit);
        ani.Play(Run.name);
        nav.SetDestination(stone.transform.position);
        //ifChangedestination = true;
        if (Vector3.Distance(this.transform.position, stone.transform.position) < 5)       //如果达到目标地点
        {
            //PickUp();                 //找到石头，场景中石头消失
            Debug.Log("Has Found");
            currentState = SoldierState.Placing;        //放置
            
        }
    }
    public void placeStone()
    {
        if (ifChangedestination)            //是否已到达设定的目标地点
        {
            Debug.Log("change destination");
            placetoPut = place[i];
            i++;
            ifChangedestination = false;        //直到走到该目标放完石头后，才重新设定新的放置位置
        }
        NavMeshHit hit;
        bool hasObstacle = nav.Raycast(placetoPut, out hit);
        ani.Play(Run.name);
        nav.SetDestination(placetoPut);
        
        if (Vector3.Distance(this.transform.position, placetoPut) < 5)       //如果达到目标地点
        {
            Debug.Log("find place to put down");
            //PickUp();                 //找到石头，场景中石头消失
            ifChangedestination = true;
            currentState = SoldierState.Finding;        //放置
           
        }
        /*NavMeshHit hit;
        bool hasObstacle = nav.Raycast(stone2.transform.position, out hit);
        ani.Play(Run.name);
        nav.SetDestination(stone2.transform.position);*/
    }
}
