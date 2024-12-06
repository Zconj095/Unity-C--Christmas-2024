using System;
using UnityEngine;

public class MultilayerClassifier : MonoBehaviour
{
    // Define network structure
    private int inputSize = 5;     // Number of input features
    private int hiddenSize = 3;    // Number of neurons in hidden layer
    private int outputSize = 3;    // Number of classes (one-hot encoding)

    private float[,] weightsInputToHidden; // Input to hidden layer weights
    private float[,] weightsHiddenToOutput; // Hidden to output layer weights
    private float[] biasesHidden; // Hidden layer biases
    private float[] biasesOutput; // Output layer biases

    private float learningRate = 0.01f;

    // Training data (inputs and labels)
    private float[,] trainingData = {
        { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f },
        { 0.5f, 0.4f, 0.3f, 0.2f, 0.1f },
        { 0.3f, 0.7f, 0.8f, 0.1f, 0.4f }
    };

    private float[,] trainingLabels = {
        { 1, 0, 0 }, // Class 0
        { 0, 1, 0 }, // Class 1
        { 0, 0, 1 }  // Class 2
    };

    // Initialize the network
    void Start()
    {
        InitializeWeights();

        // Train the classifier
        for (int epoch = 0; epoch < 1000; epoch++)
        {
            for (int i = 0; i < trainingData.GetLength(0); i++)
            {
                Train(GetRow(trainingData, i), GetRow(trainingLabels, i));
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
    }

    // Activation function (ReLU for hidden layers)
    private float Activate(float value)
    {
        return Math.Max(0, value);
    }

    // Derivative of ReLU
    private float ActivateDerivative(float value)
    {
        return value > 0 ? 1 : 0;
    }

    // Softmax for output layer
    private float[] Softmax(float[] values)
    {
        float max = Mathf.Max(values);
        float sum = 0f;
        float[] result = new float[values.Length];

        for (int i = 0; i < values.Length; i++)
        {
            result[i] = Mathf.Exp(values[i] - max);
            sum += result[i];
        }

        for (int i = 0; i < values.Length; i++)
        {
            result[i] /= sum;
        }

        return result;
    }

    // Forward pass
    private (float[], float[]) Forward(float[] input)
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
        }

        output = Softmax(output); // Apply softmax to get probabilities

        return (hidden, output);
    }

    // Backpropagation and weight updates
    private void Train(float[] input, float[] target)
    {
        // Forward pass
        var (hidden, output) = Forward(input        );

        // Output layer error
        float[] outputError = new float[outputSize];
        for (int i = 0; i < outputSize; i++)
        {
            outputError[i] = output[i] - target[i]; // Cross-entropy loss gradient
        }

        // Hidden layer error
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

        // Update weights and biases (output layer)
        for (int i = 0; i < hiddenSize; i++)
        {
            for (int j = 0; j < outputSize; j++)
            {
                weightsHiddenToOutput[i, j] -= learningRate * outputError[j] * hidden[i];
            }
        }

        for (int i = 0; i < outputSize; i++)
        {
            biasesOutput[i] -= learningRate * outputError[i];
        }

        // Update weights and biases (hidden layer)
        for (int i = 0; i < inputSize; i++)
        {
            for (int j = 0; j < hiddenSize; j++)
            {
                weightsInputToHidden[i, j] -= learningRate * hiddenError[j] * input[i];
            }
        }

        for (int i = 0; i < hiddenSize; i++)
        {
            biasesHidden[i] -= learningRate * hiddenError[i];
        }
    }

    // Utility function to get a row from a 2D array
    private float[] GetRow(float[,] matrix, int rowIndex)
    {
        float[] row = new float[matrix.GetLength(1)];
        for (int i = 0; i < matrix.GetLength(1); i++)
        {
            row[i] = matrix[rowIndex, i];
        }
        return row;
    }

    // Predict class based on input
    public int Predict(float[] input)
    {
        var (_, output) = Forward(input);
        int predictedClass = 0;
        float maxProbability = output[0];

        for (int i = 1; i < output.Length; i++)
        {
            if (output[i] > maxProbability)
            {
                maxProbability = output[i];
                predictedClass = i;
            }
        }

        return predictedClass;
    }
}

