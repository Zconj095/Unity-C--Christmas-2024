using System.Collections.Generic;
using UnityEngine;
using System;

public class NoiseModel
{
    public string Name { get; private set; }
    public double Probability { get; private set; } // Probability of noise occurrence

    public NoiseModel(string name, double probability)
    {
        Name = name;
        Probability = probability;
    }

    public virtual void ApplyNoise(QuantumState state)
    {
        var random = new System.Random(); // Fully qualified System.Random
        if (random.NextDouble() < Probability)
        {
            Debug.Log($"Applying {Name} noise to the quantum state.");
        }
    }
}
