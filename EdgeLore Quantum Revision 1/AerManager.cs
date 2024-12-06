using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AerManager : MonoBehaviour
{
    private AerSimulator simulator;
    private List<AerJob> jobQueue = new List<AerJob>();

    void Start()
    {
        simulator = gameObject.GetComponent<AerSimulator>();
        if (simulator == null)
        {
            simulator = gameObject.AddComponent<AerSimulator>();
        }

        Debug.Log("AerSimulator initialized.");
    }



    public void AddJob(string jobId, List<string> circuit, int numQubits)
    {
        AerJob job = new AerJob(jobId, circuit, numQubits);
        jobQueue.Add(job);
        Debug.Log($"Added job {jobId} to the queue.");
    }

    public async Task ExecuteJobs()
    {
        foreach (AerJob job in jobQueue)
        {
            await job.Execute(simulator);
            DisplayJobResult(job);
        }

        jobQueue.Clear();
    }

    private void DisplayJobResult(AerJob job)
    {
        if (job.Result == null)
        {
            Debug.LogError($"Job {job.JobId} returned null results.");
            return;
        }

        Debug.Log($"Results for Job {job.JobId}:");
        foreach (var kvp in job.Result)
        {
            Debug.Log($"State: {kvp.Key}, Probability: {kvp.Value}");
        }
    }

}
