using System.Collections.Generic;
using UnityEngine;

public class InfiniteChunkGenerator : MonoBehaviour
{
    public GameObject chunkPrefab; // The prefab for a chunk of 5 layers
    public Transform playerTransform; // Reference to the player or camera
    public float chunkHeight = 50f; // Height of one chunk (5 layers * layer height)
    public int initialChunks = 2; // Number of chunks to spawn at the start
    public int bufferChunks = 1; // Extra chunks to keep ahead of the player

    private Queue<GameObject> activeChunks = new Queue<GameObject>();
    private float nextSpawnY; // Y position for the next chunk

    void Start()
    {
        // Spawn initial chunks
        for (int i = 0; i < initialChunks; i++)
        {
            SpawnChunk(i * chunkHeight);
        }
    }

    void Update()
    {
        // Check if the player is close to the top of the last chunk
        if (playerTransform.position.y + (bufferChunks * chunkHeight) > nextSpawnY)
        {
            // Spawn a new chunk
            SpawnChunk(nextSpawnY);

            // Optionally, remove the oldest chunk to prevent infinite growth
            if (activeChunks.Count > initialChunks)
            {
                DestroyOldestChunk();
            }
        }
    }

    void SpawnChunk(float yPosition)
    {
        GameObject newChunk = Instantiate(chunkPrefab, new Vector3(0, yPosition, 0), Quaternion.identity);
        activeChunks.Enqueue(newChunk);
        nextSpawnY += chunkHeight;
    }

    void DestroyOldestChunk()
    {
        GameObject oldestChunk = activeChunks.Dequeue();
        Destroy(oldestChunk);
    }
}
