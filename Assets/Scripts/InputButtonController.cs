using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class InputButtonController : MonoBehaviour {

    public Text inputText;

    public static string playerName;

    public void OnClickInputButton()
    {
        playerName = inputText.text;
        Ranking ranking = new Ranking();
        ranking.AddRanking();
        ranking.LoadRanking();
        ranking.SaveRanking();
        SceneManager.LoadScene(0);
    }
}
