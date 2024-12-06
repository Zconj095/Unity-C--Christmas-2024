using UnityEngine;
using System.Collections.Generic;
public class AerSimulatorExample : MonoBehaviour
{
    private AerManager aerManager;

    void Start()
    {
        aerManager = gameObject.GetComponent<AerManager>();
        if (aerManager == null)
        {
            aerManager = gameObject.AddComponent<AerManager>();
        }

        // Define circuits
        List<string> circuit1 = new List<string> { "H 0", "CNOT 0 1" };
        List<string> circuit2 = new List<string> { "X 1", "H 0", "Z 1" };

        // Add jobs to the manager
        aerManager.AddJob("Job1", circuit1, 2);
        aerManager.AddJob("Job2", circuit2, 2);

        // Execute all jobs
        ExecuteAllJobs();
    }

    private async void ExecuteAllJobs()
    {
        await aerManager.ExecuteJobs();
    }
}
