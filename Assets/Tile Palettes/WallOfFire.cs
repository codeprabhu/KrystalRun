using System.Collections;
using System.Collections.Generic; // For List<>
using UnityEngine;

public class WallOfFire : MonoBehaviour
{
    public float speed = 5f; // Speed of the wall's upward movement
    public float maxDistanceBehindPlayer = 6f; // Maximum distance the wall can be behind the player (in tiles/units)
    private bool isMoving = false; // Tracks whether the wall should move
    private FireSpriteSpawner fireSpriteSpawner;
    private Transform playerTransform; // Reference to the player's transform

    void Start()
    {
        // Add and adjust PolygonCollider2D to match the sprite
        PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D>();
        if (polygonCollider == null)
        {
            polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
        }

        // Ensure the collider is a trigger
        polygonCollider.isTrigger = true;

        // Find the FireSpriteSpawner instance
        fireSpriteSpawner = FindFirstObjectByType<FireSpriteSpawner>();
        if (fireSpriteSpawner == null)
        {
            Debug.LogError("FireSpriteSpawner not found in the scene!");
        }

        // Find the Player's Transform
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player object not found in the scene!");
        }

        // Start the wall movement after a delay
        Invoke(nameof(StartMoving), 5f); // Delay of 5 seconds
    }

    void Update()
{
    if (isMoving && playerTransform != null)
    {
        // Move the wall upward at a constant speed
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Check if the wall has fallen too far behind the player
        float distanceBehind = playerTransform.position.y - transform.position.y;

        if (distanceBehind > maxDistanceBehindPlayer)
        {
            // Teleport to maxDistanceBehindPlayer units below the player
            transform.position = new Vector3(
                transform.position.x,
                playerTransform.position.y - maxDistanceBehindPlayer,
                transform.position.z
            );
        }
    }
}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Trigger the FireSpriteSpawner's HandleFireCollision method
            if (fireSpriteSpawner != null)
            {
                fireSpriteSpawner.HandleFireCollision();
            }
        }
    }

    private void StartMoving()
    {
        isMoving = true; // Allow the wall to start moving
    }
}
