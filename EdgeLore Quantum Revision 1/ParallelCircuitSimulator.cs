using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ParallelCircuitSimulator : MonoBehaviour
{
    [SerializeField] private int numCircuits = 3;

    public void SimulateCircuits(List<string[]> circuits)
    {
        Parallel.For(0, circuits.Count, i =>
        {
            SimulateSingleCircuit(circuits[i], i);
        });

        Debug.Log("All circuits simulated in parallel.");
    }

    private void SimulateSingleCircuit(string[] circuit, int circuitIndex)
    {
        Debug.Log($"Simulating Circuit {circuitIndex}");
        foreach (var gate in circuit)
        {
            Debug.Log($"Executing Gate: {gate}");
            // Add simulation logic here
        }
    }
}
