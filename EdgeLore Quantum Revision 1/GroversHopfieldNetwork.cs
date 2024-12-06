using System;
using System.Collections.Generic;
using UnityEngine;

public class GroversHopfieldNetwork : MonoBehaviour
{
    [Header("Network Settings")]
    [SerializeField] private int numNeurons = 8;                // Number of neurons in the network
    [SerializeField] private int maxIterations = 10;           // Maximum number of retrieval iterations
    [SerializeField] private bool logSteps = true;             // Log intermediate steps

    [Header("Training Data")]
    [SerializeField] private List<int[]> trainingPatterns;     // Patterns to be stored in the network

    [Header("Grover's Search Settings")]
    [SerializeField] private int quantumIterations = 2;        // Number of Grover's iterations for search

    [Header("Visualization")]
    [SerializeField] private GameObject neuronPrefab;          // Prefab for neurons
    [SerializeField] private Transform visualizationContainer; // Parent object for visualization

    private float[,] weightMatrix;                            // Weight matrix of the Hopfield network
    private int[] currentPattern;                             // Current input pattern

    [ContextMenu("Train Network")]
    public void TrainNetwork()
    {
        if (trainingPatterns == null || trainingPatterns.Count == 0)
        {
            Debug.LogError("No training patterns provided.");
            return;
        }

        // Initialize the weight matrix
        weightMatrix = new float[numNeurons, numNeurons];

        // Hebbian Learning Rule
        foreach (var pattern in trainingPatterns)
        {
            for (int i = 0; i < numNeurons; i++)
            {
                for (int j = 0; j < numNeurons; j++)
                {
                    if (i != j)
                    {
                        weightMatrix[i, j] += pattern[i] * pattern[j];
                    }
                }
            }
        }

        Debug.Log("Training Completed. Weight Matrix Initialized.");
    }

    [ContextMenu("Retrieve Pattern")]
    public void RetrievePattern()
    {
        if (currentPattern == null || currentPattern.Length != numNeurons)
        {
            Debug.LogError("Current input pattern is not set or invalid.");
            return;
        }

        Debug.Log("Starting Pattern Retrieval...");
        int[] retrievedPattern = QuantumSearch(currentPattern);

        Debug.Log("Pattern Retrieval Completed.");
        Debug.Log($"Retrieved Pattern: {string.Join(", ", retrievedPattern)}");

        VisualizePattern(retrievedPattern);
    }

    private int[] QuantumSearch(int[] inputPattern)
    {
        // Initialize the pattern retrieval process
        int[] state = new int[numNeurons];
        Array.Copy(inputPattern, state, numNeurons);

        for (int iteration = 0; iteration < maxIterations; iteration++)
        {
            if (logSteps)
                Debug.Log($"Iteration {iteration + 1}: Current State - {string.Join(", ", state)}");

            // Grover's Search for the Most Likely State
            state = ApplyGroversSearch(state);

            // Check for convergence
            if (IsConverged(state))
            {
                if (logSteps)
                    Debug.Log("Convergence Achieved.");
                break;
            }
        }

        return state;
    }

    private int[] ApplyGroversSearch(int[] state)
    {
        int[] newState = new int[numNeurons];
        float[] probabilities = new float[trainingPatterns.Count];

        // Calculate the probabilities for each stored pattern
        for (int i = 0; i < trainingPatterns.Count; i++)
        {
            probabilities[i] = CalculateOverlap(state, trainingPatterns[i]);
        }

        // Apply Grover's algorithm
        for (int iter = 0; iter < quantumIterations; iter++)
        {
            // Mark the target state
            int targetIndex = FindMaxIndex(probabilities);

            // Diffusion operator
            float mean = 0f;
            foreach (var prob in probabilities)
            {
                mean += prob;
            }
            mean /= probabilities.Length;

            for (int i = 0; i < probabilities.Length; i++)
            {
                probabilities[i] = 2 * mean - probabilities[i];
            }
        }

        // Find the most probable pattern
        int mostProbableIndex = FindMaxIndex(probabilities);
        Array.Copy(trainingPatterns[mostProbableIndex], newState, numNeurons);

        return newState;
    }

    private float CalculateOverlap(int[] state, int[] pattern)
    {
        float overlap = 0f;
        for (int i = 0; i < numNeurons; i++)
        {
            overlap += state[i] * pattern[i];
        }
        return overlap / numNeurons;
    }

    private bool IsConverged(int[] state)
    {
        // Check if the state matches any stored pattern
        foreach (var pattern in trainingPatterns)
        {
            bool match = true;
            for (int i = 0; i < numNeurons; i++)
            {
                if (state[i] != pattern[i])
                {
                    match = false;
                    break;
                }
            }
            if (match) return true;
        }
        return false;
    }

    private int FindMaxIndex(float[] array)
    {
        int maxIndex = 0;
        float maxValue = array[0];
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] > maxValue)
            {
                maxValue = array[i];
                maxIndex = i;
            }
        }
        return maxIndex;
    }

    private void VisualizePattern(int[] pattern)
    {
        if (neuronPrefab == null || visualizationContainer == null)
        {
            Debug.LogError("Visualization components are not set.");
            return;
        }

        // Clear previous visualization
        foreach (Transform child in visualizationContainer)
        {
            Destroy(child.gameObject);
        }

        // Visualize the pattern
        for (int i = 0; i < pattern.Length; i++)
        {
            GameObject neuron = Instantiate(neuronPrefab, visualizationContainer);
            neuron.transform.localPosition = new Vector3(i * 1.5f, 0f, 0f); // Adjust spacing
            neuron.name = $"Neuron {i}: {pattern[i]}";
            neuron.GetComponent<Renderer>().material.color = pattern[i] == 1 ? Color.green : Color.red;
        }
    }
}
