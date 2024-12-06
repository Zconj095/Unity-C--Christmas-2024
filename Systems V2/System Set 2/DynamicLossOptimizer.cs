using UnityEngine;

public class DynamicLossOptimizer : MonoBehaviour
{
    public int inputDimension = 5; // Number of input nodes (d)
    public int hiddenDimension = 3; // Number of hidden nodes (k)
    public float lambda = 0.01f; // Regularization weight
    public float learningRate = 0.001f;

    private float[] input; // Input data (X)
    private float[] output; // Output data (X')
    private float[] hidden; // Hidden layer (h)
    private float[] mean; // Mean (μ)
    private float[] stdDev; // Standard deviation (σ)
    private float[,] weightsInputHidden; // Weights from input to hidden layer
    private float[,] weightsHiddenOutput; // Weights from hidden to output layer

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        input = new float[inputDimension];
        output = new float[inputDimension];
        hidden = new float[hiddenDimension];
        mean = new float[inputDimension];
        stdDev = new float[inputDimension];

        weightsInputHidden = new float[inputDimension, hiddenDimension];
        weightsHiddenOutput = new float[hiddenDimension, inputDimension];

        // Random initialization of weights
        InitializeWeights(weightsInputHidden);
        InitializeWeights(weightsHiddenOutput);
    }

    private void InitializeWeights(float[,] weights)
    {
        for (int i = 0; i < weights.GetLength(0); i++)
        {
            for (int j = 0; j < weights.GetLength(1); j++)
            {
                weights[i, j] = Random.Range(-0.1f, 0.1f);
            }
        }
    }

    private void ForwardPass()
    {
        // Calculate hidden layer (h = sigmoid(WX))
        for (int j = 0; j < hiddenDimension; j++)
        {
            hidden[j] = 0f;
            for (int i = 0; i < inputDimension; i++)
            {
                hidden[j] += input[i] * weightsInputHidden[i, j];
            }
            hidden[j] = Sigmoid(hidden[j]);
        }

        // Calculate output layer (X' = sigmoid(W'h))
        for (int i = 0; i < inputDimension; i++)
        {
            output[i] = 0f;
            for (int j = 0; j < hiddenDimension; j++)
            {
                output[i] += hidden[j] * weightsHiddenOutput[j, i];
            }
            output[i] = Sigmoid(output[i]);
        }
    }

    private void BackwardPass()
    {
        // Calculate gradients and update weights
        float[,] gradInputHidden = new float[inputDimension, hiddenDimension];
        float[,] gradHiddenOutput = new float[hiddenDimension, inputDimension];

        // Loss gradients
        for (int i = 0; i < inputDimension; i++)
        {
            for (int j = 0; j < hiddenDimension; j++)
            {
                gradInputHidden[i, j] = input[i] * GradientHidden(hidden[j], i) + 
                                        lambda * weightsInputHidden[i, j] * Mathf.Pow(hidden[j] * (1 - hidden[j]), 2);
            }
        }

        for (int j = 0; j < hiddenDimension; j++)
        {
            for (int i = 0; i < inputDimension; i++)
            {
                gradHiddenOutput[j, i] = hidden[j] * (input[i] - output[i]) + 
                                         lambda * weightsHiddenOutput[j, i] * Mathf.Pow(hidden[j] * (1 - hidden[j]), 2);
            }
        }

        // Update weights using gradient descent
        UpdateWeights(weightsInputHidden, gradInputHidden);
        UpdateWeights(weightsHiddenOutput, gradHiddenOutput);
    }

    private void UpdateWeights(float[,] weights, float[,] gradients)
    {
        for (int i = 0; i < weights.GetLength(0); i++)
        {
            for (int j = 0; j < weights.GetLength(1); j++)
            {
                weights[i, j] -= learningRate * gradients[i, j];
            }
        }
    }

    private float Sigmoid(float x)
    {
        return 1f / (1f + Mathf.Exp(-x));
    }

    private float GradientHidden(float h, int i)
    {
        return weightsInputHidden[i, 0] * h * (1 - h);
    }

    private void Update()
    {
        ForwardPass();
        BackwardPass();
    }
}
