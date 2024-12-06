using System;
using UnityEngine;

public class TaylorApproximation : MonoBehaviour
{
    // Example parameters
    public int dimension = 2; // Dimensionality of W
    public float alpha = 0.01f; // Learning rate
    public float[] W0; // Initial point
    private float[,] Hessian; // Hessian matrix

    void Start()
    {
        // Initialize weights and Hessian
        W0 = new float[dimension];
        InitializeWeights(W0);
        Hessian = ComputeHessian(W0);

        // Compute the Taylor approximation at a new point
        float[] W = { 1.0f, 1.5f }; // Example new point
        float lossApproximation = TaylorExpand(W0, W);

        // Output results
        Debug.Log($"Taylor Approximation of L(W): {lossApproximation}");
    }

    /// <summary>
    /// Example loss function: L(W) = W^T W (L = ||W||^2)
    /// </summary>
    private float Loss(float[] W)
    {
        float loss = 0;
        foreach (float w in W)
        {
            loss += w * w;
        }
        return loss;
    }

    /// <summary>
    /// Gradient of the loss function: ∇L(W) = 2W
    /// </summary>
    private float[] Gradient(float[] W)
    {
        float[] grad = new float[W.Length];
        for (int i = 0; i < W.Length; i++)
        {
            grad[i] = 2 * W[i];
        }
        return grad;
    }

    /// <summary>
    /// Compute the Hessian matrix: H = 2I (for L = ||W||^2)
    /// </summary>
    private float[,] ComputeHessian(float[] W)
    {
        float[,] hessian = new float[W.Length, W.Length];
        for (int i = 0; i < W.Length; i++)
        {
            for (int j = 0; j < W.Length; j++)
            {
                hessian[i, j] = i == j ? 2.0f : 0.0f; // Identity matrix scaled by 2
            }
        }
        return hessian;
    }

    /// <summary>
    /// Taylor series approximation of L(W)
    /// </summary>
    private float TaylorExpand(float[] W0, float[] W)
    {
        // Loss at W0
        float L_W0 = Loss(W0);

        // Gradient at W0
        float[] grad_W0 = Gradient(W0);

        // First-order term: (W - W0)^T ∇L(W0)
        float firstOrderTerm = 0;
        for (int i = 0; i < W0.Length; i++)
        {
            firstOrderTerm += (W[i] - W0[i]) * grad_W0[i];
        }

        // Second-order term: 0.5 * (W - W0)^T H (W - W0)
        float secondOrderTerm = 0;
        float[] diff = new float[W.Length];
        for (int i = 0; i < W.Length; i++)
        {
            diff[i] = W[i] - W0[i];
        }

        for (int i = 0; i < W.Length; i++)
        {
            for (int j = 0; j < W.Length; j++)
            {
                secondOrderTerm += 0.5f * diff[i] * Hessian[i, j] * diff[j];
            }
        }

        // Combine terms
        return L_W0 + firstOrderTerm + secondOrderTerm;
    }

    /// <summary>
    /// Initialize weights randomly.
    /// </summary>
    private void InitializeWeights(float[] W)
    {
        for (int i = 0; i < W.Length; i++)
        {
            W[i] = UnityEngine.Random.Range(-1.0f, 1.0f);
        }
    }
}
