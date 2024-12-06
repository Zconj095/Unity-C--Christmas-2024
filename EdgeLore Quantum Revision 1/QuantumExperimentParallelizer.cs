using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class QuantumExperimentParallelizer : MonoBehaviour
{
    [SerializeField] private int numExperiments = 10;

    public void RunExperiments(List<string[]> experimentConfigurations)
    {
        Parallel.For(0, experimentConfigurations.Count, i =>
        {
            ExecuteExperiment(experimentConfigurations[i], i);
        });

        Debug.Log("All experiments executed in parallel.");
    }

    private void ExecuteExperiment(string[] config, int experimentIndex)
    {
        Debug.Log($"Running Experiment {experimentIndex}");
        foreach (var step in config)
        {
            Debug.Log($"Step: {step}");
            // Add experiment logic here
        }
    }
}
