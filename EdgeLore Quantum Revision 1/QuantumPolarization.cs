using UnityEngine;

public class QuantumPolarization : MonoBehaviour
{
    [SerializeField] private int numQubits = 1;
    [SerializeField] private Vector3[] polarizationVectors; // Polarization vectors for each qubit

    void Start()
    {
        InitializePolarization();
    }

    public void InitializePolarization()
    {
        polarizationVectors = new Vector3[numQubits];
        for (int i = 0; i < numQubits; i++)
        {
            // Default to |0⟩ state (Z-axis polarization)
            polarizationVectors[i] = new Vector3(0, 0, 1);
        }
        Debug.Log("Initialized polarization vectors.");
    }

    public void SetPolarization(int qubit, Vector3 newVector)
    {
        if (qubit < 0 || qubit >= numQubits)
        {
            Debug.LogError("Invalid qubit index.");
            return;
        }
        polarizationVectors[qubit] = newVector.normalized;
        Debug.Log($"Set polarization for qubit {qubit}: {newVector}");
    }

    public Vector3 GetPolarization(int qubit)
    {
        if (qubit < 0 || qubit >= numQubits)
        {
            Debug.LogError("Invalid qubit index.");
            return Vector3.zero;
        }
        return polarizationVectors[qubit];
    }
}
