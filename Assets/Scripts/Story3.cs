using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story3 : MonoBehaviour {
    public static bool storyEnd = false;
    public static bool restart = false;
    public static bool reload = false;
    public GameObject Camera;

    // Use this for initialization
    void Start()
    {
        if (reload)
        {

            storyEnd = true;
            Invoke("Reload", 0.03f);
            return;
        }
        if (restart)
        {
            StoryEnd();
            restart = false;
            return;
        }
        storyEnd = false;
        Invoke("StoryEnd", 32f);
    }


    private void StoryEnd()
    {
        storyEnd = true;
        Destroy(gameObject);
    }


    public void SkipStory()
    {
        storyEnd = true;
        gameObject.SetActive(false);
    }

    public void Reload()
    {
        reload = false;

        gameObject.SetActive(false);
        Camera.GetComponent<SceneManager3>().LoadByJson();
    }
}
