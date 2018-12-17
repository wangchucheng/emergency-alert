using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public Button AddButtonOfVolume;
    public Button MinusButtonOfVolume;
    public Button AddButtonOfSoundEffect;
    public Button MinusButtonOfSoundEffect;
    public Slider VolumeSlider;
    public Slider SoundEffectSlider;

    // Use this for initialization

    void Start()
    {
        //为button的OnClick事件添加监听器，当监听到Click事件时，回调函数
        //AddButtonOfVolume.onClick.AddListener(AddOfVolume);
        //MinusButtonOfVolume.onClick.AddListener(MinusOfVolume);
        //AddButtonOfSoundEffect.onClick.AddListener(AddOfSoundEffect);
        //MinusButtonOfSoundEffect.onClick.AddListener(MinusOfSoundEffect);

    }

    // Update is called once per frame
    void Update()
    {

    }
    void AddOfVolume()
    {
        VolumeSlider.value += 0.1f;
    }

    void MinusOfVolume()
    {
        VolumeSlider.value -= 0.1f;
    }

    void AddOfSoundEffect()
    {
        SoundEffectSlider.value += 0.1f;
    }

    void MinusOfSoundEffect()
    {
        SoundEffectSlider.value -= 0.1f;
    }

}
