using UnityEngine;

public class MonaQuantumPhaseEstimation : MonoBehaviour
{
    public Vector3 QuantumState;

    public Vector3 EstimatePhase()
    {
        Debug.Log($"Estimating Phase for State: {QuantumState}");
        Vector3 phase = new Vector3(
            QuantumState.x * Mathf.PI,
            QuantumState.y * Mathf.PI,
            QuantumState.z * Mathf.PI
        );
        Debug.Log($"Estimated Phase: {phase}");
        return phase;
    }
}
