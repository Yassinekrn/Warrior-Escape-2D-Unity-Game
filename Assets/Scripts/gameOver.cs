using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameOver : MonoBehaviour
{
    //public Text timeScore;
    public void Setup () // pass the argument: int score
    {
        gameObject.SetActive(true);
        //timeScore.text = "Score: " + score.ToString();
    }

    public void restartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void mainmenuButton()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
