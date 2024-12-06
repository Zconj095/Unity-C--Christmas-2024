using System;
using System.Collections.Generic;
using UnityEngine;

public class GroversQuantumHebbianLearning : MonoBehaviour
{
    [Header("Hebbian Network Settings")]
    [SerializeField] private int numNeurons = 8;             // Number of neurons
    [SerializeField] private List<int[]> trainingPatterns;   // Patterns to store
    [SerializeField] private float learningRate = 0.1f;      // Hebbian learning rate
    [SerializeField] private int maxIterations = 10;         // Maximum retrieval iterations

    [Header("Grover's Search Settings")]
    [SerializeField] private int quantumIterations = 2;      // Grover's search iterations

    private float[,] weightMatrix;                           // Hebbian weight matrix
    private int[] currentState;                              // Current state vector

    [ContextMenu("Train Network")]
    public void TrainNetwork()
    {
        // Initialize weight matrix
        weightMatrix = new float[numNeurons, numNeurons];
        foreach (var pattern in trainingPatterns)
        {
            for (int i = 0; i < numNeurons; i++)
            {
                for (int j = 0; j < numNeurons; j++)
                {
                    if (i != j)
                    {
                        weightMatrix[i, j] += learningRate * pattern[i] * pattern[j];
                    }
                }
            }
        }

        Debug.Log("Hebbian Learning Complete. Weight Matrix Initialized.");
    }

    [ContextMenu("Retrieve Pattern")]
    public void RetrievePattern()
    {
        if (currentState == null || currentState.Length != numNeurons)
        {
            Debug.LogError("Current state is not set or invalid.");
            return;
        }

        Debug.Log("Starting Retrieval...");
        int[] retrievedPattern = ApplyGroversSearch(currentState);
        Debug.Log("Retrieval Completed.");
        Debug.Log($"Retrieved Pattern: {string.Join(", ", retrievedPattern)}");
    }

    private int[] ApplyGroversSearch(int[] state)
    {
        int[] newState = new int[numNeurons];
        float[] probabilities = new float[trainingPatterns.Count];

        // Calculate pattern probabilities
        for (int i = 0; i < trainingPatterns.Count; i++)
        {
            probabilities[i] = CalculateOverlap(state, trainingPatterns[i]);
        }

        // Apply Grover's search
        for (int iter = 0; iter < quantumIterations; iter++)
        {
            int targetIndex = FindMaxIndex(probabilities);

            float mean = 0f;
            foreach (var prob in probabilities)
                mean += prob;
            mean /= probabilities.Length;

            for (int i = 0; i < probabilities.Length; i++)
                probabilities[i] = 2 * mean - probabilities[i];
        }

        int mostProbableIndex = FindMaxIndex(probabilities);
        Array.Copy(trainingPatterns[mostProbableIndex], newState, numNeurons);

        return newState;
    }

    private float CalculateOverlap(int[] state, int[] pattern)
    {
        float overlap = 0f;
        for (int i = 0; i < numNeurons; i++)
            overlap += state[i] * pattern[i];
        return overlap / numNeurons;
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
}
