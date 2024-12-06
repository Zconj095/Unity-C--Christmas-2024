using UnityEngine;

public class EntangledStateCircuit : MonoBehaviour
{
    public void CreateEntangledState(int numQubits)
    {
        Debug.Log($"Creating entangled state with {numQubits} qubits...");

        // Apply Hadamard gate to the first qubit
        Debug.Log("H Gate applied to Qubit 0.");

        // Apply controlled-X gates to create entanglement
        for (int i = 1; i < numQubits; i++)
        {
            Debug.Log($"CNOT Gate applied between Qubit 0 and Qubit {i}.");
        }

        Debug.Log("Entangled state created.");
    }
}
