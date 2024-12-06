using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class HypervectorCluster : MonoBehaviour
{
    public List<MultidimensionalHypervector> Vectors = new List<MultidimensionalHypervector>();
    public MultidimensionalHypervector Centroid;

    public void UpdateCentroid()
    {
        int dimensions = Vectors[0].Dimensions;
        Centroid = new MultidimensionalHypervector(dimensions);
        for (int i = 0; i < dimensions; i++)
        {
            Centroid.Values[i] = Vectors.Average(v => v.Values[i]);
        }
    }

    public static List<HypervectorCluster> ClusterVectors(List<MultidimensionalHypervector> vectors, int numClusters)
    {
        List<HypervectorCluster> clusters = new List<HypervectorCluster>();
        for (int i = 0; i < numClusters; i++)
        {
            clusters.Add(new HypervectorCluster());
        }

        // Random initialization
        System.Random rand = new System.Random();
        foreach (var vector in vectors)
        {
            clusters[rand.Next(numClusters)].Vectors.Add(vector);
        }

        // Update centroids iteratively
        for (int iter = 0; iter < 10; iter++) // Limit iterations
        {
            foreach (var cluster in clusters) cluster.UpdateCentroid();

            // Reassign vectors to nearest cluster
            foreach (var vector in vectors)
            {
                HypervectorCluster nearest = clusters
                    .OrderBy(c => MultidimensionalHypervector.CalculateSimilarity(vector, c.Centroid))
                    .Last(); // Use cosine similarity
                nearest.Vectors.Add(vector);
            }
        }

        return clusters;
    }
}
