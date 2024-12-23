using System;
using UnityEngine;
using MathNet.Numerics;
using MathNet.Numerics.Integration;

public class ElementalCompositionAnalysis : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private double frequency = 50.0; // f: Frequency of analysis
    [SerializeField] private double domainStart = 0.0; // Start of integration domain
    [SerializeField] private double domainEnd = 100.0; // End of integration domain
    [SerializeField] private int integrationSteps = 1000; // Steps for numerical integration

    [Header("Results")]
    [SerializeField] private double signalIntensity; // I(f): Intensity of the elemental signal

    void Start()
    {
        // Perform numerical integration to calculate I(f)
        signalIntensity = CalculateElementalSignalIntensity(frequency, domainStart, domainEnd, integrationSteps);

        // Log the resulting intensity
        Debug.Log($"Signal Intensity I(f) at Frequency {frequency} Hz: {signalIntensity:F4}");
    }

    private double CalculateElementalSignalIntensity(double freq, double start, double end, int steps)
    {
        // Define the integration step size
        double stepSize = (end - start) / steps;
        double integralSum = 0.0;

        // Perform numerical integration over the domain
        for (int i = 0; i < steps; i++)
        {
            double x = start + i * stepSize;
            double rho = ElementalDensity(x); // ?(x): Density of elemental interactions
            double exponentialTerm = Math.Cos(2 * Math.PI * freq * x) - Math.Sin(2 * Math.PI * freq * x);

            integralSum += rho * exponentialTerm * stepSize;
        }

        return integralSum;
    }

    private double ElementalDensity(double x)
    {
        // Example elemental density function ?(x): Gaussian distribution
        double mean = 50.0; // Mean position of interactions
        double sigma = 10.0; // Spread of interactions
        return Math.Exp(-Math.Pow(x - mean, 2) / (2 * sigma * sigma));
    }
}
