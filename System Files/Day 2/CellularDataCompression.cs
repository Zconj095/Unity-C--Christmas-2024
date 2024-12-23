using System;
using System.Collections.Generic;
using System.Linq;
using Accord.Math; // For advanced mathematical computations
using UnityEngine;

public class CellularDataCompression
{
    /// <summary>
    /// Computes the entropy (H) of biological data using the given probabilities.
    /// </summary>
    /// <param name="probabilities">List of probabilities (p_i) for genetic or cellular features.</param>
    /// <returns>Entropy H of the biological information.</returns>
    public static double ComputeEntropy(List<double> probabilities)
    {
        double entropy = 0.0;

        foreach (var p in probabilities)
        {
            if (p > 0) // Avoid log(0) which is undefined
            {
                entropy += p * Math.Log(p, 2); // Using log base 2
            }
        }

        return -entropy; // Entropy is negative of the sum
    }

    /// <summary>
    /// Normalizes a list of raw counts into probabilities.
    /// </summary>
    /// <param name="rawCounts">List of raw counts of features.</param>
    /// <returns>Normalized probabilities.</returns>
    public static List<double> NormalizeCounts(List<int> rawCounts)
    {
        int total = rawCounts.Sum(); // Total count of all features
        List<double> probabilities = new List<double>();

        foreach (var count in rawCounts)
        {
            probabilities.Add((double)count / total);
        }

        return probabilities;
    }
}

// Unity Example Script
public class CellularDataCompressionExample : MonoBehaviour
{
    private void Start()
    {
        // Example raw counts for genetic or cellular features
        List<int> rawCounts = new List<int> { 50, 30, 20 };

        // Normalize counts to probabilities
        List<double> probabilities = CellularDataCompression.NormalizeCounts(rawCounts);

        // Compute entropy of the biological data
        double entropy = CellularDataCompression.ComputeEntropy(probabilities);

        // Log the probabilities and entropy
        Debug.Log("Probabilities: " + string.Join(", ", probabilities));
        Debug.Log("Entropy (H): " + entropy);
    }
}
