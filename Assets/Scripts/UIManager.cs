using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text pointsText; // Reference to the UI Text component
    private PointCollector pointCollector;

    void Start()
    {
        pointCollector = FindObjectOfType<PointCollector>();
        UpdatePointsText();
    }

    void Update()
    {
        UpdatePointsText();
    }

    void UpdatePointsText()
    {
        pointsText.text = "Points: " + pointCollector.points;
    }
}