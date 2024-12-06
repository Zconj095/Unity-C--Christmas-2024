using UnityEngine;
using UnityEngine.UI;

public class FrequencyFluxMeterCalibration : MonoBehaviour
{
    public Text frequencyDisplay;    // UI Text to display frequency
    public Slider calibrationSlider; // UI Slider for calibration
    public Text calibrationDisplay;  // UI Text to display calibration percentage

    private float frequency;         // Variable to store simulated frequency value
    private float calibrationFactor; // Calibration factor from 0 to 1

    void Start()
    {
        frequency = 0f;
        calibrationFactor = 1f; // Default 100% calibration
        calibrationSlider.onValueChanged.AddListener(OnCalibrationChanged);
        InvokeRepeating("UpdateFrequency", 1f, 1f); // Update frequency every second
    }

    void UpdateFrequency()
    {
        float rawFrequency = SimulateFrequencyFlux(); // Simulate new frequency value
        frequency = rawFrequency * calibrationFactor; // Apply calibration factor
        DisplayFrequency(frequency); // Update the display
    }

    float SimulateFrequencyFlux()
    {
        return Random.Range(0f, 1000f); // Example range from 0 to 1000 Hz
    }

    void OnCalibrationChanged(float value)
    {
        calibrationFactor = value; // Update the calibration factor
        calibrationDisplay.text = "Calibration: " + (value * 100f).ToString("F0") + "%"; // Update calibration display
    }

    void DisplayFrequency(float freq)
    {
        frequencyDisplay.text = "Frequency: " + freq.ToString("F2") + " Hz";
    }
}