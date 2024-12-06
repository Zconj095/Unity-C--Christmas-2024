using UnityEngine;

public class QuantumVector : MonoBehaviour
{
    public Vector3 QuantumState = new Vector3(1, 0, 0); // Default quantum vector state

    public Vector3 Generate()
    {
        Debug.Log($"Generating Quantum Vector: {QuantumState}");
        return QuantumState;
    }
}
