using System;
using System.Collections.Generic;
using UnityEngine;

public class QuantumBayesianLearning : MonoBehaviour
{
    [Header("QBL Settings")]
    [SerializeField] private int numHypotheses = 8;         // Number of hypotheses
    [SerializeField] private float initialAmplitude = 0.1f; // Initial amplitude for each hypothesis
    [SerializeField] private float learningRate = 0.05f;    // Initial learning rate
    [SerializeField] private int numIterations = 10;        // Number of iterations
    [SerializeField] private bool logUpdates = true;        // Log updates for debugging

    [Header("Visualization")]
    [SerializeField] private GameObject hypothesisBarPrefab; // Prefab for hypothesis bars
    [SerializeField] private Transform visualizationContainer; // Parent for bars

    private float[] amplitudes; // Array of amplitudes for each hypothesis
    private float[] probabilities; // Array of probabilities for each hypothesis
    private List<GameObject> hypothesisBars; // Visualization bars

    [ContextMenu("Run Quantum Bayesian Learning")]
    public void RunQuantumBayesianLearning()
    {
        InitializeHypotheses();

        for (int iteration = 0; iteration < numIterations; iteration++)
        {
            if (logUpdates)
                Debug.Log($"Iteration {iteration + 1}");

            // Simulate observing data and updating probabilities
            int observedHypothesis = SimulateObservation();
            UpdateProbabilities(observedHypothesis);

            // Dynamically adjust learning rate
            AdjustLearningRate();

            if (logUpdates)
                LogProbabilities($"After Iteration {iteration + 1}");

            // Update visualization
            UpdateVisualization();
        }

        Debug.Log("Quantum Bayesian Learning Completed.");
        Debug.Log($"Most Probable Hypothesis: {GetMostProbableHypothesis()}");
    }

    private void InitializeHypotheses()
    {
        amplitudes = new float[numHypotheses];
        probabilities = new float[numHypotheses];
        hypothesisBars = new List<GameObject>();

        // Initialize amplitudes and probabilities
        for (int i = 0; i < numHypotheses; i++)
        {
            amplitudes[i] = initialAmplitude;
            probabilities[i] = Mathf.Pow(amplitudes[i], 2); // Probability is the square of the amplitude
        }

        // Set up visualization
        InitializeVisualization();
    }

    private int SimulateObservation()
    {
        // Simulate an observation based on the current probabilities
        float totalProbability = 0f;
        foreach (float prob in probabilities)
        {
            totalProbability += prob;
        }

        float randomValue = UnityEngine.Random.value * totalProbability;
        float cumulativeProbability = 0f;

        for (int i = 0; i < probabilities.Length; i++)
        {
            cumulativeProbability += probabilities[i];
            if (randomValue <= cumulativeProbability)
            {
                if (logUpdates)
                    Debug.Log($"Observed Hypothesis: {i}");
                return i;
            }
        }

        return probabilities.Length - 1; // Default to the last hypothesis if none selected
    }

    private void UpdateProbabilities(int observedHypothesis)
    {
        // Update amplitudes and probabilities based on observed data
        for (int i = 0; i < amplitudes.Length; i++)
        {
            if (i == observedHypothesis)
            {
                amplitudes[i] += learningRate; // Increase amplitude for observed hypothesis
            }
            else
            {
                amplitudes[i] -= learningRate / (amplitudes.Length - 1); // Decrease amplitude for others
            }

            amplitudes[i] = Mathf.Clamp(amplitudes[i], 0f, 1f); // Ensure amplitudes are valid
            probabilities[i] = Mathf.Pow(amplitudes[i], 2); // Recalculate probabilities
        }
    }

    private void AdjustLearningRate()
    {
        // Adjust the learning rate dynamically based on the confidence in the current state
        float entropy = 0f;
        foreach (float prob in probabilities)
        {
            if (prob > 0)
                entropy -= prob * Mathf.Log(prob);
        }

        learningRate = Mathf.Clamp(0.1f / (entropy + 1f), 0.01f, 0.1f); // Adjust learning rate inversely to entropy
        if (logUpdates)
            Debug.Log($"Adjusted Learning Rate: {learningRate}");
    }

    private int GetMostProbableHypothesis()
    {
        int maxIndex = 0;
        float maxValue = probabilities[0];
        for (int i = 1; i < probabilities.Length; i++)
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
        for (int i = 0; i < probabilities.Length; i++)
        {
            Debug.Log($"Hypothesis {i}: Amplitude = {amplitudes[i]}, Probability = {probabilities[i]}");
        }
    }

    private void InitializeVisualization()
    {
        if (hypothesisBarPrefab == null || visualizationContainer == null)
        {
            Debug.LogError("Visualization components are not set.");
            return;
        }

        // Clear previous visualization
        foreach (Transform child in visualizationContainer)
        {
            Destroy(child.gameObject);
        }

        hypothesisBars.Clear();

        // Create bars for each hypothesis
        for (int i = 0; i < numHypotheses; i++)
        {
            GameObject bar = Instantiate(hypothesisBarPrefab, visualizationContainer);
            bar.name = $"Hypothesis {i}";
            bar.transform.localPosition = new Vector3(i * 1.5f, 0f, 0f); // Adjust spacing
            hypothesisBars.Add(bar);
        }
    }

    private void UpdateVisualization()
    {
        for (int i = 0; i < hypothesisBars.Count; i++)
        {
            float height = probabilities[i];
            hypothesisBars[i].transform.localScale = new Vector3(1f, height, 1f);
        }
    }
}
