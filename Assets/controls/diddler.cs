using UnityEngine;

public class Boundary : MonoBehaviour
{
    public Vector2 minBounds; // Minimum x, y coordinates
    public Vector2 maxBounds; // Maximum x, y coordinates
    public Transform player;
    public float backwardLimit = 5f; // Allowable backward movement (buffer)

    private float minYPosition; // Tracks the furthest forward the player has gone

    void Start()
    {
        // Find the player by tag and initialize the minimum Y position
        player = GameObject.FindWithTag("Player").transform;
        minYPosition = player.position.y; // Start tracking forward progress
    }

    void Update()
    {
        // Update the minimum Y position based on the player's forward movement
        if (player.position.y > minYPosition)
        {
            minYPosition = player.position.y;
        }

        // Clamp the player's position within bounds and backward limit
        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(player.position.x, minBounds.x, maxBounds.x), // Clamp X within bounds
            Mathf.Clamp(player.position.y, Mathf.Max(minYPosition - backwardLimit, minBounds.y), maxBounds.y) // Restrict excessive backward movement
        );

        // Apply the clamped position
        player.position = clampedPosition;
    }
}
