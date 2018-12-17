using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinImageController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //返回主菜单
    public void BacktoMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    //继续下一关
    public void PlayNext()
    {
        Time.timeScale = 1;
        Scene curScene = SceneManager.GetActiveScene();
        Loading.loadingScene = curScene.buildIndex+1;
        SceneManager.LoadScene(4);
    }
}
