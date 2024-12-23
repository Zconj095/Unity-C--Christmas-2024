using System;
using UnityEngine;
using MathNet.Numerics;
using Accord.Math;

public class EnzymaticReactionModel : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private double vmax = 1.0; // Maximum reaction velocity
    [SerializeField] private double substrateConcentration = 1.0; // [S]
    [SerializeField] private double michaelisConstant = 0.5; // Km
    [SerializeField] private double magicalEnhancementFactor = 0.2; // ?
    [SerializeField] private double timeFactor = 0.0; // Time t in seconds

    [Header("Results")]
    [SerializeField] private double reactionVelocity; // v

    void Update()
    {
        // Update the reaction velocity dynamically
        reactionVelocity = CalculateReactionVelocity(vmax, substrateConcentration, michaelisConstant, magicalEnhancementFactor, timeFactor);

        // Update the time factor for demonstration purposes (optional)
        timeFactor += Time.deltaTime;

        // Log the reaction velocity to the console
        Debug.Log($"Time: {timeFactor:F2}s, Reaction Velocity: {reactionVelocity:F4}");
    }

    private double CalculateReactionVelocity(double vmax, double substrateConcentration, double km, double lambda, double time)
    {
        // Michaelis-Menten Kinetics with Magical Augmentation
        double michaelisMentenTerm = (vmax * substrateConcentration) / (km + substrateConcentration);
        double magicalAugmentation = lambda * Math.Sin(time); // Example magical augmentation as a sinusoidal function

        return michaelisMentenTerm + magicalAugmentation;
    }
}
