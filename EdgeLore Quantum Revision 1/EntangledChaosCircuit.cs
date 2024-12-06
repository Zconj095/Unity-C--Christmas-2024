using System;
using UnityEngine;

public class EntangledChaosCircuit : MonoBehaviour
{
    private System.Random random = new System.Random();

    public void CreateEntangledChaos(int numQubits)
    {
        Debug.Log($"Creating entangled chaos with {numQubits} qubits...");

        // Create initial entanglement
        Debug.Log("Creating entangled state...");
        var entangledStateCircuit = gameObject.AddComponent<EntangledStateCircuit>();
        entangledStateCircuit.CreateEntangledState(numQubits);

        // Introduce chaos by applying random gates
        for (int i = 0; i < numQubits; i++)
        {
            string randomGate = GetRandomGate();
            Debug.Log($"Applying {randomGate} Gate to Qubit {i}.");
        }

        Debug.Log("Entangled chaos state created.");
    }

    private string GetRandomGate()
    {
        string[] gates = { "X", "Y", "Z", "H", "S", "T" };
        return gates[random.Next(gates.Length)];
    }
}
