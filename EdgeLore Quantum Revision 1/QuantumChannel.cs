using UnityEngine;

public class QuantumChannel : MonoBehaviour
{
    [SerializeField] private double depolarizingProbability = 0.1;

    public Vector3 ApplyChannel(Vector3 inputState)
    {
        // Example: Depolarizing channel
        float probability = (float)depolarizingProbability; // Cast double to float
        Vector3 outputState = inputState * (1 - probability); // Use the casted value
        Debug.Log($"Applied depolarizing channel. Output State: {outputState}");
        return outputState;
    }
}
