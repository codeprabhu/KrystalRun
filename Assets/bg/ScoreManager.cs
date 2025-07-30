using UnityEngine;
using TMPro; // Required for TextMeshPro

public class EndCreditsScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText; // Reference to the TextMeshProUGUI for displaying the final score

    void Start()
    {

        // Retrieve the final score from PlayerPrefs
        float finalScore = PlayerPrefs.GetFloat("FinalScore", 0);

        // Update the TextMeshProUGUI to display the final score
        finalScoreText.text = "Final Score: " + Mathf.FloorToInt(finalScore).ToString();
    }
}
