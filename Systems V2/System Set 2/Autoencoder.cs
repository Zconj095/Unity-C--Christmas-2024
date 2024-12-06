using System;
using UnityEngine;

public class Autoencoder : MonoBehaviour
{
    // Define the network architecture
    private int inputSize = 5; // Number of input features
    private int hiddenSize = 3; // Size of the bottleneck layer
    private int outputSize = 5; // Output size (reconstructed input)

    private float[,] weightsInputToHidden; // Weights from input to hidden layer
    private float[,] weightsHiddenToOutput; // Weights from hidden to output layer
    private float[] biasesHidden; // Biases for hidden layer
    private float[] biasesOutput; // Biases for output layer

    private float learningRate = 0.01f;

    // Initialize the network
    void Start()
    {
        InitializeWeights();

        // Example input data
        float[,] trainingData = {
            { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f },
            { 0.5f, 0.4f, 0.3f, 0.2f, 0.1f },
            { 0.3f, 0.7f, 0.8f, 0.1f, 0.4f }
        };

        // Train the autoencoder
        for (int epoch = 0; epoch < 1000; epoch++)
        {
            for (int row = 0; row < trainingData.GetLength(0); row++)
            {
                // Extract the row as a 1D array
                float[] input = ExtractRow(trainingData, row);

                Train(input);
            }

            if (epoch % 100 == 0)
            {
                float fitness = EvaluateFitness(trainingData);
                Debug.Log($"Epoch {epoch}, Fitness: {fitness}");
            }
        }

        Debug.Log("Training complete!");
    }

    // Initialize weights and biases with random values
    private void InitializeWeights()
    {
        System.Random random = new System.Random();

        // Input to hidden weights
        weightsInputToHidden = new float[inputSize, hiddenSize];
        for (int i = 0; i < inputSize; i++)
        {
            for (int j = 0; j < hiddenSize; j++)
            {
                weightsInputToHidden[i, j] = (float)random.NextDouble() * 0.1f;
            }
        }

        // Hidden to output weights
        weightsHiddenToOutput = new float[hiddenSize, outputSize];
        for (int i = 0; i < hiddenSize; i++)
        {
            for (int j = 0; j < outputSize; j++)
            {
                weightsHiddenToOutput[i, j] = (float)random.NextDouble() * 0.1f;
            }
        }

        // Biases
        biasesHidden = new float[hiddenSize];
        biasesOutput = new float[outputSize];
        for (int i = 0; i < hiddenSize; i++) biasesHidden[i] = (float)random.NextDouble() * 0.1f;
        for (int i = 0; i < outputSize; i++) biasesOutput[i] = (float)random.NextDouble() * 0.1f;
    }

    // Activation function (ReLU)
    private float Activate(float value)
    {
        return Mathf.Max(0, value);
    }

    // Derivative of ReLU
    private float ActivateDerivative(float value)
    {
        return value > 0 ? 1 : 0;
    }

    // Forward pass
    private float[] Forward(float[] input)
    {
        // Hidden layer computation
        float[] hidden = new float[hiddenSize];
        for (int i = 0; i < hiddenSize; i++)
        {
            hidden[i] = biasesHidden[i];
            for (int j = 0; j < inputSize; j++)
            {
                hidden[i] += input[j] * weightsInputToHidden[j, i];
            }
            hidden[i] = Activate(hidden[i]);
        }

        // Output layer computation
        float[] output = new float[outputSize];
        for (int i = 0; i < outputSize; i++)
        {
            output[i] = biasesOutput[i];
            for (int j = 0; j < hiddenSize; j++)
            {
                output[i] += hidden[j] * weightsHiddenToOutput[j, i];
            }
            output[i] = Activate(output[i]);
        }

        return output;
    }

    // Backpropagation and weight updates
    private void Train(float[] input)
    {
        // Forward pass
        float[] hidden = new float[hiddenSize];
        for (int i = 0; i < hiddenSize; i++)
        {
            hidden[i] = biasesHidden[i];
            for (int j = 0; j < inputSize; j++)
            {
                hidden[i] += input[j] * weightsInputToHidden[j, i];
            }
            hidden[i] = Activate(hidden[i]);
        }

        float[] output = new float[outputSize];
        for (int i = 0; i < outputSize; i++)
        {
            output[i] = biasesOutput[i];
            for (int j = 0; j < hiddenSize; j++)
            {
                output[i] += hidden[j] * weightsHiddenToOutput[j, i];
            }
            output[i] = Activate(output[i]);
        }

        // Compute output error
        float[] outputError = new float[outputSize];
        for (int i = 0; i < outputSize; i++)
        {
            outputError[i] = input[i] - output[i];
        }

        // Compute hidden error
        float[] hiddenError = new float[hiddenSize];
        for (int i = 0; i < hiddenSize; i++)
        {
            hiddenError[i] = 0;
            for (int j = 0; j < outputSize; j++)
            {
                hiddenError[i] += outputError[j] * weightsHiddenToOutput[i, j];
            }
            hiddenError[i] *= ActivateDerivative(hidden[i]);
        }

        // Update weights and biases
        for (int i = 0; i < hiddenSize; i++)
        {
            for (int j = 0; j < outputSize; j++)
            {
                weightsHiddenToOutput[i, j] += learningRate * outputError[j] * hidden[i];
            }
        }

        for (int i = 0; i < inputSize; i++)
        {
            for (int j = 0; j < hiddenSize; j++)
            {
                weightsInputToHidden[i, j] += learningRate * hiddenError[j] * input[i];
            }
        }

        for (int i = 0; i < hiddenSize; i++) biasesHidden[i] += learningRate * hiddenError[i];
        for (int i = 0; i < outputSize; i++) biasesOutput[i] += learningRate * outputError[i];
    }

    // Evaluate fitness
    private float EvaluateFitness(float[,] trainingData)
    {
        float totalError = 0f;

        for (int row = 0; row < trainingData.GetLength(0); row++)
        {
            float[] input = ExtractRow(trainingData, row);
            float[] output = Forward(input);

            for (int i = 0; i < input.Length; i++)
            {
                totalError += Mathf.Abs(input[i] - output[i]);
            }
        }

        return -totalError; // Lower error = higher fitness
    }

    // Helper method to extract a row as a 1D array
    private float[] ExtractRow(float[,] matrix, int row)
    {
        float[] result = new float[matrix.GetLength(1)];
        for (int col = 0; col < matrix.GetLength(1); col++)
        {
            result[col] = matrix[row, col];
        }
        return result;
    }
}
