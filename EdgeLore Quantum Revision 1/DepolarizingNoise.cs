using System;
using UnityEngine;
using System.Numerics;

public class DepolarizingNoise : NoiseModel
{
    public DepolarizingNoise(double probability) : base("Depolarizing Noise", probability) { }

    public override void ApplyNoise(QuantumState state)
    {
        var random = new System.Random();
        if (random.NextDouble() < Probability)
        {
            Debug.Log($"Applying Depolarizing Noise...");
            for (int i = 0; i < state.StateVector.Length; i++)
            {
                state.StateVector[i] = new Complex(
                    state.StateVector[i].Real * 0.99,
                    state.StateVector[i].Imaginary * 0.99
                );
            }
        }
    }
}
