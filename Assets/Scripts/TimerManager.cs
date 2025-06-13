using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    public float timeLimit = 60f;
    public TMP_Text timerText;

    private float timeRemaining;
    private bool timerRunning = false;

    void Start()
    {
        timeRemaining = timeLimit;
        UpdateTimerDisplay(timeRemaining);
    }

    void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                timerRunning = false;
                timeRemaining = 0;
                UpdateTimerDisplay(0);
                OnTimerEnd();
            }
        }
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        timeToDisplay = Mathf.Max(0, timeToDisplay);
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnTimerEnd()
    {
        Debug.Log("Time's up!");
        SceneManager.LoadScene("FinalRoom");
    }

    // 🔁 Call this method from your trigger to start the timer
    public void StartTimer()
    {
        timeRemaining = timeLimit;
        timerRunning = true;
    }

    public void StopTimer()
    {
        
        timerRunning = false;
    }
}
