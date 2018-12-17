using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public float distanceAway;
    public float distanceUp; 
    public float smooth;               // how smooth the camera movement is

    private Vector3 m_TargetPosition;       // the position the camera is trying to be in)

    private Transform follow;        //the position of Player
    private GameObject soldier;
    //public Transform follow;

    void Start()
    {
        soldier = GameObject.FindWithTag("soldier");
        follow = soldier.transform;
    }

    void FixedUpdate()
    {
        // 设置相机位置
        m_TargetPosition = follow.position + Vector3.up * distanceUp - follow.forward * distanceAway;
        transform.position = Vector3.Lerp(transform.position, m_TargetPosition, Time.deltaTime * smooth);

        // make sure the camera is looking the right way!
        transform.LookAt(follow);
    }

}
