using System;
using UnityEngine;

public class NormalizationGradients : MonoBehaviour
{
    public int dimension = 2;  // Number of features
    public int samples = 10;  // Number of samples
    public float epsilon = 1e-5f;  // Small constant for stability

    private float[] beta;   // Learnable parameter beta
    private float[] gamma;  // Learnable parameter gamma
    private float[][] inputs; // Input values
    private float[][] normalized; // Normalized values
    private float[] mean;
    private float[] variance;

    void Start()
    {
        // Initialize parameters
        beta = new float[dimension];
        gamma = new float[dimension];
        InitializeParameters(beta);
        InitializeParameters(gamma);

        // Generate inputs
        inputs = GenerateInputs(samples, dimension);

        // Compute mean and variance
        (mean, variance) = ComputeMeanAndVariance(inputs);

        // Normalize inputs
        normalized = Normalize(inputs, mean, variance);

        // Compute gradients
        ComputeGradients();
    }

    /// <summary>
    /// Initialize parameters randomly.
    /// </summary>
    private void InitializeParameters(float[] parameters)
    {
        for (int i = 0; i < parameters.Length; i++)
        {
            parameters[i] = UnityEngine.Random.Range(-1.0f, 1.0f);
        }
    }

    /// <summary>
    /// Generate random inputs.
    /// </summary>
    private float[][] GenerateInputs(int samples, int dimension)
    {
        float[][] inputs = new float[samples][];
        for (int i = 0; i < samples; i++)
        {
            inputs[i] = new float[dimension];
            for (int j = 0; j < dimension; j++)
            {
                inputs[i][j] = UnityEngine.Random.Range(-1.0f, 1.0f);
            }
        }
        return inputs;
    }

    /// <summary>
    /// Compute mean and variance for normalization.
    /// </summary>
    private (float[], float[]) ComputeMeanAndVariance(float[][] inputs)
    {
        int samples = inputs.Length;
        int dimension = inputs[0].Length;

        float[] mean = new float[dimension];
        float[] variance = new float[dimension];

        // Compute mean
        for (int j = 0; j < dimension; j++)
        {
            for (int i = 0; i < samples; i++)
            {
                mean[j] += inputs[i][j];
            }
            mean[j] /= samples;
        }

        // Compute variance
        for (int j = 0; j < dimension; j++)
        {
            for (int i = 0; i < samples; i++)
            {
                variance[j] += Mathf.Pow(inputs[i][j] - mean[j], 2);
            }
            variance[j] /= samples;
        }

        return (mean, variance);
    }

    /// <summary>
    /// Normalize inputs.
    /// </summary>
    private float[][] Normalize(float[][] inputs, float[] mean, float[] variance)
    {
        int samples = inputs.Length;
        int dimension = inputs[0].Length;
        float[][] normalized = new float[samples][];

        for (int i = 0; i < samples; i++)
        {
            normalized[i] = new float[dimension];
            for (int j = 0; j < dimension; j++)
            {
                normalized[i][j] = (inputs[i][j] - mean[j]) / Mathf.Sqrt(variance[j] + epsilon);
            }
        }
        return normalized;
    }

    /// <summary>
    /// Compute gradients for beta and gamma.
    /// </summary>
    private void ComputeGradients()
    {
        float[] dL_dBeta = new float[dimension];
        float[] dL_dGamma = new float[dimension];

        for (int j = 0; j < dimension; j++)
        {
            for (int i = 0; i < samples; i++)
            {
                dL_dBeta[j] += normalized[i][j];
                dL_dGamma[j] += normalized[i][j] * inputs[i][j];
            }
        }

        Debug.Log("Gradient w.r.t Beta: " + string.Join(", ", dL_dBeta));
        Debug.Log("Gradient w.r.t Gamma: " + string.Join(", ", dL_dGamma));
    }

    private (float bias, float variance, float noise) BiasVarianceDecomposition(float[][] inputs, float[] predictions, float[] groundTruth)
    {
        int samples = inputs.Length;
        float bias = 0;
        float variance = 0;
        float noise = 0;

        // Compute Bias^2
        for (int i = 0; i < samples; i++)
        {
            bias += Mathf.Pow(predictions[i] - groundTruth[i], 2);
        }
        bias /= samples;

        // Compute Variance
        float meanPrediction = 0;
        for (int i = 0; i < samples; i++)
        {
            meanPrediction += predictions[i];
        }
        meanPrediction /= samples;

        for (int i = 0; i < samples; i++)
        {
            variance += Mathf.Pow(predictions[i] - meanPrediction, 2);
        }
        variance /= samples;

        // Compute Noise
        for (int i = 0; i < samples; i++)
        {
            noise += Mathf.Pow(groundTruth[i] - meanPrediction, 2);
        }
        noise /= samples;

        return (bias, variance, noise);
    }

}
