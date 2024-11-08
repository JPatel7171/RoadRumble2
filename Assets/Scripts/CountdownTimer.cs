using UnityEngine;
using UnityEngine.UI; // Required for UI elements
using TMPro; // Add this if you're using TextMeshPro

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 60f; // Set the countdown time in seconds
    private float currentTime;
    public Text countdownText; // Reference to the UI Text element

    void Start()
    {
        currentTime = countdownTime; // Initialize the current time
    }

    void Update()
    {
        // Decrease the current time
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime; // Decrease time by the time passed since the last frame
            UpdateTimerDisplay();
        }
        else
        {
            // Timer has finished
            currentTime = 0;
            TimerFinished();
        }
    }

    void UpdateTimerDisplay()
    {
        // Update the UI Text with the remaining time
        countdownText.text = Mathf.Ceil(currentTime).ToString(); // Display as whole number
    }

    void TimerFinished()
    {
        // Actions to take when the timer finishes
        countdownText.text = "Time's Up!";
        // You can add more actions here, like stopping the game or triggering an event
    }
}