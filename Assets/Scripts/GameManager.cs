using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text lapTimeText;
    public Text speedText;
    private float lapTime;
    private bool isRacing;
    public Rigidbody rb;  // Assign this in the inspector for your car's Rigidbody

    public float acceleration = 500f;
    public float steering = 2f;

    void Start()
    {
        lapTime = 0f;
        isRacing = true;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isRacing)
        {
            lapTime += Time.deltaTime;
            lapTimeText.text = "Lap Time: " + lapTime.ToString("F2") + "s";
        }

        // WASD movement
        float moveInput = Input.GetAxis("Vertical");  // W and S keys
        float steerInput = Input.GetAxis("Horizontal");  // A and D keys

        Vector3 forwardMove = transform.forward * moveInput * acceleration * Time.deltaTime;
        rb.AddForce(forwardMove, ForceMode.Acceleration);

        // Steering the car
        rb.transform.Rotate(Vector3.up * steerInput * steering);

        // Update speed in GameManager
        if (rb != null)
        {
            UpdateSpeed(rb.linearVelocity.magnitude * 3.6f); // Converts to km/h

        }
    }

    public void UpdateSpeed(float speed)
    {
        speedText.text = "Speed: " + speed.ToString("F2") + " km/h";
    }

    public void EndRace()
    {
        isRacing = false;
        // Additional logic for ending the race can be added here
    }
}