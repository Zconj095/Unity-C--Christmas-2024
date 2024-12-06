using System.Collections.Generic;
using UnityEngine;
using System;

public class AerSimulator : MonoBehaviour
{
    public Dictionary<string, float> SimulateCircuit(List<string> gates, int numQubits)
    {
        Debug.Log("Simulating quantum circuit...");

        // Simplified simulation logic: Generate random probabilities for states
        Dictionary<string, float> stateProbabilities = new Dictionary<string, float>();
        int numStates = (int)Mathf.Pow(2, numQubits);

        for (int i = 0; i < numStates; i++)
        {
            string state = Convert.ToString(i, 2).PadLeft(numQubits, '0'); // Binary state representation
            stateProbabilities[state] = UnityEngine.Random.Range(0.0f, 1.0f);
        }

        // Normalize probabilities
        float total = 0;
        foreach (var prob in stateProbabilities.Values) total += prob;
        foreach (var key in stateProbabilities.Keys)
        {
            stateProbabilities[key] /= total;
        }

        Debug.Log("Quantum circuit simulation completed.");
        return stateProbabilities;
    }
}
