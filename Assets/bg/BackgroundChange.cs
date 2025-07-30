using UnityEngine;
using UnityEngine.UI;

public class BackgroundChanger : MonoBehaviour
{
    // Array of background images
    public Sprite[] backgrounds;

    // Reference to the Image component of the background
    private Image backgroundImage;

    void Start()
    {
        // Get the Image component attached to this GameObject
        backgroundImage = GetComponent<Image>();

        // Change the background to a random image from the array
        ChangeBackground();
    }

    void ChangeBackground()
    {
        // Select a random index from the backgrounds array
        int index = Random.Range(0, backgrounds.Length);
        
        // Set the selected image as the background
        backgroundImage.sprite = backgrounds[index];
    }
}
