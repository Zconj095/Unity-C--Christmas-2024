using UnityEngine;
using UnityEngine.UI;

public class FrequencyFluxMeterCalibrationBar : MonoBehaviour
{
    public Text frequencyDisplay;    // UI Text to display frequency
    public Image calibrationBar;     // UI Image for fill-up bar
    public Text calibrationDisplay;  // UI Text for calibration percentage
    public Button incrementButton;   // Button to increase calibration
    public Button decrementButton;   // Button to decrease calibration

    private float frequency;         // Variable to store simulated frequency value
    private float calibrationFactor; // Calibration factor from 0 to 1

    void Start()
    {
        frequency = 0f;
        calibrationFactor = 1f; // Start at 100% 
        calibrationBar.fillAmount = calibrationFactor; 
        incrementButton.onClick.AddListener(IncrementCalibration);
        decrementButton.onClick.AddListener(DecrementCalibration);
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

    void IncrementCalibration()
    {
        calibrationFactor = Mathf.Clamp(calibrationFactor + 0.01f, 0f, 1f); // Increase by 1%
        UpdateCalibrationDisplay();
    }

    void DecrementCalibration()
    {
        calibrationFactor = Mathf.Clamp(calibrationFactor - 0.01f, 0f, 1f); // Decrease by 1%
        UpdateCalibrationDisplay();
    }

    void UpdateCalibrationDisplay()
    {
        calibrationBar.fillAmount = calibrationFactor; // Update fill amount
        calibrationDisplay.text = "Calibration: " + (calibrationFactor * 100f).ToString("F0") + "%"; // Update text display
    }

    void DisplayFrequency(float freq)
    {
        frequencyDisplay.text = "Frequency: " + freq.ToString("F2") + " Hz";
    }
}