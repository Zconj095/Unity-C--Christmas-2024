using System;
using MathNet.Numerics.LinearAlgebra; // For matrix and vector computations
using MathNet.Numerics; // For numerical operations
using System.Numerics; // For complex numbers
using UnityEngine;

public class MagicalCircuitOptimization : MonoBehaviour
{
    // Circuit parameters
    private double magicalResistance; // R
    private double capacitance; // C
    private double angularFrequency; // ω

    /// <summary>
    /// Initializes the magical circuit parameters.
    /// </summary>
    /// <param name="resistance">Magical resistance (R).</param>
    /// <param name="capacitance">Capacitance (C).</param>
    /// <param name="frequency">Frequency (f).</param>
    public void InitializeCircuit(double resistance, double capacitance, double frequency)
    {
        this.magicalResistance = resistance;
        this.capacitance = capacitance;
        this.angularFrequency = 2 * Math.PI * frequency; // ω = 2πf
    }

    /// <summary>
    /// Computes the magical flux Φ(t) for a given voltage V(t).
    /// </summary>
    /// <param name="voltage">Voltage function V(t).</param>
    /// <param name="time">Time t.</param>
    /// <returns>Magical flux Φ(t) as a complex number.</returns>
    public Complex ComputeMagicalFlux(Func<double, double> voltage, double time)
    {
        // Compute impedance Z = R + 1/(jωC)
        var realPart = magicalResistance; // R
        var imaginaryPart = -1 / (angularFrequency * capacitance); // 1 / jωC
        var impedance = new Complex(realPart, imaginaryPart);

        // Compute V(t)
        double voltageValue = voltage(time);

        // Compute Φ(t) = V(t) / Z
        var flux = new Complex(voltageValue, 0) / impedance;

        return flux;
    }

    /// <summary>
    /// Example voltage function V(t).
    /// </summary>
    private static double ExampleVoltageFunction(double t)
    {
        return 10 * Math.Sin(2 * Math.PI * 60 * t); // 10V peak at 60Hz
    }

    /// <summary>
    /// Test the Magical Circuit Optimization system.
    /// </summary>
    [UnityEngine.ContextMenu("Run Magical Circuit Optimization")]
    public void TestMagicalCircuitOptimization()
    {
        // Initialize circuit parameters
        InitializeCircuit(10, 0.001, 60); // R = 10Ω, C = 1mF, f = 60Hz

        // Compute the magical flux at specific times
        double[] times = MathNet.Numerics.Generate.LinearSpaced(10, 0, 0.1); // Generate 10 time steps between 0 and 0.1 seconds

        foreach (var time in times)
        {
            var flux = ComputeMagicalFlux(ExampleVoltageFunction, time);

            // Log the results
            UnityEngine.Debug.Log($"Time: {time}s, Magical Flux Φ(t): {flux}");
        }
    }
}
