using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    // Method to start the game (load the game scene)
    public void StartGame()
    {
        Debug.Log("Game started!");
        // Replace "GameScene" with the name of your actual game scene
        SceneManager.LoadScene("Game");
    }

    // Method to exit the game
    public void ExitGame()
    {
        Debug.Log("Game exited.");
        Application.Quit();
    }
}
