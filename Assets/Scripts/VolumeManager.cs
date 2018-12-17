using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour {
    public static VolumeManager Instance;
    public AudioSource MusicPlayer;

    public Slider volumeSlider;

    public static float musicvol = 0.2f;


    // Use this for initialization
    void Awake()
    {
        Instance = this;
        MusicPlayer = GetComponent<AudioSource>();

        volumeSlider =volumeSlider.GetComponent<Slider>();

        volumeSlider.value = musicvol;
    }

    private void Update()
    {
        musicvol = volumeSlider.value;
        MusicPlayer.volume = volumeSlider.value;
        
    }
    //音乐播放
    public void PlayMusic(string name)
    {
        Debug.Log(name);
        if (MusicPlayer.isPlaying == false)
        {
            AudioClip clip = Resources.Load<AudioClip>(name);
            MusicPlayer.clip = clip;
            MusicPlayer.Play();
        }
    }
    //音乐停止
    public void StopMusic()
    {
        MusicPlayer.Stop();
    }
    
}
