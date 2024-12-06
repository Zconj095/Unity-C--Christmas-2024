using System;
using System.Collections.Generic;
using UnityEngine;

public class GroversAlgorithm : MonoBehaviour
{
    [Header("Grover's Algorithm Settings")]
    [SerializeField] private int numQubits = 3;             // Number of qubits
    [SerializeField] private int targetState = 5;           // Target state to search for
    [SerializeField] private int iterations = 2;            // Number of iterations
    [SerializeField] private bool logIntermediateStates = true; // Log states after each step

    [Header("Visualization")]
    [SerializeField] private bool enableVisualization = true; // Toggle visualization
    [SerializeField] private GameObject probabilityBarPrefab; // Prefab for probability bars
    [SerializeField] private Transform visualizationContainer; // Parent for probability bars

    [Header("Results")]
    [SerializeField] private List<float> probabilities;     // Probabilities of each state

    private List<GameObject> probabilityBars;               // List of bar GameObjects

    // Updated to return the most probable state
    public Vector3 RunGrover()
    {
        if (numQubits < 1 || targetState >= (1 << numQubits))
        {
            Debug.LogError("Invalid settings for Grover's Algorithm. Check numQubits and targetState.");
            return Vector3.zero; // Default return if invalid configuration
        }

        Debug.Log($"Running Grover's Algorithm with {numQubits} qubits and target state {targetState}.");

        // Initialize quantum states
        int totalStates = 1 << numQubits; // 2^numQubits

        // Initialize the probabilities list with correct size
        probabilities = new List<float>();
        for (int i = 0; i < totalStates; i++)
        {
            probabilities.Add(1f / totalStates); // Equal superposition
        }

        if (enableVisualization)
            InitializeVisualization(totalStates);

        for (int iteration = 0; iteration < iterations; iteration++)
        {
            if (logIntermediateStates)
                LogProbabilities($"Before Iteration {iteration + 1}");

            // Apply Oracle
            ApplyOracle();

            // Apply Diffusion Operator
            ApplyDiffusionOperator();

            if (enableVisualization)
                UpdateVisualization();

            if (logIntermediateStates)
                LogProbabilities($"After Iteration {iteration + 1}");
        }

        Debug.Log("Grover's Algorithm Completed.");
        int mostProbableState = GetMostProbableState();
        Debug.Log($"Most Probable State: {mostProbableState}");

        // Convert most probable state to a vector (example encoding)
        Vector3 outputState = new Vector3(mostProbableState, probabilities[mostProbableState], iterations);
        return outputState;
    }

    private void ApplyOracle()
    {
        // Flip the amplitude of the target state
        if (targetState >= 0 && targetState < probabilities.Count)
        {
            probabilities[targetState] *= -1f;
        }
        else
        {
            Debug.LogError($"Target state {targetState} is out of bounds for probabilities list.");
        }
    }

    private void ApplyDiffusionOperator()
    {
        float meanAmplitude = 0f;

        // Calculate the mean amplitude
        foreach (float prob in probabilities)
        {
            meanAmplitude += prob;
        }
        meanAmplitude /= probabilities.Count;

        // Reflect all amplitudes about the mean
        for (int i = 0; i < probabilities.Count; i++)
        {
            probabilities[i] = 2f * meanAmplitude - probabilities[i];
        }
    }

    private int GetMostProbableState()
    {
        int maxIndex = 0;
        float maxValue = probabilities[0];
        for (int i = 1; i < probabilities.Count; i++)
        {
            if (probabilities[i] > maxValue)
            {
                maxValue = probabilities[i];
                maxIndex = i;
            }
        }
        return maxIndex;
    }

    private void LogProbabilities(string message)
    {
        Debug.Log(message);
        for (int i = 0; i < probabilities.Count; i++)
        {
            Debug.Log($"State |{i}>: {probabilities[i]}");
        }
    }

    private void InitializeVisualization(int totalStates)
    {
        if (probabilityBarPrefab == null || visualizationContainer == null)
        {
            Debug.LogError("Probability visualization components are not set.");
            return;
        }

        // Clear previous bars
        foreach (Transform child in visualizationContainer)
        {
            Destroy(child.gameObject);
        }

        probabilityBars = new List<GameObject>();

        // Create new bars
        for (int i = 0; i < totalStates; i++)
        {
            GameObject bar = Instantiate(probabilityBarPrefab, visualizationContainer);
            bar.name = $"State |{i}>";
            bar.transform.localPosition = new Vector3(i * 1.5f, 0f, 0f); // Adjust spacing
            probabilityBars.Add(bar);
        }

        // Ensure the number of bars matches the number of states
        if (probabilityBars.Count != totalStates)
        {
            Debug.LogError($"Number of probability bars ({probabilityBars.Count}) does not match total states ({totalStates}).");
        }
    }

    private void UpdateVisualization()
    {
        if (probabilityBars == null)
        {
            Debug.LogError("Probability bars are not initialized.");
            return;
        }

        if (probabilityBars.Count != probabilities.Count)
        {
            Debug.LogError($"Probability bars count ({probabilityBars.Count}) does not match probabilities count ({probabilities.Count}).");
            return;
        }

        for (int i = 0; i < probabilities.Count; i++)
        {
            // Update the height of each bar to reflect probability
            float height = Mathf.Abs(probabilities[i]);
            probabilityBars[i].transform.localScale = new Vector3(1f, height, 1f);
        }
    }
}
