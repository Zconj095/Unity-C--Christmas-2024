using System;
using UnityEngine;

public class StateLogger : MonoBehaviour
{
    public void LogState(QuantumState state)
    {
        Debug.Log("Quantum State Evolution:");
        for (int i = 0; i < state.StateVector.Length; i++)
        {
            // Ensure Convert is accessible for binary conversion
            Debug.Log($"|{Convert.ToString(i, 2)}⟩: {state.StateVector[i]}");
        }
    }
}
