using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SkyBox : MonoBehaviour {
    public Material[] skyMaterial;
    public Light light;
    private float timer;
    public GameObject lightMove;

    private Skybox sky;
    private float LightInten = 1.3f;
    private float change;
	// Use this for initialization
	void Start () {
        timer = 0f;
        sky = transform.GetComponent<Skybox>();
        Scene curScene = SceneManager.GetActiveScene();
        if (curScene.buildIndex == 2)
        {
            lightMove.transform.rotation = Quaternion.Euler(new Vector3(30, -90, 0));
            change = 0.013f;
        }
        else if(curScene.buildIndex == 3)
        {
            lightMove.transform.rotation = Quaternion.Euler(new Vector3(30, -30, 0));
            change = 0.013f;
        }
        else
        {
            lightMove.transform.rotation = Quaternion.Euler(new Vector3(150, 20, 0));
            change = -0.013f;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        timer += (Time.deltaTime/10);

        if (timer >= 0 && timer < 3)
        {
            RenderSettings.skybox = skyMaterial[1];
            light.intensity = LightInten-0.2f;
            lightMove.transform.Rotate(new Vector3(change, 0, 0));
        }
        if (timer >= 3 && timer < 6)
        {
            RenderSettings.skybox = skyMaterial[0];
            light.intensity = LightInten-0.15f;
            lightMove.transform.Rotate(new Vector3(change, 0, 0));
        }
        if (timer >= 6 && timer < 9)
        {
            RenderSettings.skybox = skyMaterial[2];
            light.intensity = LightInten-0.1f;
            lightMove.transform.Rotate(new Vector3(change, 0, 0));
        }
        if (timer >= 9 && timer < 12)
        {
            RenderSettings.skybox = skyMaterial[0];
            light.intensity = LightInten;
            lightMove.transform.Rotate(new Vector3(change, 0, 0));
        }
        if (timer >= 12 && timer < 15)
        {
            RenderSettings.skybox = skyMaterial[1];
            light.intensity = LightInten;
            lightMove.transform.Rotate(new Vector3(change, 0, 0));
        }
        if (timer >= 15 && timer < 18)
        {
            RenderSettings.skybox = skyMaterial[4];
            light.intensity = LightInten - 0.1f;
            lightMove.transform.Rotate(new Vector3(change, 0, 0));
        }
        if (timer >= 18 && timer < 21)
        {
            RenderSettings.skybox = skyMaterial[5];
            light.intensity = LightInten - 0.2f;
            lightMove.transform.Rotate(new Vector3(change, 0, 0));
        }
        if (timer >= 21 && timer < 24)
        {
            RenderSettings.skybox = skyMaterial[6];
            light.intensity = LightInten - 0.3f;
            lightMove.transform.Rotate(new Vector3(change, 0, 0));
        }
        if (timer >= 24 )
        {
            timer = 0;
            if(SceneManager.GetActiveScene().buildIndex == 2)
            {
                lightMove.transform.rotation = Quaternion.Euler(new Vector3(30, -90, 0));
            }
            else
            {
                lightMove.transform.rotation = Quaternion.Euler(new Vector3(150, -30, 0));
            }
            
        }
        
    }
}
