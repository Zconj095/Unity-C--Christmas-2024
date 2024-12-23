using System;
using System.Collections.Generic;
using System.Linq;
using Accord.Math; // For advanced mathematical computations
using UnityEngine;

public class EvolutionaryInformationModeling
{
    /// <summary>
    /// Computes the phylogenetic information gain (I_phylo).
    /// </summary>
    /// <param name="currentProbabilities">List of probabilities (p_i) of genes in the current population.</param>
    /// <param name="ancestralProbabilities">List of probabilities (q_i) of genes in the ancestral population.</param>
    /// <returns>Information gain (I_phylo).</returns>
    public static double ComputeInformationGain(List<double> currentProbabilities, List<double> ancestralProbabilities)
    {
        if (currentProbabilities.Count != ancestralProbabilities.Count)
            throw new ArgumentException("The size of current and ancestral probability lists must match.");

        double informationGain = 0.0;

        for (int i = 0; i < currentProbabilities.Count; i++)
        {
            double p = currentProbabilities[i];
            double q = ancestralProbabilities[i];

            if (p > 0 && q > 0) // Avoid log(0) and division by zero
            {
                informationGain += p * Math.Log(p / q, 2); // log base 2
            }
        }

        return informationGain;
    }

    /// <summary>
    /// Normalizes raw counts into probabilities.
    /// </summary>
    /// <param name="rawCounts">List of raw counts.</param>
    /// <returns>List of probabilities normalized from raw counts.</returns>
    public static List<double> NormalizeCounts(List<int> rawCounts)
    {
        int total = rawCounts.Sum(); // Total sum of counts
        List<double> probabilities = new List<double>();

        foreach (var count in rawCounts)
        {
            probabilities.Add((double)count / total);
        }

        return probabilities;
    }
}

// Unity Example Script
public class EvolutionaryInformationModelingExample : MonoBehaviour
{
    private void Start()
    {
        // Example raw counts for current population and ancestral population
        List<int> currentCounts = new List<int> { 30, 40, 30 }; // Example current population
        List<int> ancestralCounts = new List<int> { 25, 50, 25 }; // Example ancestral population

        // Normalize raw counts to probabilities
        List<double> currentProbabilities = EvolutionaryInformationModeling.NormalizeCounts(currentCounts);
        List<double> ancestralProbabilities = EvolutionaryInformationModeling.NormalizeCounts(ancestralCounts);

        // Compute phylogenetic information gain
        double informationGain = EvolutionaryInformationModeling.ComputeInformationGain(currentProbabilities, ancestralProbabilities);

        // Log the results
        Debug.Log("Current Probabilities: " + string.Join(", ", currentProbabilities));
        Debug.Log("Ancestral Probabilities: " + string.Join(", ", ancestralProbabilities));
        Debug.Log("Phylogenetic Information Gain (I_phylo): " + informationGain);
    }
}
