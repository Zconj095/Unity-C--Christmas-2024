using UnityEngine;

public class ElectronSignal : MonoBehaviour
{
    [Tooltip("Intensity of the primary electron beam.")]
    public float beamIntensity = 1.0f;

    [Tooltip("Layer mask for detecting the sample.")]
    public LayerMask sampleLayer;

    private Texture2D signalTexture;
    private Camera signalCamera;

    [Tooltip("Resolution of the detection grid.")]
    public int resolution = 256;

    void Start()
    {
        // Create a signal texture for visualizing the signal
        signalTexture = new Texture2D(resolution, resolution, TextureFormat.RFloat, false);

        // Setup a camera to simulate signal detection
        signalCamera = new GameObject("Signal Camera").AddComponent<Camera>();
        signalCamera.orthographic = true;
        signalCamera.orthographicSize = 5.0f;
        signalCamera.clearFlags = CameraClearFlags.SolidColor;
        signalCamera.backgroundColor = Color.black;
        signalCamera.cullingMask = sampleLayer;
        signalCamera.transform.position = transform.position + Vector3.back * 10;
        signalCamera.transform.LookAt(transform.position);
    }

    void Update()
    {
        SimulateElectronSignal();
    }

    private void SimulateElectronSignal()
    {
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                // Map pixel coordinates to world space
                Vector3 rayOrigin = signalCamera.ScreenToWorldPoint(new Vector3(x, y, signalCamera.nearClipPlane));
                Ray ray = new Ray(rayOrigin, signalCamera.transform.forward);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, sampleLayer))
                {
                    // Get the signal contribution
                    MaterialProperties materialProps = hit.collider.GetComponent<MaterialProperties>();
                    float signal = materialProps != null ? materialProps.atomicNumber * beamIntensity : 0.0f;

                    // Map signal to grayscale
                    signalTexture.SetPixel(x, y, new Color(signal, signal, signal));
                }
                else
                {
                    // No hit: set to black
                    signalTexture.SetPixel(x, y, Color.black);
                }
            }
        }

        // Apply the updated signal texture
        signalTexture.Apply();
    }

    void OnGUI()
    {
        // Display the signal texture
        GUI.DrawTexture(new Rect(10, 10, 256, 256), signalTexture, ScaleMode.StretchToFill, false);
    }
}
