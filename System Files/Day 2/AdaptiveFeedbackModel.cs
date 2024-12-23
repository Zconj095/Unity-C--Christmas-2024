using System;
using Accord.Math; // For numerical computations and handling dynamic models
using UnityEngine;

public class AdaptiveFeedbackModel
{
    // Parameters
    private double growthRate; // α
    private double carryingCapacity; // K
    private double beta; // β

    // Current population and environmental pressure
    public double CurrentPopulation { get; private set; }
    public double EnvironmentalPressure { get; set; } // I_t

    /// <summary>
    /// Constructor to initialize the model parameters.
    /// </summary>
    public AdaptiveFeedbackModel(double initialPopulation, double growthRate, double carryingCapacity, double beta)
    {
        CurrentPopulation = initialPopulation;
        this.growthRate = growthRate;
        this.carryingCapacity = carryingCapacity;
        this.beta = beta;
    }

    /// <summary>
    /// Simulates one step of population change based on the environmental selection pressure equation.
    /// </summary>
    /// <param name="timeStep">Time step for the simulation.</param>
    public void SimulateStep(double timeStep)
    {
        // Equation: ΔP_t = αP_t(1 - P_t/K) - βP_tI_t
        double deltaPopulation = growthRate * CurrentPopulation * (1 - CurrentPopulation / carryingCapacity)
                                - beta * CurrentPopulation * EnvironmentalPressure;

        // Update population based on time step
        CurrentPopulation += deltaPopulation * timeStep;

        // Ensure population doesn't go negative
        CurrentPopulation = Math.Max(CurrentPopulation, 0);

        // Log the results to the Unity Console
        Debug.Log($"Population at this step: {CurrentPopulation}");
    }
}

// Example Usage in Unity
public class AdaptiveFeedbackModelExample : MonoBehaviour
{
    private AdaptiveFeedbackModel model;

    private void Start()
    {
        // Initialize parameters
        double initialPopulation = 50.0; // Initial population
        double growthRate = 0.1; // α
        double carryingCapacity = 500.0; // K
        double beta = 0.02; // β
        double environmentalPressure = 0.5; // I_t

        // Create the adaptive feedback model
        model = new AdaptiveFeedbackModel(initialPopulation, growthRate, carryingCapacity, beta)
        {
            EnvironmentalPressure = environmentalPressure
        };

        // Simulate over multiple steps
        for (int i = 0; i < 100; i++) // 100 time steps
        {
            model.SimulateStep(0.1); // 0.1 is the time step
        }
    }
}
