using System;
using UnityEngine;

public class GroversSearch : QuantumAlgorithm
{
    public GroversSearch() : base("Grover's Search") { }

    public override void Execute(QuantumCircuit circuit, QuantumSimulator simulator)
    {
        Debug.Log("Executing Grover's Search...");

        // Apply Hadamard to all qubits
        for (int i = 0; i < circuit.NumQubits; i++)
        {
            circuit.AddGate(QuantumGate.Hadamard(i));
        }

        simulator.Simulate();
    }
}
