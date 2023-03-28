using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void exitGame ()
    {
        Application.Quit ();
    }

    public void startGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
