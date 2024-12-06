using UnityEngine;

public class BackscatteredElectrons : MonoBehaviour
{
    [Tooltip("Layer mask for detecting the sample.")]
    public LayerMask sampleLayer;

    [Tooltip("Maximum detection angle for backscattered electrons (degrees).")]
    public float maxDetectionAngle = 90.0f;

    [Tooltip("Intensity multiplier for backscattered electrons.")]
    public float intensityMultiplier = 1.0f;

    private Texture2D bseTexture;
    private Camera bseCamera;

    [Tooltip("Resolution of the detection grid.")]
    public int resolution = 256;

    void Start()
    {
        // Create a texture for backscattered electron intensity visualization
        bseTexture = new Texture2D(resolution, resolution, TextureFormat.RFloat, false);

        // Setup a camera to simulate BSE detection
        bseCamera = new GameObject("BSE Camera").AddComponent<Camera>();
        bseCamera.orthographic = true;
        bseCamera.orthographicSize = 5.0f;
        bseCamera.clearFlags = CameraClearFlags.SolidColor;
        bseCamera.backgroundColor = Color.black;
        bseCamera.cullingMask = sampleLayer;
        bseCamera.transform.position = transform.position + Vector3.back * 10;
        bseCamera.transform.LookAt(transform.position);
    }

    void Update()
    {
        SimulateBackscatteredElectrons();
    }

    private void SimulateBackscatteredElectrons()
    {
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                // Map pixel coordinates to world space
                Vector3 rayOrigin = bseCamera.ScreenToWorldPoint(new Vector3(x, y, bseCamera.nearClipPlane));
                Ray ray = new Ray(rayOrigin, bseCamera.transform.forward);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, sampleLayer))
                {
                    // Calculate the detection angle
                    float detectionAngle = Vector3.Angle(ray.direction, hit.normal);

                    // Get material properties
                    MaterialProperties materialProps = hit.collider.GetComponent<MaterialProperties>();
                    float atomicNumber = materialProps != null ? materialProps.atomicNumber : 1.0f;

                    // Compute BSE intensity
                    float intensity = (detectionAngle <= maxDetectionAngle) ? atomicNumber * intensityMultiplier : 0.0f;

                    // Map intensity to grayscale
                    bseTexture.SetPixel(x, y, new Color(intensity, intensity, intensity));
                }
                else
                {
                    // No hit: set to black
                    bseTexture.SetPixel(x, y, Color.black);
                }
            }
        }

        // Apply the updated BSE texture
        bseTexture.Apply();
    }

    void OnGUI()
    {
        // Display the BSE texture
        GUI.DrawTexture(new Rect(280, 10, 256, 256), bseTexture, ScaleMode.StretchToFill, false);
    }
}
