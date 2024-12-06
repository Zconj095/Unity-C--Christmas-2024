using System;

public class PretrainingReductionNetwork
{
    private int inputSize;
    private int reducedSize;  // Size of the reduced representation
    private int outputSize;

    // Weights and biases for input to reduced layer
    private float[,] weightsInputToReduced;
    private float[] biasesReduced;

    // Weights and biases for reduced to output layer
    private float[,] weightsReducedToOutput;
    private float[] biasesOutput;

    private float learningRate;

    public PretrainingReductionNetwork(int inputSize, int reducedSize, int outputSize, float learningRate = 0.01f)
    {
        this.inputSize = inputSize;
        this.reducedSize = reducedSize;
        this.outputSize = outputSize;
        this.learningRate = learningRate;

        // Initialize weights and biases randomly
        weightsInputToReduced = InitializeMatrix(inputSize, reducedSize);
        biasesReduced = InitializeArray(reducedSize);

        weightsReducedToOutput = InitializeMatrix(reducedSize, outputSize);
        biasesOutput = InitializeArray(outputSize);
    }

    // Pretraining stage: learn reduced representation
    public float[] Pretrain(float[] input)
    {
        float[] reducedOutput = new float[reducedSize];
        for (int i = 0; i < reducedSize; i++)
        {
            reducedOutput[i] = biasesReduced[i];
            for (int j = 0; j < inputSize; j++)
            {
                reducedOutput[i] += input[j] * weightsInputToReduced[j, i];
            }
            reducedOutput[i] = Activate(reducedOutput[i]);  // Activation function
        }
        return reducedOutput;
    }

    // Fine-tune stage: Use reduced representation to train output weights
    public float[] Forward(float[] input)
    {
        float[] reduced = Pretrain(input);  // Use pretrained reduction
        float[] output = new float[outputSize];
        for (int i = 0; i < outputSize; i++)
        {
            output[i] = biasesOutput[i];
            for (int j = 0; j < reducedSize; j++)
            {
                output[i] += reduced[j] * weightsReducedToOutput[j, i];
            }
            output[i] = Activate(output[i]);  // Activation function
        }
        return output;
    }

    // Backpropagation to train both layers
    public void Backpropagate(float[] input, float[] target)
    {
        // Forward pass
        float[] reduced = Pretrain(input);
        float[] output = Forward(input);

        // Output layer error
        float[] outputError = new float[outputSize];
        for (int i = 0; i < outputSize; i++)
        {
            outputError[i] = output[i] - target[i];
        }

        // Hidden (reduced) layer error
        float[] reducedError = new float[reducedSize];
        for (int i = 0; i < reducedSize; i++)
        {
            reducedError[i] = 0;
            for (int j = 0; j < outputSize; j++)
            {
                reducedError[i] += outputError[j] * weightsReducedToOutput[i, j];
            }
            reducedError[i] *= ActivateDerivative(reduced[i]);
        }

        // Update weights and biases for reduced to output layer
        for (int i = 0; i < reducedSize; i++)
        {
            for (int j = 0; j < outputSize; j++)
            {
                weightsReducedToOutput[i, j] -= learningRate * outputError[j] * reduced[i];
            }
        }

        for (int i = 0; i < outputSize; i++)
        {
            biasesOutput[i] -= learningRate * outputError[i];
        }

        // Update weights and biases for input to reduced layer
        for (int i = 0; i < inputSize; i++)
        {
            for (int j = 0; j < reducedSize; j++)
            {
                weightsInputToReduced[i, j] -= learningRate * reducedError[j] * input[i];
            }
        }

        for (int i = 0; i < reducedSize; i++)
        {
            biasesReduced[i] -= learningRate * reducedError[i];
        }
    }

    // Activation function (ReLU)
    private float Activate(float x)
    {
        return Math.Max(0, x);
    }

    // Derivative of ReLU
    private float ActivateDerivative(float x)
    {
        return x > 0 ? 1 : 0;
    }

    // Utility to initialize a matrix with random values
    private float[,] InitializeMatrix(int rows, int cols)
    {
        Random rand = new Random();
        float[,] matrix = new float[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = (float)rand.NextDouble() - 0.5f; // Random values between -0.5 and 0.5
            }
        }
        return matrix;
    }

    // Utility to initialize an array with random values
    private float[] InitializeArray(int size)
    {
        Random rand = new Random();
        float[] array = new float[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = (float)rand.NextDouble() - 0.5f; // Random values between -0.5 and 0.5
        }
        return array;
    }
}
