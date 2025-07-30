using UnityEngine;

public class VerticalFitCamera : MonoBehaviour
{
    [Tooltip("Target aspect ratio (e.g., 9:16 for vertical fit)")]
    public float targetAspect = 9f / 16f; 

    private void Start()
    {
        AdjustCameraViewport();
    }

    private void AdjustCameraViewport()
    {
        // Calculate the current screen aspect ratio
        float windowAspect = (float)Screen.width / Screen.height;

        // Calculate the scaling height based on the target aspect ratio
        float scaleHeight = windowAspect / targetAspect;

        Camera camera = GetComponent<Camera>();

        if (scaleHeight < 1.0f)
        {
            // Pillarbox: Black bars on the sides
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;
        }
        else
        {
            // Letterbox: Black bars on the top and bottom
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = camera.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }

    private void OnValidate()
    {
        // Automatically adjust in the Editor when values are changed
        AdjustCameraViewport();
    }
}
