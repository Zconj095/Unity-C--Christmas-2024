using System.Collections.Generic;
using UnityEngine;

public class CircuitAnalysisPass : MonoBehaviour
{
    public class Gate
    {
        public string Type { get; set; }
        public List<string> Qubits { get; set; }

        public Gate(string type, List<string> qubits)
        {
            Type = type;
            Qubits = qubits;
        }
    }

    [SerializeField] private List<Gate> quantumCircuit = new List<Gate>();

    public void AnalyzeCircuit()
    {
        Dictionary<string, int> gateCounts = new Dictionary<string, int>();
        int circuitDepth = 0;

        foreach (var gate in quantumCircuit)
        {
            if (!gateCounts.ContainsKey(gate.Type))
                gateCounts[gate.Type] = 0;
            gateCounts[gate.Type]++;
        }

        circuitDepth = CalculateCircuitDepth();

        Debug.Log("Circuit Analysis:");
        foreach (var entry in gateCounts)
        {
            Debug.Log($"Gate: {entry.Key}, Count: {entry.Value}");
        }
        Debug.Log($"Circuit Depth: {circuitDepth}");
    }

    private int CalculateCircuitDepth()
    {
        // Example: Assume each gate adds 1 to the depth for simplicity.
        return quantumCircuit.Count;
    }

    public void AddGate(string type, List<string> qubits)
    {
        quantumCircuit.Add(new Gate(type, qubits));
    }
}
