using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;

public class QuantumCircuitHyperthreader : MonoBehaviour
{
    [Header("Quantum Circuit Configuration")]
    [Tooltip("Number of qubits in the circuit.")]
    [SerializeField] private int numQubits = 5;

    [Tooltip("Quantum gates in the circuit, represented as strings (e.g., 'H 0', 'CNOT 0 1').")]
    [SerializeField] private List<string> circuitGates = new List<string>();

    [Header("Performance Settings")]
    [Tooltip("Enable adaptive hyperthreading.")]
    [SerializeField] private bool adaptiveHyperthreading = true;

    [Tooltip("Maximum parallel tasks.")]
    [SerializeField] private int maxParallelTasks = Environment.ProcessorCount;

    [Header("Results Visualization")]
    [Tooltip("Quantum visualizer for simulation results.")]
    [SerializeField] private QuantumVisualizer visualizer;

    private QuantumState quantumState;

    /// <summary>
    /// Initializes the quantum state for the simulation.
    /// </summary>
    private void InitializeState()
    {
        quantumState = new QuantumState(numQubits);
        Debug.Log($"Quantum state initialized with {numQubits} qubits.");
    }

    /// <summary>
    /// Simulates the quantum circuit with hyperthreading and adaptive capabilities.
    /// </summary>
    public async void SimulateCircuit()
    {
        Debug.Log("Starting Quantum Circuit Simulation with Hyperthreading...");

        InitializeState();

        if (circuitGates == null || circuitGates.Count == 0)
        {
            Debug.LogError("No gates provided for simulation.");
            return;
        }

        // Divide gates into parallelizable tasks
        var tasks = new List<Task>();
        int batchSize = adaptiveHyperthreading ? Math.Max(1, circuitGates.Count / maxParallelTasks) : 1;

        for (int i = 0; i < circuitGates.Count; i += batchSize)
        {
            int start = i;
            int end = Math.Min(i + batchSize, circuitGates.Count);
            tasks.Add(Task.Run(() => SimulateGateBatch(circuitGates.GetRange(start, end))));
        }

        // Wait for all tasks to complete
        await Task.WhenAll(tasks);
        Debug.Log("Quantum Circuit Simulation Completed.");

        // Visualize the final quantum state
        visualizer?.VisualizeStateEvolution(quantumState);
    }

    /// <summary>
    /// Simulates a batch of quantum gates.
    /// </summary>
    private void SimulateGateBatch(List<string> gates)
    {
        foreach (string gate in gates)
        {
            ApplyGate(gate);
        }
    }

    /// <summary>
    /// Parses and applies a quantum gate to the quantum state.
    /// </summary>
    private void ApplyGate(string gate)
    {
        string[] tokens = gate.Split(' ');
        string operation = tokens[0];
        int[] qubits = Array.ConvertAll(tokens[1..], int.Parse);

        switch (operation.ToUpper())
        {
            case "H": // Hadamard gate
                quantumState.ApplyGate(QuantumGate.Hadamard(qubits[0]));
                break;
            case "CNOT": // Controlled NOT gate
                if (qubits.Length >= 2)
                    quantumState.ApplyGate(QuantumGate.CNOT(qubits[0], qubits[1]));
                break;
            case "R": // Rotation gate
                if (qubits.Length >= 2 && double.TryParse(tokens[2], out double angle))
                    quantumState.ApplyGate(QuantumGate.Rotation(qubits[0], angle));
                break;
            default:
                Debug.LogWarning($"Unknown operation: {operation}");
                break;
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
