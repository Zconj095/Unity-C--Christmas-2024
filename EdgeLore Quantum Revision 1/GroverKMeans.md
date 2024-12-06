# GroverKMeans

## Overview
The `GroverKMeans` script implements a quantum-inspired K-Means clustering algorithm using Grover's search to assign data points to clusters. It generates random data points within a specified range, initializes centroids, and iteratively assigns points to the nearest centroid while updating the centroids based on the cluster assignments. The script also provides visualization for the data points and centroids in a Unity environment. This script fits into a larger codebase that likely involves quantum computing concepts and visualizations in Unity.

## Variables

- **numClusters**: (int) The number of clusters for the K-Means algorithm.
- **numPoints**: (int) The total number of data points to be generated.
- **numIterations**: (int) The maximum number of iterations to run the K-Means algorithm.
- **dataRange**: (Vector2) The range within which the data points will be randomly generated.
- **numQubits**: (int) The number of qubits used for Grover's search algorithm.
- **logIntermediateSteps**: (bool) A flag to determine whether to log intermediate steps during the algorithm's execution.
- **pointPrefab**: (GameObject) The prefab used to visualize data points in Unity.
- **centroidPrefab**: (GameObject) The prefab used to visualize centroids in Unity.
- **visualizationContainer**: (Transform) The parent transform under which the visualized data points and centroids will be instantiated.
- **dataPoints**: (Vector2[]) An array that stores the generated data points.
- **centroids**: (Vector2[]) An array that stores the current centroids of the clusters.
- **clusterAssignments**: (int[]) An array that holds the cluster assignment for each data point.

## Functions

- **RunQuantumKMeans()**: This is the main function that initiates the K-Means clustering process. It initializes data points and centroids, then iteratively assigns clusters and updates centroids for a specified number of iterations.

- **InitializeDataPoints()**: This function generates random data points within the specified range and initializes the `clusterAssignments` array.

- **InitializeCentroids()**: This function randomly initializes the centroids based on the specified number of clusters.

- **AssignClusters()**: This function assigns each data point to the nearest centroid using Grover's search, which is simulated in this context.

- **FindClosestCentroid(Vector2 point)**: This function finds and returns the index of the closest centroid to a given data point by calculating the distance between the point and each centroid.

- **UpdateCentroids()**: This function recalculates the centroids based on the current cluster assignments and updates the centroid positions accordingly.

- **VisualizeDataPoints()**: This function instantiates visual representations of the data points in the Unity scene.

- **VisualizeCentroids()**: This function instantiates visual representations of the centroids in the Unity scene.

- **LogState(string message)**: This function logs the current state of the centroids and data points, including their assignments to the console for debugging purposes.