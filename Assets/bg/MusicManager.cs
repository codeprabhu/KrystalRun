using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip musicClip;  // Drag your music clip here in the Inspector
    private AudioSource audioSource;

    private static MusicManager instance;

    void Awake()
    {
        // Ensure there is only one instance of MusicManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Prevent this GameObject from being destroyed when loading a new scene
        }
        else
        {
            // Destroy duplicate MusicManager objects if they exist
            Destroy(gameObject);
        }

        // Find or add the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // If there's no AudioSource, add one
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set up the AudioSource
        audioSource.clip = musicClip;  // Assign the music clip
        audioSource.loop = true;       // Set to loop indefinitely
        audioSource.Play();            // Start playing the music if it's not already
    }
}
