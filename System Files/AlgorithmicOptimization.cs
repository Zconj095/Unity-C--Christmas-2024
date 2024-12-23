using System;
using System.Linq;
using MathNet.Numerics; // For advanced computations
using MathNet.Numerics.Integration; // For integration if needed
using MathNet.Numerics.RootFinding; // For root solvers
using Accord.Statistics.Models.Regression.Linear; // For linear regression
using UnityEngine;

public class AlgorithmicOptimization : MonoBehaviour
{
    // Public coefficients for T(n) = a * n^k + b * log(n) + c
    [Header("Equation Parameters")]
    public double a = 1.0; // Coefficient for n^k
    public double b = 1.0; // Coefficient for log(n)
    public double c = 0.0; // Constant term
    public double k = 2.0; // Exponent for n^k

    [Header("Input Size (n)")]
    public int n = 10; // Input size for computation

    void Start()
    {
        // Compute the time complexity
        double complexity = ComputeTimeComplexity(a, b, c, k, n);

        // Log the complexity to the Unity console
        Debug.Log($"Computed Time Complexity T({n}): {complexity}");
    }

    /// <summary>
    /// Computes the time complexity based on the equation T(n) = a * n^k + b * log(n) + c.
    /// </summary>
    /// <param name="a">Coefficient for n^k</param>
    /// <param name="b">Coefficient for log(n)</param>
    /// <param name="c">Constant term</param>
    /// <param name="k">Exponent of n</param>
    /// <param name="n">Input size</param>
    /// <returns>Computed time complexity</returns>
    private double ComputeTimeComplexity(double a, double b, double c, double k, int n)
    {
        // Validate the input
        if (n <= 0)
        {
            Debug.LogError("Input size n must be greater than 0.");
            return double.NaN;
        }

        // Compute individual components
        double term1 = a * Math.Pow(n, k); // a * n^k
        double term2 = b * Math.Log(n);   // b * log(n)
        double constant = c; // Constant term

        // Combine terms
        double totalComplexity = term1 + term2 + constant;

        // For demonstration: Log individual components
        Debug.Log($"T(n) Breakdown -> Term1 (a*n^k): {term1}, Term2 (b*log(n)): {term2}, Constant: {constant}");

        return totalComplexity;
    }

    /// <summary>
    /// Dynamically optimizes coefficients (a, b, c, k) based on experimental data.
    /// </summary>
    public void OptimizeCoefficients(double[] observedData, int[] inputSizes)
    {
        // Create the design matrix
        var designMatrix = inputSizes
            .Select(n => new[] { Math.Pow(n, k), Math.Log(n), 1.0 })
            .ToArray();

        // Perform ordinary least squares regression
        var ols = new OrdinaryLeastSquares();
        var regression = ols.Learn(designMatrix, observedData);

        // Update coefficients
        a = regression.Weights[0];
        b = regression.Weights[1];
        c = regression.Intercept;

        Debug.Log($"Optimized Coefficients -> a: {a}, b: {b}, c: {c}");
    }
}
