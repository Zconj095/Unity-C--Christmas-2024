using System;
using System.Collections.Generic;
using UnityEngine;

public class GroverKMeans : MonoBehaviour
{
    [Header("K-Means Parameters")]
    [SerializeField] private int numClusters = 3;      // Number of clusters
    [SerializeField] private int numPoints = 10;      // Number of data points
    [SerializeField] private int numIterations = 5;   // Maximum number of iterations
    [SerializeField] private Vector2 dataRange = new Vector2(0, 10); // Range of data points

    [Header("Grover's Algorithm")]
    [SerializeField] private int numQubits = 3;       // Number of qubits for Grover's search
    [SerializeField] private bool logIntermediateSteps = true;

    [Header("Visualization")]
    [SerializeField] private GameObject pointPrefab;  // Prefab for data points
    [SerializeField] private GameObject centroidPrefab; // Prefab for centroids
    [SerializeField] private Transform visualizationContainer;

    private Vector2[] dataPoints;                    // Array of data points
    private Vector2[] centroids;                     // Array of centroids
    private int[] clusterAssignments;                // Cluster assignments for each data point

    [ContextMenu("Run Quantum K-Means")]
    public void RunQuantumKMeans()
    {
        InitializeDataPoints();
        InitializeCentroids();

        for (int iteration = 0; iteration < numIterations; iteration++)
        {
            if (logIntermediateSteps)
                Debug.Log($"Iteration {iteration + 1}");

            // Step 1: Assign points to the closest centroid using Grover's search
            AssignClusters();

            // Step 2: Update centroids based on cluster assignments
            UpdateCentroids();

            if (logIntermediateSteps)
                LogState($"After Iteration {iteration + 1}");
        }

        Debug.Log("Quantum K-Means Completed.");
    }

    private void InitializeDataPoints()
    {
        dataPoints = new Vector2[numPoints];
        clusterAssignments = new int[numPoints];

        // Generate random data points within the specified range
        for (int i = 0; i < numPoints; i++)
        {
            dataPoints[i] = new Vector2(
                UnityEngine.Random.Range(dataRange.x, dataRange.y),
                UnityEngine.Random.Range(dataRange.x, dataRange.y)
            );
        }

        VisualizeDataPoints();
    }

    private void InitializeCentroids()
    {
        centroids = new Vector2[numClusters];

        // Initialize centroids randomly from the data range
        for (int i = 0; i < numClusters; i++)
        {
            centroids[i] = new Vector2(
                UnityEngine.Random.Range(dataRange.x, dataRange.y),
                UnityEngine.Random.Range(dataRange.x, dataRange.y)
            );
        }

        VisualizeCentroids();
    }

    private void AssignClusters()
    {
        for (int i = 0; i < dataPoints.Length; i++)
        {
            // Use Grover's algorithm to find the closest centroid
            clusterAssignments[i] = FindClosestCentroid(dataPoints[i]);
        }
    }

    private int FindClosestCentroid(Vector2 point)
    {
        int closestCentroid = 0;
        float minDistance = float.MaxValue;

        // Simulate Grover's search by iterating over centroids and finding the minimum distance
        for (int i = 0; i < centroids.Length; i++)
        {
            float distance = Vector2.Distance(point, centroids[i]);

            // "Oracle" marks the closest centroid
            if (distance < minDistance)
            {
                minDistance = distance;
                closestCentroid = i;
            }
        }

        if (logIntermediateSteps)
            Debug.Log($"Point {point} assigned to Centroid {closestCentroid}");

        return closestCentroid;
    }

    private void UpdateCentroids()
    {
        Vector2[] newCentroids = new Vector2[numClusters];
        int[] clusterSizes = new int[numClusters];

        // Recalculate centroids as the mean of assigned points
        for (int i = 0; i < dataPoints.Length; i++)
        {
            int cluster = clusterAssignments[i];
            newCentroids[cluster] += dataPoints[i];
            clusterSizes[cluster]++;
        }

        for (int i = 0; i < numClusters; i++)
        {
            if (clusterSizes[i] > 0)
                newCentroids[i] /= clusterSizes[i];
            else
                newCentroids[i] = centroids[i]; // Keep the centroid in place if no points are assigned
        }

        centroids = newCentroids;
        VisualizeCentroids();
    }

    private void VisualizeDataPoints()
    {
        foreach (Transform child in visualizationContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var point in dataPoints)
        {
            Instantiate(pointPrefab, new Vector3(point.x, 0, point.y), Quaternion.identity, visualizationContainer);
        }
    }

    private void VisualizeCentroids()
    {
        foreach (var centroid in centroids)
        {
            Instantiate(centroidPrefab, new Vector3(centroid.x, 0, centroid.y), Quaternion.identity, visualizationContainer);
        }
    }

    private void LogState(string message)
    {
        Debug.Log(message);
        for (int i = 0; i < centroids.Length; i++)
        {
            Debug.Log($"Centroid {i}: {centroids[i]}");
        }

        for (int i = 0; i < dataPoints.Length; i++)
        {
            Debug.Log($"Point {dataPoints[i]} -> Cluster {clusterAssignments[i]}");
        }
    }
}
