using UnityEngine;
using UnityEngine.UI;

public class Timerlogic : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private bool isTimerRunning = true;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            float timeElapsed = Time.time - startTime;
            int minutes = Mathf.FloorToInt(timeElapsed / 60f);
            int seconds = Mathf.FloorToInt(timeElapsed % 60f);
            string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText.text = timeString;
        }
    }

    public float StopTimer()
    {
        isTimerRunning = false;
        return Time.time - startTime;
    }
}
