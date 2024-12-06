using System;
using System.Linq;
using UnityEngine;

public class Hypervector : MonoBehaviour
{
    public float[] Values;

    public Hypervector(int dimension)
    {
        Values = new float[dimension];
        InitializeRandom();
    }

    public void InitializeRandom()
    {
        System.Random rand = new System.Random();
        for (int i = 0; i < Values.Length; i++)
        {
            Values[i] = (float)(rand.NextDouble() * 2 - 1); // Random values in [-1, 1]
        }
    }

    public static Hypervector AddNoise(Hypervector vector, float noiseLevel)
    {
        Hypervector noisy = new Hypervector(vector.Values.Length);
        for (int i = 0; i < vector.Values.Length; i++)
        {
            noisy.Values[i] = vector.Values[i] + UnityEngine.Random.Range(-noiseLevel, noiseLevel);

            // Debug to ensure noise is applied
            Debug.Log($"Original: {vector.Values[i]}, Noisy: {noisy.Values[i]}");
        }
        return noisy;
    }

    public static Hypervector Chain(Hypervector[] vectors)
    {
        Hypervector chained = new Hypervector(vectors[0].Values.Length);
        for (int i = 0; i < chained.Values.Length; i++)
        {
            chained.Values[i] = 1f; // Start with neutral multiplicative identity
        }

        foreach (var vector in vectors)
        {
            for (int i = 0; i < vector.Values.Length; i++)
            {
                chained.Values[i] *= vector.Values[i]; // Element-wise multiplication
            }
        }

        // Debug chained vector
        Debug.Log($"Chained Vector: {string.Join(", ", chained.Values.Take(10))}"); // Show first 10 values
        return chained;
    }

    public static float CalculateSimilarity(Hypervector v1, Hypervector v2)
    {
        float dotProduct = v1.Values.Zip(v2.Values, (a, b) => a * b).Sum();
        float norm1 = Mathf.Sqrt(v1.Values.Sum(x => x * x));
        float norm2 = Mathf.Sqrt(v2.Values.Sum(x => x * x));
        return dotProduct / (norm1 * norm2); // Cosine similarity
    }

    public static float[] CalculateChanges(Hypervector original, Hypervector noisy)
    {
        float[] changes = new float[original.Values.Length];
        for (int i = 0; i < original.Values.Length; i++)
        {
            changes[i] = Mathf.Abs(original.Values[i] - noisy.Values[i]);
        }

        // Debug changes to ensure non-zero values
        Debug.Log($"Changes: {string.Join(", ", changes.Take(10))}");
        return changes;
    }
}
