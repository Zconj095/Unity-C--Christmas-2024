using System;
using UnityEngine;

public class ExponentialApproximation : MonoBehaviour
{
    public float P0 = 1.0f;  // Initial value
    public float Lambda = 0.01f;  // Small perturbation (adjust as needed)

    void Start()
    {
        float exactResult = CalculateExact(P0, Lambda);
        float approximatedResult = CalculateApproximation(P0, Lambda);
        float deltaP = approximatedResult - P0;

        Debug.Log($"Exact Result: {exactResult}");
        Debug.Log($"Approximated Result: {approximatedResult}");
        Debug.Log($"Delta P: {deltaP}");
    }

    /// <summary>
    /// Calculates the exact result using e^Lambda.
    /// </summary>
    float CalculateExact(float P0, float Lambda)
    {
        return P0 * Mathf.Exp(Lambda);
    }

    /// <summary>
    /// Approximates the result using (1 + Lambda).
    /// </summary>
    float CalculateApproximation(float P0, float Lambda)
    {
        return P0 * (1 + Lambda);
    }
}
