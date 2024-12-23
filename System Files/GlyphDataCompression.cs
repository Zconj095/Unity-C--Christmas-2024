using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics;
using UnityEngine; // For advanced mathematical computations

public class GlyphDataCompression : MonoBehaviour
{
    /// <summary>
    /// Computes the entropy of the glyph data.
    /// H_glyph = -Σ p_i * log2(p_i)
    /// </summary>
    /// <param name="probabilities">List of probabilities p_i for each glyph.</param>
    /// <returns>Entropy value H_glyph.</returns>
    public static double ComputeGlyphEntropy(List<double> probabilities)
    {
        double entropy = 0;

        foreach (double p in probabilities)
        {
            if (p > 0) // To avoid log(0)
            {
                entropy -= p * Math.Log(p, 2); // Using log base 2
            }
        }

        return entropy;
    }

    /// <summary>
    /// Normalizes a list of glyph frequencies into probabilities.
    /// </summary>
    /// <param name="frequencies">List of raw frequencies for each glyph.</param>
    /// <returns>Normalized probabilities.</returns>
    public static List<double> NormalizeFrequencies(List<int> frequencies)
    {
        double total = frequencies.Sum();
        var probabilities = new List<double>();

        foreach (int freq in frequencies)
        {
            probabilities.Add(freq / total);
        }

        return probabilities;
    }

    /// <summary>
    /// Test the Glyph-Based Data Compression system.
    /// </summary>
    [UnityEngine.ContextMenu("Run Glyph Data Compression")]
    public static void TestGlyphDataCompression()
    {
        // Example glyph frequencies
        List<int> glyphFrequencies = new List<int> { 10, 20, 30, 40 }; // Example frequencies for symbols

        // Normalize to probabilities
        List<double> probabilities = NormalizeFrequencies(glyphFrequencies);

        // Compute entropy
        double entropy = ComputeGlyphEntropy(probabilities);

        // Log results
        UnityEngine.Debug.Log($"Glyph Frequencies: {string.Join(", ", glyphFrequencies)}");
        UnityEngine.Debug.Log($"Normalized Probabilities: {string.Join(", ", probabilities)}");
        UnityEngine.Debug.Log($"Glyph Data Entropy (H_glyph): {entropy}");
    }
}
