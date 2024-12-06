using UnityEngine;

public class PulseVisualizer : MonoBehaviour
{
    public GameObject PulsePrefab;

    public void VisualizePulse(double amplitude, double frequency, double duration)
    {
        GameObject pulse = Instantiate(PulsePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        pulse.name = $"Pulse: Amp={amplitude}, Freq={frequency}, Dur={duration}";

        // Adjust visual properties based on pulse parameters
        pulse.transform.localScale = new Vector3((float)amplitude, (float)frequency, (float)duration);
    }
}
