using UnityEngine;

public class ElectronDetector : MonoBehaviour
{
    [Tooltip("Intensity multiplier for topographic contrast.")]
    public float topographicMultiplier = 1.0f;

    [Tooltip("Intensity multiplier for compositional contrast.")]
    public float compositionalMultiplier = 1.0f;

    [Tooltip("Resolution of the detection grid.")]
    public int resolution = 256;

    [Tooltip("Layer mask for detecting the sample.")]
    public LayerMask sampleLayer;

    private Texture2D topographicContrastImage;
    private Texture2D compositionalContrastImage;

    private Camera detectionCamera;

    void Start()
    {
        // Initialize the detection images
        topographicContrastImage = new Texture2D(resolution, resolution, TextureFormat.RFloat, false);
        compositionalContrastImage = new Texture2D(resolution, resolution, TextureFormat.RFloat, false);

        // Create a dedicated camera for electron detection
        detectionCamera = new GameObject("Detection Camera").AddComponent<Camera>();
        detectionCamera.orthographic = true;
        detectionCamera.orthographicSize = 5.0f;
        detectionCamera.cullingMask = sampleLayer;
        detectionCamera.clearFlags = CameraClearFlags.SolidColor;
        detectionCamera.backgroundColor = Color.black;
    }

    void Update()
    {
        SimulateDetection();
    }

    private void SimulateDetection()
    {
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                // Map pixel coordinates to world space
                Vector3 rayOrigin = detectionCamera.ScreenToWorldPoint(new Vector3(x, y, detectionCamera.nearClipPlane));
                Ray ray = new Ray(rayOrigin, detectionCamera.transform.forward);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, sampleLayer))
                {
                    // Calculate topographic contrast
                    float topographicIntensity = Vector3.Dot(hit.normal, -ray.direction);
                    topographicIntensity = Mathf.Clamp01(topographicIntensity * topographicMultiplier);

                    // Calculate compositional contrast
                    MaterialProperties materialProps = hit.collider.GetComponent<MaterialProperties>();
                    float atomicNumber = materialProps != null ? materialProps.atomicNumber : 1.0f; // Default atomic number = 1
                    float compositionalIntensity = atomicNumber * compositionalMultiplier;

                    // Set the pixel values
                    topographicContrastImage.SetPixel(x, y, new Color(topographicIntensity, topographicIntensity, topographicIntensity));
                    compositionalContrastImage.SetPixel(x, y, new Color(compositionalIntensity, compositionalIntensity, compositionalIntensity));
                }
                else
                {
                    // No hit: set to black
                    topographicContrastImage.SetPixel(x, y, Color.black);
                    compositionalContrastImage.SetPixel(x, y, Color.black);
                }
            }
        }

        // Apply the textures
        topographicContrastImage.Apply();
        compositionalContrastImage.Apply();
    }

    void OnGUI()
    {
        // Display contrast images
        GUI.DrawTexture(new Rect(10, 10, 256, 256), topographicContrastImage, ScaleMode.StretchToFill, false);
        GUI.DrawTexture(new Rect(280, 10, 256, 256), compositionalContrastImage, ScaleMode.StretchToFill, false);
    }
}
