using UnityEngine;

public class PointCollector : MonoBehaviour
{
    public int points = 0; // Total points collected
    public AudioClip collectSound; // Sound to play when collecting a point

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is a collectible
        if (other.CompareTag("Collectible"))
        {
            // Increment points
            points += 1;

            // Optionally play a sound
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            // Destroy the collectible
            Destroy(other.gameObject);

            // Optionally, update the UI or log the points
            Debug.Log("Points: " + points);
        }
    }
}