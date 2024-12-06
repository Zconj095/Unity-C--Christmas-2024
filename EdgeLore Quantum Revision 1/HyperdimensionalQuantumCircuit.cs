using System.Collections.Generic;
using UnityEngine;

public class HyperdimensionalQuantumCircuit : MonoBehaviour
{
    [SerializeField] private int numQubits = 5; // Number of qubits in the circuit
    [SerializeField] private List<string> gates = new List<string>(); // Gates applied in the circuit

    public void ApplyGate(string gate, int[] targetQubits)
    {
        string gateOperation = $"{gate} on Qubits: {string.Join(", ", targetQubits)}";
        gates.Add(gateOperation);
        Debug.Log($"Applied Gate: {gateOperation}");
    }

    public void PrintCircuit()
    {
        Debug.Log("Hyperdimensional Quantum Circuit:");
        foreach (var gate in gates)
        {
            Debug.Log(gate);
        }
    }
}
