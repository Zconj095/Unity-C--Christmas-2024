using System;
using UnityEngine;

public class RecursiveGradient : MonoBehaviour
{
    // Example Parameters
    public float theta = 0.5f;          // Parameter (θ)
    public float dTheta = 0.01f;        // Small change in θ (Δθ)
    public int timeSteps = 5;           // Number of recursion steps
    public float initialX = 1.0f;       // Initial state (x_t)
    public float u = 2.0f;              // Input (u_t)

    void Start()
    {
        // Calculate the recursive gradient dx_t
        float dx_t = ComputeRecursiveGradient(ExampleFunction, initialX, u, theta, dTheta, timeSteps);

        // Log the result
        Debug.Log($"Recursive Gradient (dx_t): {dx_t}");
    }

    /// <summary>
    /// Example function F(x, u, θ): θ * x^2 + u
    /// </summary>
    float ExampleFunction(float x, float u, float theta)
    {
        return theta * Mathf.Pow(x, 2) + u;
    }

    /// <summary>
    /// Computes the recursive gradient dx_t based on the provided formula.
    /// </summary>
    /// <param name="F">Function F(x, u, θ)</param>
    /// <param name="x_t">Initial state x_t</param>
    /// <param name="u_t">Input u_t</param>
    /// <param name="theta">Parameter θ</param>
    /// <param name="dTheta">Perturbation Δθ</param>
    /// <param name="timeSteps">Number of recursion steps</param>
    /// <returns>Recursive gradient dx_t</returns>
    float ComputeRecursiveGradient(Func<float, float, float, float> F, float x_t, float u_t, float theta, float dTheta, int timeSteps)
    {
        float dx_t = 0.0f; // Initialize gradient dx_t

        for (int t = 0; t < timeSteps; t++)
        {
            // Compute gradients
            float gradTheta = ComputeGradientTheta(F, x_t, u_t, theta);
            float gradX = ComputeGradientX(F, x_t, u_t, theta);

            // Update dx_t recursively
            dx_t = gradTheta * dTheta + gradX * dx_t;

            // Update x_t for the next step (if needed)
            x_t = F(x_t, u_t, theta);
        }

        return dx_t;
    }

    /// <summary>
    /// Approximates the gradient ∇_θ F(x, u, θ) using finite differences.
    /// </summary>
    float ComputeGradientTheta(Func<float, float, float, float> F, float x, float u, float theta)
    {
        float epsilon = 1e-5f; // Small change for finite difference approximation
        float F1 = F(x, u, theta + epsilon);
        float F2 = F(x, u, theta);
        return (F1 - F2) / epsilon;
    }

    /// <summary>
    /// Approximates the gradient ∇_x F(x, u, θ) using finite differences.
    /// </summary>
    float ComputeGradientX(Func<float, float, float, float> F, float x, float u, float theta)
    {
        float epsilon = 1e-5f; // Small change for finite difference approximation
        float F1 = F(x + epsilon, u, theta);
        float F2 = F(x, u, theta);
        return (F1 - F2) / epsilon;
    }
}
