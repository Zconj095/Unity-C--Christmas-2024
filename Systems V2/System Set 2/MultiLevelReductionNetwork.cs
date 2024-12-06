using System;

public class MultiLevelReductionNetwork
{
    private int inputSize;       // Input layer size
    private int firstLevelSize; // First-level reduced size
    private int secondLevelSize; // Second-level reduced size
    private int outputSize;      // Output layer size

    // Weights and biases for first-level reduction
    private float[,] weightsInputToFirstLevel;
    private float[] biasesFirstLevel;

    // Weights and biases for second-level reduction
    private float[,] weightsFirstToSecondLevel;
    private float[] biasesSecondLevel;

    // Weights and biases for second-level to output
    private float[,] weightsSecondToOutput;
    private float[] biasesOutput;

    private float learningRate;

    public MultiLevelReductionNetwork(int inputSize, int firstLevelSize, int secondLevelSize, int outputSize, float learningRate = 0.01f)
    {
        this.inputSize = inputSize;
        this.firstLevelSize = firstLevelSize;
        this.secondLevelSize = secondLevelSize;
        this.outputSize = outputSize;
        this.learningRate = learningRate;

        // Initialize weights and biases for all layers
        weightsInputToFirstLevel = InitializeMatrix(inputSize, firstLevelSize);
        biasesFirstLevel = InitializeArray(firstLevelSize);

        weightsFirstToSecondLevel = InitializeMatrix(firstLevelSize, secondLevelSize);
        biasesSecondLevel = InitializeArray(secondLevelSize);

        weightsSecondToOutput = InitializeMatrix(secondLevelSize, outputSize);
        biasesOutput = InitializeArray(outputSize);
    }

    // Pretraining for first-level reduction
    public float[] PretrainFirstLevel(float[] input)
    {
        float[] firstLevelOutput = new float[firstLevelSize];
        for (int i = 0; i < firstLevelSize; i++)
        {
            firstLevelOutput[i] = biasesFirstLevel[i];
            for (int j = 0; j < inputSize; j++)
            {
                firstLevelOutput[i] += input[j] * weightsInputToFirstLevel[j, i];
            }
            firstLevelOutput[i] = Activate(firstLevelOutput[i]);  // Activation function
        }
        return firstLevelOutput;
    }

    // Pretraining for second-level reduction
    public float[] PretrainSecondLevel(float[] firstLevelOutput)
    {
        float[] secondLevelOutput = new float[secondLevelSize];
        for (int i = 0; i < secondLevelSize; i++)
        {
            secondLevelOutput[i] = biasesSecondLevel[i];
            for (int j = 0; j < firstLevelSize; j++)
            {
                secondLevelOutput[i] += firstLevelOutput[j] * weightsFirstToSecondLevel[j, i];
            }
            secondLevelOutput[i] = Activate(secondLevelOutput[i]);  // Activation function
        }
        return secondLevelOutput;
    }

    // Forward pass: from input to output
    public float[] Forward(float[] input)
    {
        // Pretraining steps
        float[] firstLevelOutput = PretrainFirstLevel(input);
        float[] secondLevelOutput = PretrainSecondLevel(firstLevelOutput);

        // Final output computation
        float[] output = new float[outputSize];
        for (int i = 0; i < outputSize; i++)
        {
            output[i] = biasesOutput[i];
            for (int j = 0; j < secondLevelSize; j++)
            {
                output[i] += secondLevelOutput[j] * weightsSecondToOutput[j, i];
            }
            output[i] = Activate(output[i]);  // Activation function
        }
        return output;
    }

    // Backpropagation to train all layers
    public void Backpropagate(float[] input, float[] target)
    {
        // Forward pass
        float[] firstLevelOutput = PretrainFirstLevel(input);
        float[] secondLevelOutput = PretrainSecondLevel(firstLevelOutput);
        float[] output = Forward(input);

        // Output layer error
        float[] outputError = new float[outputSize];
        for (int i = 0; i < outputSize; i++)
        {
            outputError[i] = output[i] - target[i];
        }

        // Second-level layer error
        float[] secondLevelError = new float[secondLevelSize];
        for (int i = 0; i < secondLevelSize; i++)
        {
            secondLevelError[i] = 0;
            for (int j = 0; j < outputSize; j++)
            {
                secondLevelError[i] += outputError[j] * weightsSecondToOutput[i, j];
            }
            secondLevelError[i] *= ActivateDerivative(secondLevelOutput[i]);
        }

        // First-level layer error
        float[] firstLevelError = new float[firstLevelSize];
        for (int i = 0; i < firstLevelSize; i++)
        {
            firstLevelError[i] = 0;
            for (int j = 0; j < secondLevelSize; j++)
            {
                firstLevelError[i] += secondLevelError[j] * weightsFirstToSecondLevel[i, j];
            }
            firstLevelError[i] *= ActivateDerivative(firstLevelOutput[i]);
        }

        // Update weights and biases for second-level to output
        for (int i = 0; i < secondLevelSize; i++)
        {
            for (int j = 0; j < outputSize; j++)
            {
                weightsSecondToOutput[i, j] -= learningRate * outputError[j] * secondLevelOutput[i];
            }
        }
        for (int i = 0; i < outputSize; i++)
        {
            biasesOutput[i] -= learningRate * outputError[i];
        }

        // Update weights and biases for first-level to second-level
        for (int i = 0; i < firstLevelSize; i++)
        {
            for (int j = 0; j < secondLevelSize; j++)
            {
                weightsFirstToSecondLevel[i, j] -= learningRate * secondLevelError[j] * firstLevelOutput[i];
            }
        }
        for (int i = 0; i < secondLevelSize; i++)
        {
            biasesSecondLevel[i] -= learningRate * secondLevelError[i];
        }

        // Update weights and biases for input to first-level
        for (int i = 0; i < inputSize; i++)
        {
            for (int j = 0; j < firstLevelSize; j++)
            {
                weightsInputToFirstLevel[i, j] -= learningRate * firstLevelError[j] * input[i];
            }
        }
        for (int i = 0; i < firstLevelSize; i++)
        {
            biasesFirstLevel[i] -= learningRate * firstLevelError[i];
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
