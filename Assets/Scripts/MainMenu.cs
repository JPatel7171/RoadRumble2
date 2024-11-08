using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management
using UnityEngine.UI; // Required for UI elements

public class MainMenu : MonoBehaviour
{
    // Method to start the game
    public void StartGame()
    {
        // Load the game scene (replace "GameScene" with the name of your game scene)
        SceneManager.LoadScene("Main Scene");
    }

    // Method to open options (you can implement this later)
    public void OpenOptions()
    {
        Debug.Log("Options button clicked");
        // Here you can implement the logic to open the options menu
    }

    // Method to exit the game
    public void ExitGame()
    {
        Debug.Log("Exit button clicked");
        Application.Quit(); // This will close the application
    }
}