using System;
using System.Linq;
using UnityEngine;

public class MultidimensionalHypervector : MonoBehaviour
{
    public float[] Values;
    public int Dimensions;

    public MultidimensionalHypervector(int dimensions)
    {
        Dimensions = dimensions;
        Values = new float[dimensions];
        InitializeRandom();
    }

    public void InitializeRandom()
    {
        System.Random rand = new System.Random();
        for (int i = 0; i < Dimensions; i++)
        {
            Values[i] = (float)(rand.NextDouble() * 2 - 1); // Random values in [-1, 1]
        }
    }

    public static MultidimensionalHypervector AddNoise(MultidimensionalHypervector vector, float noiseLevel)
    {
        MultidimensionalHypervector noisyVector = new MultidimensionalHypervector(vector.Dimensions);
        for (int i = 0; i < vector.Dimensions; i++)
        {
            noisyVector.Values[i] = vector.Values[i] + UnityEngine.Random.Range(-noiseLevel, noiseLevel);
        }
        return noisyVector;
    }

    public static float CalculateSimilarity(MultidimensionalHypervector v1, MultidimensionalHypervector v2)
    {
        float dotProduct = v1.Values.Zip(v2.Values, (a, b) => a * b).Sum();
        float norm1 = Mathf.Sqrt(v1.Values.Sum(x => x * x));
        float norm2 = Mathf.Sqrt(v2.Values.Sum(x => x * x));
        return dotProduct / (norm1 * norm2); // Cosine similarity
    }

    public static MultidimensionalHypervector CrossDimension(MultidimensionalHypervector v1, MultidimensionalHypervector v2)
    {
        int dimensions = Math.Max(v1.Dimensions, v2.Dimensions);
        MultidimensionalHypervector crossVector = new MultidimensionalHypervector(dimensions);

        for (int i = 0; i < dimensions; i++)
        {
            float v1Value = i < v1.Dimensions ? v1.Values[i] : 0;
            float v2Value = i < v2.Dimensions ? v2.Values[i] : 0;
            crossVector.Values[i] = (v1Value + v2Value) / 2; // Averaging values across dimensions
        }

        return crossVector;
    }
}
