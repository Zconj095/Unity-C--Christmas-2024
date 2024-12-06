using System;

public class AmplitudeDampingNoise : NoiseModel
{
    public AmplitudeDampingNoise(double probability) : base("Amplitude Damping Noise", probability) { }

    public override void ApplyNoise(QuantumState state)
    {
        var random = new System.Random();
        if (random.NextDouble() < Probability)
        {
            UnityEngine.Debug.Log($"Applying Amplitude Damping Noise...");
            for (int i = 0; i < state.StateVector.Length; i++)
            {
                state.StateVector[i] = new Complex(
                    state.StateVector[i].Real * 0.9,
                    state.StateVector[i].Imaginary
                );
            }
        }
    }
}
