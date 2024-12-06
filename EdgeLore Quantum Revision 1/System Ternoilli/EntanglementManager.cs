using UnityEngine;

public class EntanglementManager : MonoBehaviour
{
    public void Entangle(Vector2 superposedState, float flux)
    {
        Debug.Log($"Entangling state {superposedState} with flux {flux}");

        // Step 1: Normalize the flux to use as a probability weight
        float normalizedFlux = Mathf.Clamp01(flux / 10.0f); // Assuming flux ranges between 0 and 10
        Debug.Log($"Normalized Flux: {normalizedFlux}");

        // Step 2: Create an entangled pair using the superposed state and normalized flux
        Vector2 entangledStateA = superposedState * normalizedFlux;
        Vector2 entangledStateB = new Vector2(-superposedState.x, -superposedState.y) * (1 - normalizedFlux);

        // Step 3: Combine the entangled states into a final output
        Vector2 finalEntangledState = Vector2.Lerp(entangledStateA, entangledStateB, normalizedFlux);

        // Step 4: Output the entangled result
        Debug.Log($"Entangled State A: {entangledStateA}");
        Debug.Log($"Entangled State B: {entangledStateB}");
        Debug.Log($"Final Entangled State: {finalEntangledState}");
    }
}
