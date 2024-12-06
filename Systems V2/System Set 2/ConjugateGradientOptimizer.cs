using System;
using UnityEngine;

public class SpecialConjugateGradientOptimizer : MonoBehaviour
{
    public int dimension = 2;       // Dimensionality of W
    public int maxIterations = 10; // Maximum iterations
    public float tolerance = 1e-6f; // Convergence tolerance
    public float alpha = 0.1f;      // Learning rate
    private float[,] H;             // Hessian matrix
    private float[] b;              // Bias vector
    private float[] W;              // Current weights
    private float[] gradient;       // Gradient of the loss function

    void Start()
    {
        // Initialize Hessian, bias, and weights
        H = InitializeHessian(dimension);
        b = InitializeBias(dimension);
        W = InitializeWeights(dimension);

        // Perform optimization
        Optimize();

        // Output final weights
        Debug.Log("Final Weights: " + string.Join(", ", W));
    }

    /// <summary>
    /// Initialize a symmetric positive definite Hessian matrix.
    /// </summary>
    private float[,] InitializeHessian(int size)
    {
        float[,] matrix = new float[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                matrix[i, j] = i == j ? 2.0f : 0.5f; // Example positive definite matrix
            }
        }
        return matrix;
    }

    /// <summary>
    /// Initialize bias vector.
    /// </summary>
    private float[] InitializeBias(int size)
    {
        float[] vector = new float[size];
        for (int i = 0; i < size; i++)
        {
            vector[i] = UnityEngine.Random.Range(-1.0f, 1.0f);
        }
        return vector;
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
    /// Compute the gradient of the loss function: ∇L(W) = H W - b
    /// </summary>
    private float[] ComputeGradient(float[] W)
    {
        int size = W.Length;
        float[] grad = new float[size];
        for (int i = 0; i < size; i++)
        {
            grad[i] = 0;
            for (int j = 0; j < size; j++)
            {
                grad[i] += H[i, j] * W[j];
            }
            grad[i] -= b[i];
        }
        return grad;
    }

    /// <summary>
    /// Perform Conjugate Gradient Optimization
    /// </summary>
    private void Optimize()
    {
        int size = W.Length;
        gradient = ComputeGradient(W);
        float[] q = new float[size];
        Array.Copy(gradient, q, size);

        for (int iteration = 0; iteration < maxIterations; iteration++)
        {
            // Compute step size alpha_t
            float alpha_t = ComputeStepSize(q);

            // Update weights: W_t+1 = W_t - alpha_t * q
            for (int i = 0; i < size; i++)
            {
                W[i] -= alpha_t * q[i];
            }

            // Compute new gradient
            float[] newGradient = ComputeGradient(W);

            // Check for convergence
            float gradientNorm = VectorNorm(newGradient);
            if (gradientNorm < tolerance)
            {
                Debug.Log($"Converged at iteration {iteration + 1}");
                break;
            }

            // Compute beta_t
            float beta_t = ComputeBeta(q, gradient, newGradient);

            // Update conjugate direction: q_t+1 = -∇L(W_t+1) + beta_t * q
            for (int i = 0; i < size; i++)
            {
                q[i] = -newGradient[i] + beta_t * q[i];
            }

            // Update gradient
            gradient = newGradient;

            Debug.Log($"Iteration {iteration + 1}: W = {string.Join(", ", W)}");
        }
    }

    /// <summary>
    /// Compute step size: alpha_t = (q^T q) / (q^T H q)
    /// </summary>
    private float ComputeStepSize(float[] q)
    {
        float numerator = SpecialDotProduct(q, q);
        float denominator = 0;
        for (int i = 0; i < q.Length; i++)
        {
            for (int j = 0; j < q.Length; j++)
            {
                denominator += q[i] * H[i, j] * q[j];
            }
        }
        return numerator / denominator;
    }

    /// <summary>
    /// Compute beta_t = (∇L(W_t+1)^T H q) / (q^T H q)
    /// </summary>
    private float ComputeBeta(float[] q, float[] oldGradient, float[] newGradient)
    {
        float numerator = 0;
        float denominator = 0;

        for (int i = 0; i < q.Length; i++)
        {
            for (int j = 0; j < q.Length; j++)
            {
                numerator += newGradient[i] * H[i, j] * q[j];
                denominator += q[i] * H[i, j] * q[j];
            }
        }
        return numerator / denominator;
    }

    /// <summary>
    /// Compute the dot product of two vectors.
    /// </summary>
    private float SpecialDotProduct(float[] a, float[] b)
    {
        float result = 0;
        for (int i = 0; i < a.Length; i++)
        {
            result += a[i] * b[i];
        }
        return result;
    }

    /// <summary>
    /// Compute the norm of a vector.
    /// </summary>
    private float VectorNorm(float[] vector)
    {
        float sum = 0;
        foreach (float v in vector)
        {
            sum += v * v;
        }
        return Mathf.Sqrt(sum);
    }
}
