using System;
using System.Collections.Generic;
using UnityEngine;

public class BeliefNetwork : MonoBehaviour
{
    [Serializable]
    public class Layer
    {
        public int neurons;
        public float[] values;
        public float[,] weights;
        public float[] biases;
    }

    public Layer[] layers;

    public void InitializeNetwork(int[] layerSizes)
    {
        layers = new Layer[layerSizes.Length];
        for (int i = 0; i < layerSizes.Length; i++)
        {
            layers[i] = new Layer
            {
                neurons = layerSizes[i],
                values = new float[layerSizes[i]],
                biases = new float[layerSizes[i]],
                weights = i > 0 ? new float[layerSizes[i], layerSizes[i - 1]] : null
            };

            if (i > 0) InitializeWeights(layers[i].weights, layerSizes[i], layerSizes[i - 1]);
        }
    }

    private void InitializeWeights(float[,] weights, int rows, int cols)
    {
        System.Random rand = new System.Random();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                weights[i, j] = (float)rand.NextDouble() * 2 - 1; // Random weights [-1, 1]
            }
        }
    }

    public float[] ForwardPropagation(float[] inputs)
    {
        if (layers == null || layers.Length < 2)
            throw new Exception("Network not initialized!");

        Array.Copy(inputs, layers[0].values, inputs.Length);

        for (int i = 1; i < layers.Length; i++)
        {
            Layer prevLayer = layers[i - 1];
            Layer currLayer = layers[i];

            for (int j = 0; j < currLayer.neurons; j++)
            {
                float sum = currLayer.biases[j];
                for (int k = 0; k < prevLayer.neurons; k++)
                {
                    sum += prevLayer.values[k] * currLayer.weights[j, k];
                }
                currLayer.values[j] = ReLU(sum); // ReLU activation function
            }
        }

        return layers[layers.Length - 1].values;
    }

    private float ReLU(float x) => Mathf.Max(0, x);

    public void HebbianLearning(float[] inputs, float learningRate = 0.01f)
    {
        if (layers == null || layers.Length < 2)
            throw new Exception("Network not initialized!");

        for (int i = 1; i < layers.Length; i++)
        {
            Layer prevLayer = layers[i - 1];
            Layer currLayer = layers[i];

            for (int j = 0; j < currLayer.neurons; j++)
            {
                for (int k = 0; k < prevLayer.neurons; k++)
                {
                    float deltaWeight = learningRate * prevLayer.values[k] * currLayer.values[j];
                    currLayer.weights[j, k] += deltaWeight;
                }
            }
        }
    }

    public void ApplyEvolution(float mutationRate = 0.1f, float mutationMagnitude = 0.5f)
    {
        System.Random rand = new System.Random();

        foreach (var layer in layers)
        {
            if (layer.weights == null) continue;

            for (int i = 0; i < layer.weights.GetLength(0); i++)
            {
                for (int j = 0; j < layer.weights.GetLength(1); j++)
                {
                    if (rand.NextDouble() < mutationRate)
                    {
                        layer.weights[i, j] += (float)(rand.NextDouble() * 2 - 1) * mutationMagnitude;
                    }
                }
            }
        }
    }
    public float[] QuantumBeliefUpdate(float[] stateVector, float[,] transitionMatrix)
    {
        float[] newState = new float[stateVector.Length];

        for (int i = 0; i < stateVector.Length; i++)
        {
            float sum = 0;
            for (int j = 0; j < stateVector.Length; j++)
            {
                sum += stateVector[j] * transitionMatrix[i, j];
            }
            newState[i] = sum;
        }

        // Normalize to ensure probabilities sum to 1
        float total = 0;
        foreach (var val in newState) total += val;
        for (int i = 0; i < newState.Length; i++) newState[i] /= total;

        return newState;
    }
}
