using UnityEngine;

[ExecuteInEditMode]
public class CustomCameraAspect : MonoBehaviour
{
    public float targetAspect = 1.0f; // For a 1:1 aspect ratio
    public Camera renderCamera;

    void Update()
    {
        if (renderCamera != null)
        {
            // Set the target aspect ratio for the camera
            renderCamera.aspect = targetAspect;
        }
    }
}