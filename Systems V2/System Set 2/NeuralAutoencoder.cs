using UnityEngine;
using System.Collections.Generic;

public class NeuralAutoencoder : MonoBehaviour
{
    public int inputDimension = 5;
    public int hiddenDimension = 3;
    public float learningRate = 0.01f;
    public float lambda = 0.01f;

    private float[,] weightsInToHidden;
    private float[,] weightsHiddenToOut;
    private float[] hiddenNodes;
    private float[] outputNodes;
    private float[] inputNodes;

    void Start()
    {
        InitializeNetwork();
        TrainModel();
    }

    void InitializeNetwork()
    {
        weightsInToHidden = InitializeWeights(inputDimension, hiddenDimension);
        weightsHiddenToOut = InitializeWeights(hiddenDimension, inputDimension);

        hiddenNodes = new float[hiddenDimension];
        outputNodes = new float[inputDimension];
        inputNodes = new float[inputDimension];

        // Example input
        for (int i = 0; i < inputDimension; i++)
            inputNodes[i] = Random.Range(0f, 1f);
    }

    float[,] InitializeWeights(int rows, int cols)
    {
        float[,] weights = new float[rows, cols];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                weights[i, j] = Random.Range(-0.1f, 0.1f);

        return weights;
    }

    void TrainModel()
    {
        for (int epoch = 0; epoch < 1000; epoch++)
        {
            ForwardStep();
            float error = ComputeError();
            BackwardStep();

            if (epoch % 100 == 0)
                Debug.Log($"Epoch {epoch}, Loss: {error}");
        }
    }

    void ForwardStep()
    {
        // Compute hidden layer activations
        for (int j = 0; j < hiddenDimension; j++)
        {
            hiddenNodes[j] = 0f;
            for (int i = 0; i < inputDimension; i++)
                hiddenNodes[j] += inputNodes[i] * weightsInToHidden[i, j];
            hiddenNodes[j] = Sigmoid(hiddenNodes[j]);
        }

        // Compute output layer activations
        for (int i = 0; i < inputDimension; i++)
        {
            outputNodes[i] = 0f;
            for (int j = 0; j < hiddenDimension; j++)
                outputNodes[i] += hiddenNodes[j] * weightsHiddenToOut[j, i];
            outputNodes[i] = Sigmoid(outputNodes[i]);
        }
    }

    float ComputeError()
    {
        // Reconstruction loss
        float reconstructionLoss = 0f;
        for (int i = 0; i < inputDimension; i++)
        {
            reconstructionLoss += Mathf.Pow(inputNodes[i] - outputNodes[i], 2);
        }

        // Regularization term
        float regularizationLoss = 0f;
        for (int i = 0; i < inputDimension; i++)
            for (int j = 0; j < hiddenDimension; j++)
                regularizationLoss += Mathf.Pow(weightsInToHidden[i, j], 2);

        return reconstructionLoss + lambda * regularizationLoss;
    }

    void BackwardStep()
    {
        // Output layer gradients
        float[] outputGradients = new float[inputDimension];
        for (int i = 0; i < inputDimension; i++)
        {
            float error = outputNodes[i] - inputNodes[i];
            outputGradients[i] = error * SigmoidDerivative(outputNodes[i]);
        }

        // Hidden layer gradients
        float[] hiddenGradients = new float[hiddenDimension];
        for (int j = 0; j < hiddenDimension; j++)
        {
            float error = 0f;
            for (int i = 0; i < inputDimension; i++)
                error += outputGradients[i] * weightsHiddenToOut[j, i];

            hiddenGradients[j] = error * SigmoidDerivative(hiddenNodes[j]);
        }

        // Update weights for hidden to output
        for (int j = 0; j < hiddenDimension; j++)
            for (int i = 0; i < inputDimension; i++)
                weightsHiddenToOut[j, i] -= learningRate * outputGradients[i] * hiddenNodes[j];

        // Update weights for input to hidden
        for (int i = 0; i < inputDimension; i++)
            for (int j = 0; j < hiddenDimension; j++)
                weightsInToHidden[i, j] -= learningRate * hiddenGradients[j] * inputNodes[i];
    }

    float Sigmoid(float x)
    {
        return 1f / (1f + Mathf.Exp(-x));
    }

    float SigmoidDerivative(float x)
    {
        return x * (1f - x);
    }
}
