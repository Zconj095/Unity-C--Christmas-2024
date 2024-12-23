using System;
using System.Collections.Generic;
using Accord.Math;
using UnityEngine;

public class EvolutionaryPathwaySimulation
{
    /// <summary>
    /// Represents the traits of an organism and their respective benefits.
    /// </summary>
    public struct Trait
    {
        public double Benefit; // ??(x): Benefit of trait i in the current environment.

        public Trait(double benefit)
        {
            Benefit = benefit;
        }
    }

    /// <summary>
    /// Calculates the fitness of an organism with a given genotype.
    /// </summary>
    /// <param name="traits">List of traits with their benefits.</param>
    /// <param name="geneticComplexityCost">Cost of maintaining genetic complexity (G(x)).</param>
    /// <param name="lambda">Penalty coefficient (?).</param>
    /// <returns>Fitness F(x).</returns>
    public static double CalculateFitness(List<Trait> traits, double geneticComplexityCost, double lambda)
    {
        double totalBenefits = 0;

        // Sum benefits of all traits
        foreach (var trait in traits)
        {
            totalBenefits += trait.Benefit;
        }

        // Fitness equation: F(x) = ???(x) - ?G(x)
        double fitness = totalBenefits - lambda * geneticComplexityCost;

        return fitness;
    }
}

// Example Usage in Unity
public class EvolutionaryPathwaySimulationExample : MonoBehaviour
{
    private void Start()
    {
        // Example data: Organism with 3 traits
        var traits = new List<EvolutionaryPathwaySimulation.Trait>
        {
            new EvolutionaryPathwaySimulation.Trait(2.5), // Benefit of Trait 1
            new EvolutionaryPathwaySimulation.Trait(3.0), // Benefit of Trait 2
            new EvolutionaryPathwaySimulation.Trait(1.5)  // Benefit of Trait 3
        };

        // Genetic complexity cost (G(x)) example value
        double geneticComplexityCost = 1.2;

        // Penalty coefficient (?)
        double lambda = 0.8;

        // Calculate fitness
        double fitness = EvolutionaryPathwaySimulation.CalculateFitness(traits, geneticComplexityCost, lambda);

        // Log the fitness to the Unity Console
        Debug.Log($"Organism Fitness (F(x)): {fitness}");
    }
}
