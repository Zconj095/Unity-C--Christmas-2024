using System.Collections.Generic;
using UnityEngine;

public class INLBinaryWeightedCircuit : MonoBehaviour
{
    public int resolution = 8;           // Resolution of the DAC (number of bits)
    public float vRef = 5.0f;            // Reference voltage
    public float noiseLevel = 0.01f;     // Noise level to simulate imperfections
    public float targetRangeMin = 0.0f; // Target analog range (min)
    public float targetRangeMax = 5.0f; // Target analog range (max)

    private List<float> idealOutputs = new List<float>();
    private List<float> measuredOutputs = new List<float>();
    private List<float> inlValues = new List<float>();

    void Start()
    {
        CalculateOutputs();
        CalculateINL();
        PrintResults();
    }

    void CalculateOutputs()
    {
        int maxDigitalValue = (1 << resolution) - 1;

        for (int digitalValue = 0; digitalValue <= maxDigitalValue; digitalValue++)
        {
            // Calculate ideal output
            float idealOutput = vRef * (float)digitalValue / maxDigitalValue;

            // Add noise or non-linearity for measured output
            float measuredOutput = idealOutput + Random.Range(-noiseLevel, noiseLevel);

            // Map the measured output to a specific range
            measuredOutput = Mathf.Lerp(targetRangeMin, targetRangeMax, measuredOutput / vRef);

            idealOutputs.Add(idealOutput);
            measuredOutputs.Add(measuredOutput);
        }
    }

    void CalculateINL()
    {
        for (int i = 0; i < idealOutputs.Count; i++)
        {
            float inl = measuredOutputs[i] - idealOutputs[i];
            inlValues.Add(inl);
        }
    }

    void PrintResults()
    {
        Debug.Log("Digital Input\tIdeal Output\tMeasured Output\tINL");
        for (int i = 0; i < idealOutputs.Count; i++)
        {
            Debug.Log($"{i}\t\t{idealOutputs[i]:F3}\t\t{measuredOutputs[i]:F3}\t\t{inlValues[i]:F3}");
        }
    }
}
