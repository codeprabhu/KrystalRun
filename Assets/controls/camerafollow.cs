using UnityEngine;

public class camerafollow : MonoBehaviour
{
    [SerializeField] private Transform player; // Reference to the player's transform
    [SerializeField] private Vector3 offset;   // Offset of the camera relative to the player
    [SerializeField] private float smoothSpeed = 0.125f; // Smoothness of camera movement

    private void LateUpdate()
    {
        // Calculate the desired position
        Vector3 desiredPosition = player.position + offset;

        // Smoothly interpolate the camera's position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;
    }
}
