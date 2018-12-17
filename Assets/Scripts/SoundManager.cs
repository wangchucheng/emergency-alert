using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance;
    public AudioSource SoundPlayer;
    public Slider soundSlider;
    public static float soundvol = 0.7f;

    // Use this for initialization
    void Awake () {
        Instance = this;
        SoundPlayer = GetComponent<AudioSource>();
        soundSlider = soundSlider.GetComponent<Slider>();
        soundSlider.value = soundvol;
    }
	
	// Update is called once per frame
	void Update () {
        soundvol = soundSlider.value;
        SoundPlayer.volume = soundSlider.value;
        Debug.Log(SoundPlayer.volume);
    }
    //音效播放
    public void PlaySound(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>(name);
        SoundPlayer.PlayOneShot(clip);
    }
}
