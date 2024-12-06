using UnityEngine;

public class QuantumPhaseEstimation : MonoBehaviour
{
    public Vector3 QuantumState;

    public Vector3 EstimatePhase()
    {
        Debug.Log($"Estimating phase for state: {QuantumState}");
        QuantumState = new Vector3(
            QuantumState.x * Mathf.PI,
            QuantumState.y * Mathf.PI,
            QuantumState.z * Mathf.PI
        );
        Debug.Log($"Phase Estimated State: {QuantumState}");
        return QuantumState; // Ensure a return value
    }

}
