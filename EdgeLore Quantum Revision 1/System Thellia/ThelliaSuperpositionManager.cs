using UnityEngine;

public class ThelliaSuperpositionManager : MonoBehaviour
{
    public Vector3 QuantumState;

    public Vector3 CollapseToSuperposition()
    {
        // Apply Hadamard gate logic
        Debug.Log($"Collapsing quantum state {QuantumState} to superposition...");

        QuantumState = new Vector3(QuantumState.x / Mathf.Sqrt(2), QuantumState.y / Mathf.Sqrt(2), QuantumState.z / Mathf.Sqrt(2));
        Debug.Log($"Superposition State: {QuantumState}");

        return QuantumState;
    }
}
