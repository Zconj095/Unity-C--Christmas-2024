using UnityEngine;

public class SEMBeam : MonoBehaviour
{
    public float beamIntensity = 1.0f; // Intensity of the electron beam
    public float scanSpeed = 0.1f; // Speed of the scan
    public int resolution = 256; // Resolution of the scan image
    public LayerMask targetLayer; // Layer for nano-field particles

    private Texture2D scanImage; // Image generated by SEM
    private Camera semCamera;

    void Start()
    {
        // Initialize SEM image
        scanImage = new Texture2D(resolution, resolution, TextureFormat.RGB24, false);
        semCamera = Camera.main;
    }

    void Update()
    {
        PerformScan();
    }

    private void PerformScan()
    {
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                Vector3 scanPosition = new Vector3(
                    x / (float)resolution,
                    y / (float)resolution,
                    1.0f
                );

                Ray ray = semCamera.ViewportPointToRay(scanPosition);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, targetLayer))
                {
                    float intensity = CalculateBackscatter(hit.collider.gameObject, hit.point);
                    Color pixelColor = new Color(intensity, intensity, intensity);
                    scanImage.SetPixel(x, y, pixelColor);
                }
                else
                {
                    scanImage.SetPixel(x, y, Color.black);
                }
            }
        }

        scanImage.Apply();
    }

    private float CalculateBackscatter(GameObject target, Vector3 hitPoint)
    {
        // Example: Calculate backscatter intensity based on material density and angle
        NanoParticle particle = target.GetComponent<NanoParticle>();
        if (particle != null)
        {
            float angle = Vector3.Dot(hitPoint.normalized, Vector3.up); // Angle of incidence
            return particle.materialDensity * Mathf.Abs(angle) * beamIntensity;
        }

        return 0.0f;
    }

    void OnGUI()
    {
        // Display SEM image
        GUI.DrawTexture(new Rect(10, 10, 256, 256), scanImage, ScaleMode.StretchToFill, false);
    }
}
