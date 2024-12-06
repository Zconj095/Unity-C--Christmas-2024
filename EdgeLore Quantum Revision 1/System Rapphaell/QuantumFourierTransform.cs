using UnityEngine;

public class QuantumFourierTransform : MonoBehaviour
{
    public Vector3 QuantumState;

    public Vector3 ApplyQFT()
    {
        Debug.Log($"Applying Quantum Fourier Transform to state: {QuantumState}");
        QuantumState = new Vector3(
            Mathf.Cos(QuantumState.x) - Mathf.Sin(QuantumState.y),
            Mathf.Sin(QuantumState.x) + Mathf.Cos(QuantumState.y),
            QuantumState.z
        );
        Debug.Log($"Transformed State: {QuantumState}");
        return QuantumState; // Ensure a return value
    }

}
