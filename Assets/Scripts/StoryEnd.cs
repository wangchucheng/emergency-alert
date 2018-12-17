using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryEnd : MonoBehaviour {
    public GameObject Mark;


    void Start()
    {
        Invoke("SkipStory", 5f);
    }


    public void SkipStory()
    {
        //SceneManager.LoadScene(0);
        Mark.SetActive(true);
    }

    
}
