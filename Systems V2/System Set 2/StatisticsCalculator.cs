using System;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsCalculator : MonoBehaviour
{
    public List<float> deltaValues = new List<float> { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f }; // Example input values

    void Start()
    {
        // Calculate mean and standard deviation
        float mean = CalculateMean(deltaValues);
        float standardDeviation = CalculateStandardDeviation(deltaValues, mean);

        // Output results
        Debug.Log($"Mean (m_delta): {mean}");
        Debug.Log($"Standard Deviation (sigma_delta): {standardDeviation}");
    }

    /// <summary>
    /// Calculates the mean (m_delta) of a list of values.
    /// </summary>
    float CalculateMean(List<float> values)
    {
        float sum = 0f;
        foreach (float value in values)
        {
            sum += value;
        }
        return sum / values.Count;
    }

    /// <summary>
    /// Calculates the standard deviation (sigma_delta) of a list of values.
    /// </summary>
    float CalculateStandardDeviation(List<float> values, float mean)
    {
        float sumOfSquares = 0f;
        foreach (float value in values)
        {
            sumOfSquares += Mathf.Pow(value - mean, 2);
        }
        return Mathf.Sqrt(sumOfSquares / (values.Count - 1));
    }
}
