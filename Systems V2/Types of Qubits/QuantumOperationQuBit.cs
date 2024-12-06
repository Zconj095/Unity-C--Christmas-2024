using System;
using UnityEngine;

public class QuantumOperationQuBit : MonoBehaviour
{
    private static System.Random random = new System.Random();

    public float ProbabilityAmplitude { get; private set; }

    public QuantumOperationQuBit()
    {
        ProbabilityAmplitude = (float)random.NextDouble(); // Generates a random number between 0.0 and 1.0
    }

    public void Superpose()
    {
        ProbabilityAmplitude = (ProbabilityAmplitude + (float)random.NextDouble()) / 2;
    }

    public float Measure()
    {
        return ProbabilityAmplitude > 0.5f ? 1 : 0;
    }
}
