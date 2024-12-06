using UnityEngine;
using UnityEngine.UI;

public class FrequencyFluxMeter : MonoBehaviour
{
    public Text frequencyDisplay; // UI Text to display frequency
    private float frequency; // Variable to store simulated frequency value

    void Start()
    {
        frequency = 0f;
        InvokeRepeating("UpdateFrequency", 1f, 1f); // Update frequency every second
    }

    void UpdateFrequency()
    {
        frequency = SimulateFrequencyFlux(); // Simulate new frequency value
        DisplayFrequency(frequency); // Update the display
    }

    float SimulateFrequencyFlux()
    {
        // Simulate frequency measurement
        return Random.Range(0f, 1000f); // Example range from 0 to 1000 Hz
    }

    void DisplayFrequency(float freq)
    {
        frequencyDisplay.text = "Frequency: " + freq.ToString("F2") + " Hz";
    }
}