using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {
    public GameObject mainMenu;
    public GameObject levelMenu;
    public GameObject tank;
    public GameObject aboutUs;
    public GameObject setting;
    public GameObject help;
    public GameObject ranking;
    private void Start()
    {
        VolumeManager.Instance.PlayMusic("开始界面");
    }

    public void PlayOnLocal()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayWithNet()
    {
        return;
    }

    public void ToSelect()
    {
        GetComponent<Animator>().SetBool("isSelect", true);
        GetComponent<Animator>().SetBool("finishSelect", false);
        mainMenu.SetActive(false);
        Invoke("WakeTheLevel", 1.3f);
    }

    

    public void FinishSelect()
    {
        GetComponent<Animator>().SetBool("finishSelect", true);
        levelMenu.SetActive(false);
        Invoke("WakeTheMenu", 1.2f);
        tank.transform.Rotate(Vector3.up, -97.762f);
        tank.SetActive(false);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void WakeTheMenu()
    {
        mainMenu.SetActive(true);
    }

    private void WakeTheLevel()
    {
        tank.SetActive(true);
        levelMenu.SetActive(true);
    }

    //关于我们
    public void AboutUs()
    {
        aboutUs.SetActive(true);
    }

    public void OpenSetting()
    {
        setting.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CloseSetting()
    {
        setting.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OpenHelp()
    {
        help.SetActive(true);
    }

    public void CloseHelp()
    {
        help.SetActive(false);
    }

    //public void OpenRank()
    //{
    //    Ranking.LoadRanking();
    //    ranking.GetComponent<Ranking>().AddToPrefab();
    //    ranking.SetActive(true);
    //}

    public void CloseRank()
    {
        ranking.SetActive(false);
    }

    public void PVP()
    {
        SceneManager.LoadScene(6);
    }
}
