using UnityEngine;

public class PauliLindbladAlgorithm : MonoBehaviour
{
    [SerializeField] private double[] pauliRates = { 0.1, 0.1, 0.1 }; // X, Y, Z rates
    [SerializeField] private int numQubits = 1;

    public Vector3 ApplyPauliLindblad(Vector3 initialState)
    {
        // Simulate the effect of Pauli noise
        float px = (float)pauliRates[0];
        float py = (float)pauliRates[1];
        float pz = (float)pauliRates[2];

        // Apply noise to each axis
        Vector3 noisyState = new Vector3(
            initialState.x * (1 - px),
            initialState.y * (1 - py),
            initialState.z * (1 - pz)
        );

        Debug.Log($"Applied Pauli-Lindblad noise. Resulting state: {noisyState}");
        return noisyState;
    }
}
