using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DistributedEncodingManager : MonoBehaviour
{
    public int numVectors = 300;
    public int vectorDimension = 1000;
    public float noiseLevel = 0.1f;

    private List<Hypervector> originalVectors;
    private Hypervector chainedOriginal;
    private Hypervector chainedNoisy;

    void Start()
    {
        // Step 1: Generate Original Vectors
        originalVectors = new List<Hypervector>();
        for (int i = 0; i < numVectors; i++)
        {
            originalVectors.Add(new Hypervector(vectorDimension));
        }

        // Step 2: Perform Chaining on Original Vectors
        chainedOriginal = Hypervector.Chain(originalVectors.ToArray());

        // Step 3: Add Noise to Vectors
        List<Hypervector> noisyVectors = new List<Hypervector>();
        foreach (var vector in originalVectors)
        {
            noisyVectors.Add(Hypervector.AddNoise(vector, noiseLevel));
        }

        // Step 4: Perform Chaining on Noisy Vectors
        chainedNoisy = Hypervector.Chain(noisyVectors.ToArray());

        // Step 5: Calculate Changes
        float[] positionalChanges = Hypervector.CalculateChanges(chainedOriginal, chainedNoisy);

        // Step 6: Output Statistics
        OutputStatistics(positionalChanges);
    }

    void OutputStatistics(float[] changes)
    {
        Debug.Log($"All Changes Array: {string.Join(", ", changes.Take(10))}"); // Debug first 10 changes

        // Manual aggregation
        float meanChange = changes.Sum() / changes.Length;
        float maxChange = changes.Max();
        float minChange = changes.Min();

        Debug.Log($"Mean Change: {meanChange}");
        Debug.Log($"Max Change: {maxChange}");
        Debug.Log($"Min Change: {minChange}");

        // Calculate Scale Change
        float scaleChange = CalculateScaleChange(chainedOriginal, chainedNoisy);
        Debug.Log($"Scale Change: {scaleChange}");

        // Calculate Versatility Change
        float versatilityChange = CalculateVersatilityChange();
        Debug.Log($"Vector Versatility Change: {versatilityChange}");
    }




    float Mean(float[] values)
    {
        if (values.Length == 0) return 0;
        float sum = values.Sum();
        Debug.Log($"Sum of Values: {sum}, Count: {values.Length}");
        return sum / values.Length;
    }

    float CalculateScaleChange(Hypervector v1, Hypervector v2)
    {
        float norm1 = Norm(v1);
        float norm2 = Norm(v2);
        float normDifference = Mathf.Abs(norm1 - norm2);

        Debug.Log($"Norm 1: {norm1}, Norm 2: {norm2}, Norm Difference: {normDifference}");
        return normDifference;
    }



    float CalculateVersatilityChange()
    {
        float totalChange = 0f;
        for (int i = 0; i < originalVectors.Count; i++)
        {
            // Calculate the changes as a float[] and take the mean as the versatility metric
            float[] changes = Hypervector.CalculateChanges(originalVectors[i],
                                Hypervector.AddNoise(originalVectors[i], noiseLevel));
            totalChange += Mean(changes); // Use the Mean method to get the average change
        }
        return totalChange / originalVectors.Count;
    }

    float Norm(Hypervector vector)
    {
        return Mathf.Sqrt(vector.Values.Sum(x => x * x));
    }
}
