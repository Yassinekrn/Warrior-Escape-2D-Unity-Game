using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayScript : MonoBehaviour
{
    public Text scoreText;
    public Text highestScoreText;
    private float timerValue;

    public void DisplayScore(float timerValue)
    {
        this.timerValue = timerValue;
        string formattedTime = FormatTime(timerValue);
        scoreText.text = formattedTime;
        UpdateHighestScore();
    }

    private void UpdateHighestScore()
    {
        float highestScore = PlayerPrefs.GetFloat("HighestScore", 0f);
        if (timerValue > highestScore)
        {
            highestScore = timerValue;
            PlayerPrefs.SetFloat("HighestScore", highestScore);
        }
        highestScoreText.text = FormatTime(highestScore);
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        return formattedTime;
    }
}
