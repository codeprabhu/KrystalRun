using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Function to load the start menu
    public void LoadStartMenu()
    {
        SceneManager.LoadScene("startcreds");
    }
}
