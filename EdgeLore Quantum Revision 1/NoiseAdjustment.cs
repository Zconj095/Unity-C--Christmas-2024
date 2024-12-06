using UnityEngine;
using UnityEngine.UI;


public class NoiseAdjustment : MonoBehaviour
{
    public Slider DepolarizingNoiseSlider;
    public Slider AmplitudeDampingNoiseSlider;

    private QuantumSimulator simulator;

    void Start()
    {
        simulator = FindObjectOfType<QuantumSimulator>(); // This now works as QuantumSimulator is a Unity object

        DepolarizingNoiseSlider.onValueChanged.AddListener(UpdateDepolarizingNoise);
        AmplitudeDampingNoiseSlider.onValueChanged.AddListener(UpdateAmplitudeDampingNoise);
    }

    void UpdateDepolarizingNoise(float value)
    {
        Debug.Log($"Updated Depolarizing Noise Probability to {value}");
    }

    void UpdateAmplitudeDampingNoise(float value)
    {
        Debug.Log($"Updated Amplitude Damping Noise Probability to {value}");
    }
}
