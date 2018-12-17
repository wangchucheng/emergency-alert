using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {
    public Slider loadingSlider;
    public static int loadingScene;

    private AsyncOperation Async;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(loadScene());
    }

    // Update is called once per frame
    void Update()
    {
        if (Async != null)
        {
            if (!Async.isDone)
            {
                if (loadingSlider.value <= 0.9f)
                {
                    if (loadingSlider.value <= Async.progress)
                    {
                        loadingSlider.value += Time.deltaTime;
                    }
                }
                else
                {
                    if (loadingSlider.value < 1)
                    {
                        loadingSlider.value += Time.deltaTime;
                    }
                    else
                    {
                        Async.allowSceneActivation = true;
                    }
                }
            }
        }
    }


    private IEnumerator loadScene()
    {
        Async = SceneManager.LoadSceneAsync(loadingScene);
        Async.allowSceneActivation = false;
        yield return Async;
    }
}
