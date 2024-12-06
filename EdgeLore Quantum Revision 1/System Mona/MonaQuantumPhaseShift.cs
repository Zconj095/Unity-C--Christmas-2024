using UnityEngine;

public class MonaQuantumPhaseShift : MonoBehaviour
{
    public Vector3 QuantumState;

    public Vector3 ApplyPhaseShift(float angle)
    {
        Debug.Log($"Applying Phase Shift of {angle} radians to state: {QuantumState}");
        QuantumState = new Vector3(
            QuantumState.x * Mathf.Cos(angle) - QuantumState.y * Mathf.Sin(angle),
            QuantumState.x * Mathf.Sin(angle) + QuantumState.y * Mathf.Cos(angle),
            QuantumState.z
        );
        Debug.Log($"Phase Shifted State: {QuantumState}");
        return QuantumState;
    }
}
