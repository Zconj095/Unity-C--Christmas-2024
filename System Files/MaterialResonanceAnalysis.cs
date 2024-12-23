using System;
using MathNet.Numerics.Integration;
using MathNet.Numerics.LinearAlgebra;
using UnityEngine;
public class MaterialResonanceAnalysis : MonoBehaviour
{
    /// <summary>
    /// Computes the material resonance response R(f) for a given frequency.
    /// R(f) = ∫₀⁺∞ ρ(x) * e^(-j2πfx) dx
    /// </summary>
    /// <param name="rho">Density function ρ(x) of magical-energy interactions.</param>
    /// <param name="frequency">Frequency of analysis f.</param>
    /// <param name="domain">Domain [0, ∞) for integration (truncated for numerical purposes).</param>
    /// <returns>Resonance response R(f) (complex number).</returns>
    public static System.Numerics.Complex ComputeResonanceResponse(Func<double, double> rho, double frequency, double domainLimit)
    {
        // Define the integral function
        Func<double, System.Numerics.Complex> integrand = (x) =>
        {
            // ρ(x) * e^(-j2πfx)
            double rhoValue = rho(x);
            var exponential = System.Numerics.Complex.Exp(-System.Numerics.Complex.ImaginaryOne * 2 * Math.PI * frequency * x);
            return rhoValue * exponential;
        };

        // Perform numerical integration over [0, domainLimit]
        var realPart = GaussLegendreRule.Integrate(
            x => integrand(x).Real,
            0, domainLimit, 1000); // 1000 points for accuracy
        var imaginaryPart = GaussLegendreRule.Integrate(
            x => integrand(x).Imaginary,
            0, domainLimit, 1000);

        return new System.Numerics.Complex(realPart, imaginaryPart);
    }

    /// <summary>
    /// Example density function ρ(x).
    /// </summary>
    private static double ExampleDensityFunction(double x)
    {
        // Example: Gaussian density function
        double sigma = 1.0; // Standard deviation
        double mean = 5.0;  // Mean
        return Math.Exp(-Math.Pow(x - mean, 2) / (2 * Math.Pow(sigma, 2))) / (sigma * Math.Sqrt(2 * Math.PI));
    }

    /// <summary>
    /// Test the Material Resonance Analysis system.
    /// </summary>
    [UnityEngine.ContextMenu("Run Material Resonance Analysis")]
    public static void TestMaterialResonance()
    {
        // Define the density function ρ(x)
        Func<double, double> rho = ExampleDensityFunction;

        // Define the frequency of analysis
        double frequency = 10.0; // Example frequency

        // Compute the resonance response
        var resonanceResponse = ComputeResonanceResponse(rho, frequency, 20.0); // Integrate up to x = 20

        // Log the result
        UnityEngine.Debug.Log($"Frequency: {frequency} Hz");
        UnityEngine.Debug.Log($"Resonance Response (R(f)): {resonanceResponse}");
    }
}
