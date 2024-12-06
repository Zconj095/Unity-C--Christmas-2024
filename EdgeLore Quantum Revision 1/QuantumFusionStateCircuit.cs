using UnityEngine;

public class QuantumFusionStateCircuit : MonoBehaviour
{
    public void CreateFusionState(int numStates)
    {
        Debug.Log($"Fusing {numStates} quantum states...");

        for (int i = 0; i < numStates - 1; i++)
        {
            Debug.Log($"Fusing State {i} with State {i + 1}...");
        }

        Debug.Log("Quantum fusion state created.");
    }
}
