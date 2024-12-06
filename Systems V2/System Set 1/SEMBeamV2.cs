using UnityEngine;

public class SEMBeamV2 : MonoBehaviour
{
    public float beamIntensity = 1.0f; // Intensity of the electron beam
    public float scanSpeed = 0.1f; // Speed of the scan
    public int resolution = 256; // Resolution of the scan image
    public LayerMask targetLayer; // Layer for nano-field particles

    private Texture2D conductedImage; // Image for conducted electrons
    private Texture2D absorbedImage; // Image for absorbed electrons
    private Camera semCamera;

    void Start()
    {
        // Initialize SEM images
        conductedImage = new Texture2D(resolution, resolution, TextureFormat.RGB24, false);
        absorbedImage = new Texture2D(resolution, resolution, TextureFormat.RGB24, false);
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
                    NanoParticleV2 particle = hit.collider.GetComponent<NanoParticleV2>();

                    if (particle != null)
                    {
                        (float conducted, float absorbed) = particle.InteractWithElectron(ray.direction);

                        // Map conducted and absorbed to grayscale values
                        Color conductedColor = new Color(conducted, conducted, conducted);
                        Color absorbedColor = new Color(absorbed, absorbed, absorbed);

                        conductedImage.SetPixel(x, y, conductedColor);
                        absorbedImage.SetPixel(x, y, absorbedColor);
                    }
                }
                else
                {
                    // Background is black for no interaction
                    conductedImage.SetPixel(x, y, Color.black);
                    absorbedImage.SetPixel(x, y, Color.black);
                }
            }
        }

        conductedImage.Apply();
        absorbedImage.Apply();
    }

    

    void OnGUI()
    {
        // Display SEM images
        GUI.DrawTexture(new Rect(10, 10, 256, 256), conductedImage, ScaleMode.StretchToFill, false);
        GUI.DrawTexture(new Rect(280, 10, 256, 256), absorbedImage, ScaleMode.StretchToFill, false);
    }
}
