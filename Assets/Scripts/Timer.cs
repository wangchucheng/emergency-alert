using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public int totalSecond = 0;
    public float remainingTime;
    private float cd = 0;
    public GameObject minuteText;
    public GameObject secondText;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("TimeCutDown", totalSecond);
    }

    // Update is called once per frame

    IEnumerator TimeCutDown(float time)
    {
        while (time >= 0)
        {
            //Debug.Log(time);
            yield return new WaitForSeconds(1);
            time++;
            remainingTime = time;
            minuteText.GetComponent<Text>().text = "0" + (int)(time / 60) + ":";
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

    }


}
