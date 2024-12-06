using UnityEngine;

public class HadamardCircuit : MonoBehaviour
{
    public void ApplyHadamard(int numQubits)
    {
        Debug.Log($"Applying Hadamard gates to {numQubits} qubits...");
        for (int i = 0; i < numQubits; i++)
        {
            Debug.Log($"H Gate applied to Qubit {i}");
        }
        Debug.Log("All qubits are now in superposition.");
    }
}
