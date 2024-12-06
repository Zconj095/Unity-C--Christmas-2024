using UnityEngine;

public class ChaosStateCircuit : MonoBehaviour
{
    public void CreateChaosState(int numQubits)
    {
        Debug.Log($"Creating chaos state with {numQubits} qubits...");

        for (int i = 0; i < numQubits; i++)
        {
            Debug.Log($"Applying H Gate to Qubit {i} (superposition).");
            Debug.Log($"Applying Z Gate to Qubit {i} (phase flip).");
            Debug.Log($"Applying X Gate to Qubit {i} (bit flip).");
        }

        Debug.Log("Chaos state created.");
    }
}
