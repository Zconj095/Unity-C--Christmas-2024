using System;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNet : MonoBehaviour
{
    private List<float[]> layers; // Neural network layers
    private List<float[,]> weights; // Weights for connections
    private System.Random random;

    public NeuralNet(int[] layerSizes)
    {
        random = new System.Random();
        layers = new List<float[]>();
        weights = new List<float[,]>();

        // Initialize layers
        foreach (int size in layerSizes)
        {
            layers.Add(new float[size]);
        }

        // Initialize weights
        for (int i = 0; i < layerSizes.Length - 1; i++)
        {
            float[,] weightMatrix = new float[layerSizes[i], layerSizes[i + 1]];
            for (int j = 0; j < layerSizes[i]; j++)
            {
                for (int k = 0; k < layerSizes[i + 1]; k++)
                {
                    weightMatrix[j, k] = (float)(random.NextDouble() * 2 - 1); // Random between -1 and 1
                }
            }
            weights.Add(weightMatrix);
        }
    }

    public float[] FeedForward(float[] input)
    {
        layers[0] = input;

        for (int i = 0; i < weights.Count; i++)
        {
            for (int j = 0; j < layers[i + 1].Length; j++)
            {
                layers[i + 1][j] = 0;
                for (int k = 0; k < layers[i].Length; k++)
                {
                    layers[i + 1][j] += layers[i][k] * weights[i][k, j];
                }
                layers[i + 1][j] = Tanh(layers[i + 1][j]); // Use custom Tanh
            }
        }

        return layers[^1]; // Return last layer (output)
    }

    private float Tanh(float value)
    {
        // Custom hyperbolic tangent implementation
        double ePowerPos = Math.Exp(value);
        double ePowerNeg = Math.Exp(-value);
        return (float)((ePowerPos - ePowerNeg) / (ePowerPos + ePowerNeg));
    }
}
