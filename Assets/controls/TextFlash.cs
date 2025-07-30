using UnityEngine;
using TMPro;
using System.Collections;

public class TMPFlashAndFade : MonoBehaviour
{
    public TMP_Text flashText; // Assign your TextMeshPro object here
    public float flashDuration = 3f; // Duration of flashing
    public float fadeDuration = 2f; // Duration of fade-out
    private bool isPlayerLocked = true; // Prevent player actions during the effect

    void Start()
    {
        if (flashText != null)
        {
            flashText.alpha = 0f; // Ensure text starts invisible
            StartCoroutine(FlashAndFade());
        }
    } 

    private IEnumerator FlashAndFade()
    {
        float elapsedTime = 0f;

        // Flashing phase
        while (elapsedTime < flashDuration)
        {
            // Alternate the alpha value to create a flashing effect
            flashText.alpha = Mathf.PingPong(Time.time * 2f, 1f); // Adjust speed with multiplier (2f here)
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        flashText.alpha = 1f; // Ensure it's fully visible before fading

        // Fade-out phase
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            // Gradually reduce the alpha value for fading
            flashText.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        flashText.alpha = 0f; // Ensure text is fully invisible
        isPlayerLocked = false; // Unlock player actions
    }

    void Update()
    {
        if (isPlayerLocked)
        {
            // Prevent player actions here
            // Example: Disable movement or other input
            return;
        }

        // Allow player actions after the effect ends
        // Example: Handle player input here
    }
}