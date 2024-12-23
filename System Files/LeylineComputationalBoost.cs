using System;
using MathNet.Numerics.LinearAlgebra; // For vector and matrix operations
using MathNet.Numerics;
using UnityEngine; // For additional utilities

public class LeylineComputationalBoost : MonoBehaviour
{
    /// <summary>
    /// Computes the computational power gained from ley lines.
    /// P_ley = ∫ φ(r) ⋅ ∇ψ(r) d³r
    /// </summary>
    /// <param name="phi">Function representing the brain's magical field φ(r).</param>
    /// <param name="gradientPsi">Function representing the leyline potential gradient ∇ψ(r).</param>
    /// <param name="points">Grid of points to perform the integration.</param>
    /// <returns>Computed power P_ley.</returns>
    public static double ComputeLeylinePower(
        Func<Vector<double>, double> phi,
        Func<Vector<double>, Vector<double>> gradientPsi,
        Vector<double>[] points)
    {
        double totalPower = 0;

        // Integrate over the grid points
        foreach (var point in points)
        {
            var phiValue = phi(point);
            var gradientValue = gradientPsi(point);

            // Compute the dot product φ(r) ⋅ ∇ψ(r)
            double dotProduct = gradientValue.DotProduct(point);

            // Add to total power
            totalPower += phiValue * dotProduct;
        }

        return totalPower;
    }

    /// <summary>
    /// Test the Leyline Computational Boost system.
    /// </summary>
    [UnityEngine.ContextMenu("Run Leyline Computational Boost")]
    public static void TestLeylineComputationalBoost()
    {
        // Define φ(r): Brain's magical field
        Func<Vector<double>, double> phi = (r) => Math.Exp(-r.L2Norm()); // Exponential decay field

        // Define ∇ψ(r): Leyline potential gradient
        Func<Vector<double>, Vector<double>> gradientPsi = (r) => Vector<double>.Build.DenseOfArray(new[] { 1.0, 0.5, -0.2 }); // Static gradient vector

        // Generate a 3D grid of points
        var gridPoints = CreateGrid3D(-1, 1, -1, 1, -1, 1, 10);

        // Compute Leyline Power
        double leylinePower = ComputeLeylinePower(phi, gradientPsi, gridPoints);

        // Log results
        UnityEngine.Debug.Log($"Leyline Computational Power (P_ley): {leylinePower}");
    }

    /// <summary>
    /// Creates a 3D grid of points within specified bounds.
    /// </summary>
    /// <param name="xMin">Minimum x-coordinate.</param>
    /// <param name="xMax">Maximum x-coordinate.</param>
    /// <param name="yMin">Minimum y-coordinate.</param>
    /// <param name="yMax">Maximum y-coordinate.</param>
    /// <param name="zMin">Minimum z-coordinate.</param>
    /// <param name="zMax">Maximum z-coordinate.</param>
    /// <param name="steps">Number of steps in each dimension.</param>
    /// <returns>An array of 3D points as vectors.</returns>
    private static Vector<double>[] CreateGrid3D(double xMin, double xMax, double yMin, double yMax, double zMin, double zMax, int steps)
    {
        var xRange = Generate.LinearSpaced(steps, xMin, xMax);
        var yRange = Generate.LinearSpaced(steps, yMin, yMax);
        var zRange = Generate.LinearSpaced(steps, zMin, zMax);

        var grid = new System.Collections.Generic.List<Vector<double>>();

        foreach (var x in xRange)
        {
            foreach (var y in yRange)
            {
                foreach (var z in zRange)
                {
                    grid.Add(Vector<double>.Build.DenseOfArray(new[] { x, y, z }));
                }
            }
        }

        return grid.ToArray();
    }
}
