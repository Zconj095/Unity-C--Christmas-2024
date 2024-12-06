using System.Collections.Generic;
using UnityEngine;

public class QuantumExecutor : MonoBehaviour
{
    [SerializeField] private int numQubits = 3;
    [SerializeField] private List<string> operations = new List<string>();

    public void ExecuteCircuit()
    {
        Debug.Log($"Executing quantum circuit with {numQubits} qubits.");

        foreach (string operation in operations)
        {
            Debug.Log($"Executing operation: {operation}");
            // Add operation-specific logic here
        }

        Debug.Log("Quantum circuit execution completed.");
    }

    public void AddOperation(string operation)
    {
        operations.Add(operation);
        Debug.Log($"Added operation: {operation}");
    }
}
