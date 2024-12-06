using UnityEngine;

public class AdvancedPulseVisualizer : MonoBehaviour
{
    public GameObject PulsePrefab;

    public void VisualizePulse(double amplitude, double frequency, double duration)
    {
        GameObject pulse = Instantiate(PulsePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        pulse.name = $"Pulse: Amp={amplitude}, Freq={frequency}, Dur={duration}";

        // Adjust scale and dynamic effects
        pulse.transform.localScale = new Vector3((float)amplitude, (float)frequency, (float)duration);
        Renderer renderer = pulse.GetComponent<Renderer>();
        renderer.material.color = new Color(
            (float)amplitude / 10.0f, 
            (float)frequency / 100.0f, 
            (float)duration / 5.0f
        );
    }
}
