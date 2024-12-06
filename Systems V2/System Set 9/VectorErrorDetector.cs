using System;
using System.Linq;
using UnityEngine;

public class VectorErrorDetector : MonoBehaviour
{
    public static float[] PredictNextState(float[] vector, int historyWindow)
    {
        // Auto-regression: Predict the next state based on historical values
        float[] predicted = new float[vector.Length];
        for (int i = 0; i < vector.Length; i++)
        {
            // Simple average-based prediction (can replace with ARIMA or more advanced models)
            predicted[i] = vector.Skip(Math.Max(0, i - historyWindow)).Average();
        }
        return predicted;
    }

    public static float[] CalculateError(float[] predicted, float[] observed)
    {
        // Compute the deviation (error) between predicted and observed values
        return predicted.Zip(observed, (p, o) => Math.Abs(p - o)).ToArray();
    }

    public static bool[] DetectAnomalies(float[] errors, float threshold)
    {
        // Detect anomalies where error exceeds the threshold
        return errors.Select(e => e > threshold).ToArray();
    }

    
}
