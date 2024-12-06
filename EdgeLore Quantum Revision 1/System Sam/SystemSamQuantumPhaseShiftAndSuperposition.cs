using UnityEngine;

public class SystemSamQuantumPhaseShiftAndSuperposition : MonoBehaviour
{
    public Vector3 InputState;

    public Vector3 ApplyPhaseShift(float angle)
    {
        Debug.Log($"Applying Phase Shift of {angle} radians...");
        Vector3 phaseShiftedState = new Vector3(
            InputState.x * Mathf.Cos(angle) - InputState.y * Mathf.Sin(angle),
            InputState.x * Mathf.Sin(angle) + InputState.y * Mathf.Cos(angle),
            InputState.z
        );
        Debug.Log($"Phase-Shifted State: {phaseShiftedState}");
        return phaseShiftedState;
    }

    public Vector3 GenerateSuperposition()
    {
        Debug.Log("Generating Superposition...");
        Vector3 superposedState = InputState / Mathf.Sqrt(3.0f); // Normalize for superposition
        Debug.Log($"Superposition State: {superposedState}");
        return superposedState;
    }
}
