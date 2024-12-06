using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemThellia : MonoBehaviour
{
    void Start()
    {
        // Initialize qubits
        Qubit qubit1 = new Qubit(1f, 0f); // |0⟩
        Qubit qubit2 = new Qubit(0f, 1f); // |1⟩
        Qubit target = new Qubit(1f, 0f); // |0⟩

        // Apply Toffoli Gate
        Qubit toffoliOutput = QuantumGates.ApplyToffoli(qubit1, qubit2, target);

        // Apply Hadamard Gates
        Qubit hadamardLeft = QuantumGates.ApplyHadamard(toffoliOutput);
        Qubit hadamardRight = QuantumGates.ApplyHadamard(toffoliOutput);

        // Simulate Quantum Teleportation
        Qubit teleportedQubit = QuantumGates.QuantumTeleport(toffoliOutput);

        // Create Quantum Hyperstate
        ThelliaQuantumHyperstate hyperstate = new ThelliaQuantumHyperstate();
        hyperstate.AddQubit(hadamardLeft);
        hyperstate.AddQubit(hadamardRight);
        hyperstate.AddQubit(teleportedQubit);

        // Output the result
        Debug.Log(hyperstate.ToString());
    }
}
