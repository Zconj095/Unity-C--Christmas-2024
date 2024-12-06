using UnityEngine;

public class PauliLindbladCircuit : MonoBehaviour
{
    [SerializeField] private double[] pauliRates = { 0.1, 0.1, 0.1 }; // X, Y, Z noise rates

    public void ApplyPauliLindblad(Vector3 initialState)
    {
        Debug.Log("Applying Pauli-Lindblad noise...");

        float px = (float)pauliRates[0];
        float py = (float)pauliRates[1];
        float pz = (float)pauliRates[2];

        Vector3 noisyState = new Vector3(
            initialState.x * (1 - px),
            initialState.y * (1 - py),
            initialState.z * (1 - pz)
        );

        Debug.Log($"Resulting state after noise: {noisyState}");
    }
}
