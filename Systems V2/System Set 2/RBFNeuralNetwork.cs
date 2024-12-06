using System;
using System.Collections.Generic;
using UnityEngine;

public class RBFNeuralNetwork : MonoBehaviour
{
    private int inputSize = 3; // Number of input features (e.g., x1, x2, x3)
    private int hiddenSize = 3; // Number of hidden neurons
    private int outputSize = 1; // Single output (e.g., y)

    private float[][] inputData;
    private float[][] weightsHiddenToOutput;
    private float[][] centers; // Centers for RBF
    private float[] sigma; // Spread of RBF

    private float bias = 1.0f; // Bias term for the hidden layer

    void Start()
    {
        // Initialize inputs, weights, and parameters
        InitializeData();
        TrainRBFNetwork();
    }

    void InitializeData()
    {
        // Example input data
        inputData = new float[][]
        {
            new float[] { 0.1f, 0.5f, 0.9f },
            new float[] { 0.2f, 0.4f, 0.8f },
            new float[] { 0.3f, 0.3f, 0.7f }
        };

        centers = new float[hiddenSize][];
        sigma = new float[hiddenSize];

        for (int i = 0; i < hiddenSize; i++)
        {
            centers[i] = new float[inputSize];
            for (int j = 0; j < inputSize; j++)
            {
                centers[i][j] = UnityEngine.Random.Range(0.0f, 1.0f); // Random initialization for centers
            }
        }

        for (int i = 0; i < hiddenSize; i++)
        {
            sigma[i] = UnityEngine.Random.Range(0.1f, 1.0f); // Random initialization for sigma
        }

        weightsHiddenToOutput = new float[hiddenSize][];
        for (int i = 0; i < hiddenSize; i++)
        {
            weightsHiddenToOutput[i] = new float[outputSize];
            for (int j = 0; j < outputSize; j++)
            {
                weightsHiddenToOutput[i][j] = UnityEngine.Random.Range(-1.0f, 1.0f); // Random initialization for weights
            }
        }
    }

    float RadialBasisFunction(float[] input, float[] center, float sigma)
    {
        float distanceSquared = 0.0f;
        for (int i = 0; i < input.Length; i++)
        {
            distanceSquared += Mathf.Pow(input[i] - center[i], 2);
        }

        return Mathf.Exp(-distanceSquared / (2.0f * Mathf.Pow(sigma, 2)));
    }

    float[] ForwardPass(float[] input)
    {
        float[] hiddenOutputs = new float[hiddenSize];
        for (int i = 0; i < hiddenSize; i++)
        {
            hiddenOutputs[i] = RadialBasisFunction(input, centers[i], sigma[i]);
        }

        float[] output = new float[outputSize];
        for (int j = 0; j < outputSize; j++)
        {
            output[j] = bias; // Add bias to output
            for (int i = 0; i < hiddenSize; i++)
            {
                output[j] += hiddenOutputs[i] * weightsHiddenToOutput[i][j];
            }
        }

        return output;
    }

    void TrainRBFNetwork()
    {
        // Example training loop using Gradient Descent
        float learningRate = 0.01f;

        for (int epoch = 0; epoch < 100; epoch++)
        {
            float totalLoss = 0.0f;

            for (int dataIdx = 0; dataIdx < inputData.Length; dataIdx++)
            {
                float[] input = inputData[dataIdx];
                float[] targetOutput = new float[] { 1.0f }; // Example target output

                float[] output = ForwardPass(input);

                // Compute loss
                float loss = 0.0f;
                for (int j = 0; j < outputSize; j++)
                {
                    loss += 0.5f * Mathf.Pow(targetOutput[j] - output[j], 2);
                }
                totalLoss += loss;

                // Backpropagation for weights
                for (int j = 0; j < outputSize; j++)
                {
                    float error = targetOutput[j] - output[j];

                    for (int i = 0; i < hiddenSize; i++)
                    {
                        float[] hiddenOutputs = new float[hiddenSize];
                        for (int h = 0; h < hiddenSize; h++)
                        {
                            hiddenOutputs[h] = RadialBasisFunction(input, centers[h], sigma[h]);
                        }
                        weightsHiddenToOutput[i][j] += learningRate * error * hiddenOutputs[i];
                    }
                }
            }

            Debug.Log("Epoch " + epoch + ": Loss = " + totalLoss);
        }
    }
}
