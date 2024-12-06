using System;
using System.Collections.Generic;

public class LayeredNoiseModel
{
    private List<NoiseModel> noiseLayers = new List<NoiseModel>();

    public void AddNoiseLayer(NoiseModel noise)
    {
        noiseLayers.Add(noise);
    }

    public void ApplyNoise(QuantumState state)
    {
        foreach (var noise in noiseLayers)
        {
            noise.ApplyNoise(state);
        }
    }
}
