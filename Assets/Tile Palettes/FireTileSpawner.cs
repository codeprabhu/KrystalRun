using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Required for TextMeshPro

public class FireSpriteSpawner : MonoBehaviour
{
    public GameObject fireTilePrefab; // The prefab for fire tile (should have SpriteRenderer attached)
    public Transform playerTransform; // Reference to the player or camera
    public float fireTileSpawnDistance = 10f; // How far ahead of the player to spawn fire tiles
    public float fireTileDestroyDistance = 5f; // Distance after which to destroy the fire tiles
    public int fireTileRows = 13; // The number of rows to spawn fire tiles on
    public float minXBound = -5.995f; // Minimum X position for spawning fire tiles
    public float maxXBound = 6.995f; // Maximum X position for spawning fire tiles
    public string endCreditsSceneName = "EndCredits"; // Name of the end credits scene
    public TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI for displaying the score

    private List<GameObject> spawnedFireTiles = new List<GameObject>();
    private Vector3 lastPlayerPosition;
    private float score = 0; // Tracks the distance traveled

    void Start()
    {
        lastPlayerPosition = playerTransform.position;
        SpawnFireTilesInFront();

        if (scoreText == null)
        {
            Debug.LogError("Score TextMeshProUGUI reference is missing!");
        }
    }

    void Update()
    {
        // Update the score based on the player's position
        UpdateScore();

        // Check if the player has moved enough to generate new fire tiles
        if (Mathf.Abs(playerTransform.position.y - lastPlayerPosition.y) > fireTileSpawnDistance)
        {
            lastPlayerPosition = playerTransform.position;
            SpawnFireTilesInFront();
        }

        // Remove fire tiles that are too far behind
        RemoveDistantFireTiles();
    }

    void SpawnFireTilesInFront()
    {
        // Calculate the position where new fire tiles should spawn
        Vector3 spawnPosition = playerTransform.position + Vector3.up * fireTileSpawnDistance;

        // Spawn fire tiles along a line in front of the player
        for (int row = 0; row < fireTileRows; row++)
        {
            // Generate random X within the specified bounds
            float randomX = Random.Range(minXBound, maxXBound);

            // Fire tile position considering the row and random X within bounds
            Vector3 fireTilePosition = spawnPosition + new Vector3(randomX, row * 2, 0);

            // Create a new GameObject for the fire tile
            GameObject fireTileObject = Instantiate(fireTilePrefab, fireTilePosition, Quaternion.identity);

            // Add a BoxCollider2D to detect collisions with the player
            BoxCollider2D boxCollider = fireTileObject.AddComponent<BoxCollider2D>();
            boxCollider.isTrigger = false; // Disable trigger to allow collisions

            // Ensure the fire tile is at the correct Z-position for rendering
            fireTileObject.transform.position = new Vector3(fireTilePosition.x, fireTilePosition.y, 0); // Z=0 for same level as background

            // Add the newly spawned fire tile to the list of active fire tiles
            spawnedFireTiles.Add(fireTileObject);

            // Attach a collision detection method
            fireTileObject.AddComponent<FireTileCollision>().OnFireTileCollision += HandleFireCollision;
        }
    }

    void RemoveDistantFireTiles()
    {
        // Destroy fire tiles that are too far behind the player
        for (int i = spawnedFireTiles.Count - 1; i >= 0; i--)
        {
            if (spawnedFireTiles[i].transform.position.y + fireTileDestroyDistance < playerTransform.position.y)
            {
                Destroy(spawnedFireTiles[i]);
                spawnedFireTiles.RemoveAt(i);
            }
        }
    }

     public void HandleFireCollision()
    {
        // Wait for a short duration (0.25 seconds) and then load the end credits scene
        StartCoroutine(LoadEndCreditsScene());
    }

    public IEnumerator LoadEndCreditsScene()
    {
        // Wait for a short duration before loading the next scene
        yield return new WaitForSeconds(0f);

        // Store the score in PlayerPrefs to retrieve later
        PlayerPrefs.SetFloat("FinalScore", score);

        // Load the End Credits scene
        SceneManager.LoadScene(endCreditsSceneName);
    }

    void UpdateScore()
    {
        // Update score based on the vertical position of the player
        score = Mathf.Max(score, playerTransform.position.y);

        // Display the live score in the UI
        if (scoreText != null)
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
        }
    }

    void OnDrawGizmos()
{
    // Fire tile boxes
    Gizmos.color = Color.red;
    foreach (var fireTile in spawnedFireTiles)
    {
        if (fireTile != null)
            Gizmos.DrawWireCube(fireTile.transform.position, Vector3.one);
    }

    // Bounds
    Gizmos.color = Color.green;
    float bottom = (playerTransform != null) ? playerTransform.position.y - 10 : transform.position.y - 10;
    float top = (playerTransform != null) ? playerTransform.position.y + 50 : transform.position.y + 50;
    Gizmos.DrawLine(new Vector3(minXBound, bottom, 0), new Vector3(minXBound, top, 0));
    Gizmos.DrawLine(new Vector3(maxXBound, bottom, 0), new Vector3(maxXBound, top, 0));
}

}

public class FireTileCollision : MonoBehaviour
{
    public delegate void FireTileCollisionHandler();
    public event FireTileCollisionHandler OnFireTileCollision;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player collides with the fire tile, trigger the event
        if (collision.gameObject.CompareTag("Player"))
        {
            OnFireTileCollision?.Invoke(); // Trigger the collision event
        }
    }
}
