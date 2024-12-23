using System;
using System.Collections.Generic;
using Accord.Math; // For mathematical operations and dynamic simulations
using UnityEngine;

public class MagicalBiologicalInteractionSimulation
{
    private double baselineMutationRate; // ?: Baseline biological mutation rate
    private double magicEnhancementFactor; // ?: Magic-enhancement factor

    /// <summary>
    /// Constructor to initialize the parameters for mutation rate.
    /// </summary>
    /// <param name="baselineMutationRate">Baseline biological mutation rate (?).</param>
    /// <param name="magicEnhancementFactor">Magic-enhancement factor (?).</param>
    public MagicalBiologicalInteractionSimulation(double baselineMutationRate, double magicEnhancementFactor)
    {
        this.baselineMutationRate = baselineMutationRate;
        this.magicEnhancementFactor = magicEnhancementFactor;
    }

    /// <summary>
    /// Computes the total mutation rate (M) given the magical energy over time (?(t)).
    /// </summary>
    /// <param name="magicalEnergyFunction">Function ?(t) representing magical energy applied over time.</param>
    /// <param name="timePoints">List of time points to evaluate ?(t).</param>
    /// <returns>List of mutation rates M over the time points.</returns>
    public List<double> ComputeMutationRate(Func<double, double> magicalEnergyFunction, List<double> timePoints)
    {
        List<double> mutationRates = new List<double>();

        foreach (var t in timePoints)
        {
            double magicalEnergy = magicalEnergyFunction(t);
            double mutationRate = baselineMutationRate + magicEnhancementFactor * magicalEnergy;
            mutationRates.Add(mutationRate);
        }

        return mutationRates;
    }
}

// Unity Example Script
public class MagicalBiologicalInteractionExample : MonoBehaviour
{
    private void Start()
    {
        // Initialize simulation parameters
        double baselineMutationRate = 0.01; // ?: Baseline mutation rate
        double magicEnhancementFactor = 2.0; // ?: Magic-enhancement factor

        // Create the simulation
        var simulation = new MagicalBiologicalInteractionSimulation(baselineMutationRate, magicEnhancementFactor);

        // Define magical energy function ?(t): Example: sinusoidal magical energy over time
        Func<double, double> magicalEnergyFunction = t => Math.Sin(t) * 10;

        // Define time points for the simulation
        List<double> timePoints = new List<double>();
        for (double t = 0; t <= 10; t += 0.1) // From t=0 to t=10 with 0.1 intervals
        {
            timePoints.Add(t);
        }

        // Compute mutation rates
        List<double> mutationRates = simulation.ComputeMutationRate(magicalEnergyFunction, timePoints);

        // Log the mutation rates to Unity Console
        for (int i = 0; i < timePoints.Count; i++)
        {
            Debug.Log($"Time: {timePoints[i]:0.00}, Mutation Rate (M): {mutationRates[i]:0.000}");
        }
    }
}
