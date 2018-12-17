using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AwareUIInPoint_2 : MonoBehaviour {

    private int eachCount = 1;         //吃掉一个enemyheart加1
    public GameObject t;
    public int awareCount = 0;

    private void Start()
    {
        t.GetComponent<Text>().text =""+ awareCount;
    }
    private void Update()
    {
        t.GetComponent<Text>().text = "" + awareCount;
    }

    private void AddCount()
    {
        awareCount += eachCount;
        if (awareCount >= 3)
        {
            //胜利
            SceneManager2.Instance.getPoints = true;
        }
    }
}
