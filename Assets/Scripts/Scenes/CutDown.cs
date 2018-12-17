using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutDown : MonoBehaviour {
    public int minute = 5;
    public int totalSecond ;
    public float remainingTime;
    private float cd = 0;
    public GameObject minuteText;
    public GameObject secondText;
    public Scene curScene;
    
	// Use this for initialization
	void Start () {
        curScene = SceneManager.GetActiveScene();
        switch (curScene.buildIndex)
        {
            case 1:
                totalSecond = 120;
                break;
            case 2:
                totalSecond = 360;
                break;
        }
        StartCoroutine("TimeCutDown", totalSecond);
	}
	
	// Update is called once per frame

    IEnumerator TimeCutDown(float time)
    {
        while (time > 0)
        {
            //Debug.Log(time);
            yield return new WaitForSeconds(1);
            time--;
            remainingTime = time;
            minuteText.GetComponent<Text>().text = "0" + (int)(time / 60)+":";
            if (time/60<1)
            {
                minuteText.GetComponent<Text>().color = Color.red;
                secondText.GetComponent<Text>().color = Color.red;
            }
            if (time % 60 < 10)
            {
                secondText.GetComponent<Text>().text = "0" + (time % 60);
            }
            else
            {
                secondText.GetComponent<Text>().text = "" + (time % 60);
            }
            
        }
        minuteText.GetComponent<Text>().text = "00:";
        secondText.GetComponent<Text>().text = "00";
        switch (curScene.buildIndex)
        {
            case 1:
                SceneManager1.Instance.timeOut = true;
                break;
            case 2:
                SceneManager2.Instance.timeOut = true;
                break;
                //case 3:
                //    SceneManager3.Instance.timeOut = true;
                //    break;
        }

    }
    

}
