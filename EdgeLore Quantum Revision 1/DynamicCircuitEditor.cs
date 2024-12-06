using UnityEngine;

public class DynamicCircuitEditor : MonoBehaviour
{
    public QuantumCircuit circuit;

    void Start()
    {
        circuit = new QuantumCircuit(2); // Example with 2 qubits
    }

    public void AddGate(string gateType, int targetQubit, int controlQubit = -1)
    {
        QuantumGate gate = null;

        switch (gateType)
        {
            case "Hadamard":
                gate = QuantumGate.Hadamard(targetQubit);
                break;
            case "CNOT":
                if (controlQubit >= 0)
                    gate = QuantumGate.CNOT(controlQubit, targetQubit);
                break;
            // Add cases for other gates
        }

        if (gate != null)
        {
            circuit.AddGate(gate);
            Debug.Log($"Added {gateType} gate to qubit {targetQubit}.");
        }
    }

    public void RemoveGate(int gateIndex)
    {
        if (gateIndex >= 0 && gateIndex < circuit.Gates.Count)
        {
            circuit.Gates.RemoveAt(gateIndex);
            Debug.Log($"Removed gate at index {gateIndex}.");
        }
        else
        {
            Debug.LogWarning("Invalid gate index.");
        }
    }

    public void ReorderGate(int fromIndex, int toIndex)
    {
        if (fromIndex >= 0 && fromIndex < circuit.Gates.Count && toIndex >= 0 && toIndex < circuit.Gates.Count)
        {
            QuantumGate gate = circuit.Gates[fromIndex];
            circuit.Gates.RemoveAt(fromIndex);
            circuit.Gates.Insert(toIndex, gate);
            Debug.Log($"Reordered gate from index {fromIndex} to index {toIndex}.");
        }
        else
        {
            Debug.LogWarning("Invalid gate indices for reordering.");
        }
    }
}
