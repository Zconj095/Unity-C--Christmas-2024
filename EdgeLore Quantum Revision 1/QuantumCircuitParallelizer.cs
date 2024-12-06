using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class QuantumCircuitParallelizer : MonoBehaviour
{
    [Header("Configuration")]
    [Tooltip("Number of experiments to run.")]
    [SerializeField] private int numExperiments = 10;

    [Tooltip("Enable detailed logging for debugging.")]
    [SerializeField] private bool enableDetailedLogging = true;

    [Tooltip("Maximum degree of parallelism.")]
    [SerializeField] private int maxDegreeOfParallelism = Environment.ProcessorCount;

    [Header("Results Visualization")]
    [Tooltip("Quantum visualizer for results.")]
    [SerializeField] private QuantumVisualizer visualizer;

    private List<QuantumState> experimentResults = new List<QuantumState>();
    private readonly object syncLock = new object();

    /// <summary>
    /// Runs quantum experiments in parallel with global synchronization.
    /// </summary>
    public async Task RunExperimentsAsync(List<string[]> experimentConfigurations)
    {
        if (experimentConfigurations == null || experimentConfigurations.Count == 0)
        {
            Debug.LogError("No experiment configurations provided.");
            return;
        }

        Debug.Log($"Starting {experimentConfigurations.Count} experiments in parallel...");

        await Task.Run(() =>
        {
            Parallel.ForEach(experimentConfigurations, new ParallelOptions { MaxDegreeOfParallelism = maxDegreeOfParallelism }, (config, state, index) =>
            {
                var experimentIndex = (int)index;
                ExecuteExperiment(config, experimentIndex);
            });
        });

        Debug.Log("All experiments executed in parallel.");
        VisualizeResults();
    }

    /// <summary>
    /// Executes a single quantum experiment with synchronized qubit operations.
    /// </summary>
    private void ExecuteExperiment(string[] config, int experimentIndex)
    {
        Debug.Log($"Running Experiment {experimentIndex}...");
        QuantumState state = new QuantumState(2); // Start with 2 qubits (example setup).

        foreach (var step in config)
        {
            if (enableDetailedLogging)
            {
                Debug.Log($"Experiment {experimentIndex}, Step: {step}");
            }

            lock (syncLock)
            {
                state = ApplyExperimentStep(step, state);
            }
        }

        int measurementResult;
        lock (syncLock)
        {
            measurementResult = state.Measure(); // Measure the quantum state after execution.
        }

        Debug.Log($"Experiment {experimentIndex} measured result: {measurementResult}");

        lock (experimentResults)
        {
            experimentResults.Add(state);
        }

        Debug.Log($"Experiment {experimentIndex} completed.");
    }

    /// <summary>
    /// Parses and applies an experiment step to the quantum state.
    /// </summary>
    private QuantumState ApplyExperimentStep(string step, QuantumState state)
    {
        string[] tokens = step.Split(' ');
        string operation = tokens[0];
        int[] qubits = Array.ConvertAll(tokens[1..], int.Parse);

        switch (operation.ToUpper())
        {
            case "H": // Hadamard gate
                state.ApplyGate(QuantumGate.Hadamard(qubits[0]));
                break;
            case "CNOT": // Controlled NOT gate
                if (qubits.Length >= 2)
                    state.ApplyGate(CNOT(qubits[0], qubits[1]));
                break;
            case "R": // Rotation gate
                if (qubits.Length >= 2 && double.TryParse(tokens[2], out double angle))
                    state.ApplyGate(Rotation(qubits[0], angle));
                break;
            default:
                Debug.LogWarning($"Unknown operation: {operation}");
                break;
        }

        return state;
    }

    /// <summary>
    /// Visualizes the results of all experiments.
    /// </summary>
    private void VisualizeResults()
    {
        if (visualizer == null)
        {
            Debug.LogError("QuantumVisualizer is not assigned. Skipping visualization.");
            return;
        }

        Debug.Log("Visualizing results of all experiments...");
        for (int i = 0; i < experimentResults.Count; i++)
        {
            visualizer.VisualizeStateEvolution(experimentResults[i]);
        }
    }

    /// <summary>
    /// Implements a synchronized Controlled NOT (CNOT) gate.
    /// </summary>
    public static QuantumGate CNOT(int control, int target)
    {
        return new QuantumGate($"CNOT[{control},{target}]", 2, new int[] { control, target }, state =>
        {
            int dim = state.Length;
            Complex[] newState = new Complex[dim];
            Array.Copy(state, newState, dim);

            for (int i = 0; i < dim; i++)
            {
                if (((i >> control) & 1) == 1) // If control qubit is 1
                {
                    int flipped = i ^ (1 << target); // Flip the target qubit
                    newState[flipped] = state[i];
                }
            }

            return newState;
        });
    }

    /// <summary>
    /// Implements a synchronized Rotation gate.
    /// </summary>
    public static QuantumGate Rotation(int qubit, double angle)
    {
        return new QuantumGate($"R[{qubit},{angle}]", 1, new int[] { qubit }, state =>
        {
            int dim = state.Length;
            Complex[] newState = new Complex[dim];
            Complex rotationFactor = new Complex(Math.Cos(angle), Math.Sin(angle));

            for (int i = 0; i < dim; i++)
            {
                int bit = (i >> qubit) & 1;
                newState[i] = state[i] * (bit == 1 ? rotationFactor : Complex.One);
            }

            return newState;
        });
    }
}
