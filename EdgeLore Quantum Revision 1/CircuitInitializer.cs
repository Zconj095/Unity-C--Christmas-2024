using System.Collections.Generic; // Added for List<>
using UnityEngine;

public class CircuitInitializer : MonoBehaviour
{
    [Header("Circuit Configuration")]
    [SerializeField] private int numQubits = 3;
    [SerializeField] private int numAncillas = 2;

    [Header("Initial State")]
    [SerializeField] private string initialState = "|0⟩";

    private AncillaManager ancillaManager;
    private CircuitAnalysisPass analysisPass;

    void Start()
    {
        InitializeCircuit();
    }

    private void InitializeCircuit()
    {
        // Create ancilla manager and analysis pass
        ancillaManager = gameObject.AddComponent<AncillaManager>();
        ancillaManager.primaryQubitCount = numQubits;
        ancillaManager.ancillaQubitCount = numAncillas;

        analysisPass = gameObject.AddComponent<CircuitAnalysisPass>();

        // Initialize the registers
        ancillaManager.InitializeRegisters();

        Debug.Log($"Circuit Initialized with {numQubits} qubits and {numAncillas} ancillas.");
        Debug.Log($"Initial State: {initialState}");
    }

    public void AddGate(string type, string qubit)
    {
        analysisPass.AddGate(type, new List<string> { qubit });
        Debug.Log($"Added Gate: {type} on Qubit: {qubit}");
    }

    public void RunAnalysis()
    {
        analysisPass.AnalyzeCircuit();
    }
}
