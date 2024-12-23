using System;
using UnityEngine;
using MathNet.Numerics;
using Accord.Math;

public class ReactionPathwayOptimization : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private double frequencyFactor = 1.0e12; // A (Frequency factor, s^-1)
    [SerializeField] private double activationEnergy = 50.0; // Ea (Activation energy, kJ/mol)
    [SerializeField] private double gasConstant = 8.314; // R (Gas constant, J/(mol·K))
    [SerializeField] private double temperature = 298.15; // T (Temperature, K)
    [SerializeField] private double magicalFieldFactor = 0.1; // ? (Magical field factor)
    [SerializeField] private double magicalFieldEffect = 2.0; // ? (Magical field effect)

    [Header("Results")]
    [SerializeField] private double reactionRate; // k (Reaction rate)

    void Update()
    {
        // Calculate the reaction rate dynamically
        reactionRate = CalculateReactionRate(frequencyFactor, activationEnergy, gasConstant, temperature, magicalFieldFactor, magicalFieldEffect);

        // Log the reaction rate to the console
        Debug.Log($"Reaction Rate (k): {reactionRate:E4} s^-1");
    }

    private double CalculateReactionRate(double A, double Ea, double R, double T, double gamma, double phi)
    {
        // Arrhenius Equation with Magical Field Enhancement
        double arrheniusTerm = A * Math.Exp(-Ea / (R * T)); // A * e^(-Ea / RT)
        double magicalEnhancement = gamma * phi; // ??

        return arrheniusTerm + magicalEnhancement; // Combined reaction rate
    }
}
