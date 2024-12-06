using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AerJob : MonoBehaviour
{
    public string JobId { get; private set; }
    public List<string> Circuit { get; private set; }
    public int NumQubits { get; private set; }
    public Dictionary<string, float> Result { get; private set; }

    public AerJob(string jobId, List<string> circuit, int numQubits)
    {
        JobId = jobId;
        Circuit = circuit;
        NumQubits = numQubits;
    }

    public async Task Execute(AerSimulator simulator)
    {
        if (simulator == null)
        {
            Debug.LogError("AerSimulator instance is null. Ensure it is initialized before executing the job.");
            Result = null; // Explicitly set Result to null for error handling
            return;
        }

        Debug.Log($"Executing job {JobId}...");
        Result = await Task.Run(() => simulator.SimulateCircuit(Circuit, NumQubits));
        Debug.Log($"Job {JobId} completed.");
    }


    private void DisplayJobResult(AerJob job)
    {
        if (job.Result == null)
        {
            Debug.LogError($"Job {job.JobId} failed or returned null results.");
            return;
        }

        Debug.Log($"Results for Job {job.JobId}:");
        foreach (var kvp in job.Result)
        {
            Debug.Log($"State: {kvp.Key}, Probability: {kvp.Value}");
        }
    }


}
