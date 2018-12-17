using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuCanvas : MonoBehaviour {

    public GameObject PauseButton;
    public GameObject PauseMenu;

    public Slider VolumeSlider;
    public Slider SoundEffectSlider;
    public Text Message;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //暂停
    public void Pause()
    {
        Time.timeScale = 0;
    }

    //继续
    public void Resume()
    {
        Time.timeScale = 1;
    }

    //返回主菜单
    public void BacktoMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    //点击继续按钮
    public void OnClickResume()
    {
        PauseMenu.SetActive(false);
        PauseButton.SetActive(true);
        Resume();
    }

    //增加音量
    public void AddVolume()
    {
        VolumeSlider.value += 0.1f;
    }

    //减小音量
    public void MinusVolume()
    {
        VolumeSlider.value -= 0.1f;
    }

    //增加音效
    public void AddSoundEffect()
    {
        SoundEffectSlider.value += 0.1f;
    }

    //减小音效
    public void MinusSoundEffect()
    {
        SoundEffectSlider.value -= 0.1f;
    }

    private void ShowMessage(string str)
    {
        Message.text = str;
    }

    public void CleanMessage()
    {
        Message.text = "";
    }
}
