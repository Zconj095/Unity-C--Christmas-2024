using UnityEngine;

public class QuantumFrameworkManager : MonoBehaviour
{
    [Header("Framework Components")]
    [Tooltip("Visualizer for quantum operations.")]
    public QuantumVisualizer visualizer;

    [Tooltip("Number of primary qubits to initialize in the circuit.")]
    public int initialQubitCount = 2;

    [Tooltip("Number of ancilla qubits to initialize in the circuit.")]
    public int initialAncillaCount = 1;

    private QuantumCircuit quantumCircuit;
    private AncillaManager ancillaManager;

    void Start()
    {
        Debug.Log("QuantumFrameworkManager: Initializing framework...");

        // Validate dependencies
        if (visualizer == null)
        {
            Debug.LogError("QuantumFrameworkManager: QuantumVisualizer is not assigned.");
            return;
        }

        InitializeFramework();
    }

    /// <summary>
    /// Initializes the Quantum Framework with all necessary components.
    /// </summary>
    private void InitializeFramework()
    {
        // Create and initialize QuantumCircuit
        GameObject circuitObject = new GameObject("QuantumCircuit");
        quantumCircuit = circuitObject.AddComponent<QuantumCircuit>();
        quantumCircuit.SetNumQubits(initialQubitCount); // Use SetNumQubits for safe modification

        Debug.Log($"QuantumFrameworkManager: QuantumCircuit initialized with {initialQubitCount} qubits.");

        // Create and initialize AncillaManager
        GameObject ancillaObject = new GameObject("AncillaManager");
        ancillaManager = ancillaObject.AddComponent<AncillaManager>();
        ancillaManager.primaryQubitCount = initialQubitCount;
        ancillaManager.ancillaQubitCount = initialAncillaCount;
        ancillaManager.InitializeRegisters();

        Debug.Log($"QuantumFrameworkManager: AncillaManager initialized with {initialAncillaCount} ancilla qubits.");

        // Setup visualization
        visualizer.Initialize(quantumCircuit.NumQubits);
        Debug.Log("QuantumFrameworkManager: Visualizer initialized successfully.");

        // Example quantum operations
        SetupExampleOperations();
    }

    /// <summary>
    /// Sets up example quantum operations to demonstrate the framework.
    /// </summary>
    private void SetupExampleOperations()
    {
        Debug.Log("QuantumFrameworkManager: Setting up example operations...");

        // Add qubits and ancilla
        quantumCircuit.AddQubit();
        ancillaManager.AddAncilla();

        // Add example gates
        quantumCircuit.AddGate(new QuantumGate("Hadamard", 1, new int[] { 0 }, state =>
        {
            Debug.Log("Applying Hadamard Gate...");
            return state; // Placeholder
        }));

        quantumCircuit.AddGate(new QuantumGate("CNOT", 2, new int[] { 0, 1 }, state =>
        {
            Debug.Log("Applying CNOT Gate...");
            return state; // Placeholder
        }));

        Debug.Log("QuantumFrameworkManager: Example operations setup complete.");

        // Execute the circuit
        ExecuteCircuit();
    }

    /// <summary>
    /// Executes the quantum circuit and visualizes the result.
    /// </summary>
    private void ExecuteCircuit()
    {
        Debug.Log("QuantumFrameworkManager: Executing quantum circuit...");

        QuantumState finalState = quantumCircuit.Execute();

        Debug.Log($"QuantumFrameworkManager: Final state of the circuit: {finalState}");

        // Visualize the final state
        visualizer.VisualizeStateEvolution(finalState);
    }
}
