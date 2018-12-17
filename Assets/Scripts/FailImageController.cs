using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailImageController : MonoBehaviour {

    public GameObject FailImage;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //重新开始
    public void PlayAgain()
    {
        Time.timeScale = 1;
        Scene curScene = SceneManager.GetActiveScene();
        Loading.loadingScene = curScene.buildIndex;
        if(curScene.buildIndex == 5)
        {
            SceneManager.LoadScene(6);
            return;
        }
        switch (curScene.buildIndex)
        {
            case 1:
                Story1.restart = true;
                break;
            case 2:
                Story2.restart = true;
                break;
            case 3:
                Story3.restart = true;
                break;
        }
        SceneManager.LoadScene(4);
       
    }

    //返回主菜单
    public void BacktoMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
