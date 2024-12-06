using System;
using UnityEngine;

public class GradientAdjustment : MonoBehaviour
{
    public int dimension = 2;  // Number of dimensions
    public float beta = 0.9f;  // Momentum coefficient
    public float learningRate = 0.01f;  // Learning rate
    public float delta = 1e-6f;  // Small value for stability

    private float[,] G;  // Scaling matrix
    private float[] W;   // Parameters
    private float[] gradient; // Current gradient
    private float[] prevGradient; // Previous gradient

    void Start()
    {
        // Initialize parameters
        G = InitializeScalingMatrix(dimension);
        W = InitializeWeights(dimension);
        gradient = ComputeGradient(W);
        prevGradient = new float[dimension];

        // Perform one optimization step
        PerformUpdate();

        Debug.Log("Updated Weights: " + string.Join(", ", W));
    }

    /// <summary>
    /// Initialize scaling matrix G as an identity matrix.
    /// </summary>
    private float[,] InitializeScalingMatrix(int size)
    {
        float[,] matrix = new float[size, size];
        for (int i = 0; i < size; i++)
        {
            matrix[i, i] = 1.0f;
        }
        return matrix;
    }

    /// <summary>
    /// Initialize weights randomly.
    /// </summary>
    private float[] InitializeWeights(int size)
    {
        float[] weights = new float[size];
        for (int i = 0; i < size; i++)
        {
            weights[i] = UnityEngine.Random.Range(-1.0f, 1.0f);
        }
        return weights;
    }

    /// <summary>
    /// Compute the gradient for the loss function.
    /// </summary>
    private float[] ComputeGradient(float[] weights)
    {
        float[] grad = new float[weights.Length];
        for (int i = 0; i < weights.Length; i++)
        {
            grad[i] = 2 * weights[i];  // Example gradient for L(W) = ||W||^2
        }
        return grad;
    }

    /// <summary>
    /// Perform one parameter update step.
    /// </summary>
    private void PerformUpdate()
    {
        float[] gradientChange = new float[dimension];
        for (int i = 0; i < dimension; i++)
        {
            gradientChange[i] = gradient[i] - prevGradient[i];
        }

        // Update scaling matrix G
        float[,] newG = UpdateScalingMatrix(G, gradientChange);

        // Update weights
        for (int i = 0; i < dimension; i++)
        {
            W[i] -= learningRate * gradient[i];
        }

        // Update previous gradient
        Array.Copy(gradient, prevGradient, dimension);
        G = newG;
    }

    /// <summary>
    /// Update the scaling matrix G.
    /// </summary>
    private float[,] UpdateScalingMatrix(float[,] G, float[] gradientChange)
    {
        int size = G.GetLength(0);
        float[,] newG = new float[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                newG[i, j] = G[i, j]; // Example update
            }
        }
        return newG;
    }

    private float[] RecursiveAverage(float[] currentWeights, float[] previousAverage, float beta)
    {
        float[] newAverage = new float[currentWeights.Length];
        for (int i = 0; i < currentWeights.Length; i++)
        {
            newAverage[i] = (1 - beta) * currentWeights[i] + beta * previousAverage[i];
        }
        return newAverage;
    }

    private (float[] mean, float[] variance) ComputeMeanAndVariance(float[][] values)
    {
        int size = values[0].Length;
        int samples = values.Length;
        float[] mean = new float[size];
        float[] variance = new float[size];

        // Compute mean
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < samples; j++)
            {
                mean[i] += values[j][i];
            }
            mean[i] /= samples;
        }

        // Compute variance
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < samples; j++)
            {
                variance[i] += Mathf.Pow(values[j][i] - mean[i], 2);
            }
            variance[i] /= samples;
            variance[i] += 1e-6f; // Stability
        }

        return (mean, variance);
    }

    private float[] Normalize(float[] values, float[] mean, float[] variance, float gamma, float beta)
    {
        float[] normalized = new float[values.Length];
        for (int i = 0; i < values.Length; i++)
        {
            normalized[i] = gamma * (values[i] - mean[i]) / Mathf.Sqrt(variance[i]) + beta;
        }
        return normalized;
    }

}
