using System;
using UnityEngine;

public class SpecialGradientDescent : MonoBehaviour
{
    // Parameters
    public float alpha = 0.01f;   // Learning rate
    public float beta = 0.9f;     // Momentum factor
    public int epochs = 100;     // Number of iterations

    private float[] W;           // Weights
    private float[] V;           // Velocity (momentum)

    void Start()
    {
        // Initialize weights and velocity
        int numWeights = 5; // Example weight size
        W = new float[numWeights];
        V = new float[numWeights];
        InitializeWeights(W);

        // Perform optimization
        PerformOptimization();

        // Output final weights
        Debug.Log("Final Weights: " + string.Join(", ", W));
    }

    /// <summary>
    /// Initialize weights randomly.
    /// </summary>
    private void InitializeWeights(float[] weights)
    {
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = UnityEngine.Random.Range(-1.0f, 1.0f);
        }
    }

    /// <summary>
    /// Example loss gradient computation: ∂L/∂W.
    /// Replace this with your actual gradient computation.
    /// </summary>
    private float[] ComputeGradients(float[] weights)
    {
        float[] gradients = new float[weights.Length];
        for (int i = 0; i < weights.Length; i++)
        {
            gradients[i] = 2 * weights[i]; // Example gradient for L = W^2
        }
        return gradients;
    }

    /// <summary>
    /// Perform optimization using gradient descent with momentum.
    /// </summary>
    private void PerformOptimization()
    {
        for (int epoch = 0; epoch < epochs; epoch++)
        {
            // Compute gradients
            float[] gradients = ComputeGradients(W);

            // Update velocity: V ← βV - α∂L/∂W
            for (int i = 0; i < V.Length; i++)
            {
                V[i] = beta * V[i] - alpha * gradients[i];
            }

            // Update weights: W ← W + V
            for (int i = 0; i < W.Length; i++)
            {
                W[i] += V[i];
            }

            // Debug: Print weights at each epoch
            Debug.Log($"Epoch {epoch + 1}: Weights = {string.Join(", ", W)}");
        }
    }
}
