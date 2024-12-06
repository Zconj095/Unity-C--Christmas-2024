using System.Collections.Generic;
using System.Linq; // Required for Take method
using UnityEngine;

public class HypervectorTestHarness : MonoBehaviour
{
    public int numVectors = 10; // Number of hypervectors
    public int vectorDimension = 1000; // Dimensionality of each hypervector
    public int numClusters = 3; // Number of clusters for categorization
    public float noiseLevel = 0.1f; // Noise level for testing

    void Start()
    {
        // Step 1: Generate Multidimensional Hypervectors
        List<MultidimensionalHypervector> vectors = new List<MultidimensionalHypervector>();
        for (int i = 0; i < numVectors; i++)
        {
            var hv = new MultidimensionalHypervector(vectorDimension);
            vectors.Add(hv);
        }

        Debug.Log("Step 1: Generated Hypervectors");

        // Step 2: Add Noise to Vectors
        List<MultidimensionalHypervector> noisyVectors = new List<MultidimensionalHypervector>();
        foreach (var vector in vectors)
        {
            var noisy = MultidimensionalHypervector.AddNoise(vector, noiseLevel);
            noisyVectors.Add(noisy);
        }

        Debug.Log("Step 2: Added Noise to Hypervectors");

        // Step 3: Group Categorization with Clustering
        List<HypervectorCluster> clusters = HypervectorCluster.ClusterVectors(vectors, numClusters);

        Debug.Log("Step 3: Grouped Hypervectors into Clusters");

        for (int i = 0; i < clusters.Count; i++)
        {
            Debug.Log($"Cluster {i + 1} Centroid: {string.Join(", ", clusters[i].Centroid.Values.Take(10))}...");
        }

        // Step 4: Apply Trans-Dimensional Logic Gates
        var crossDimensionVector = MultidimensionalHypervector.CrossDimension(vectors[0], vectors[1]);
        var andVector = HypervectorGate.AND(vectors[0], vectors[1]);
        var orVector = HypervectorGate.OR(vectors[0], vectors[1]);

        Debug.Log($"Step 4: Cross Dimension Vector (First 10): {string.Join(", ", crossDimensionVector.Values.Take(10))}");
        Debug.Log($"AND Gate Vector (First 10): {string.Join(", ", andVector.Values.Take(10))}");
        Debug.Log($"OR Gate Vector (First 10): {string.Join(", ", orVector.Values.Take(10))}");

        // Step 5: Calculate Rifts Between Vectors
        float rift = VectorRiftCalculator.CalculateRift(vectors[0], vectors[1]);
        Debug.Log($"Step 5: Calculated Rift Between Vectors: {rift}");

        Debug.Log("Processing Complete");
    }
}
