using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixSimulationAnalyzer : MonoBehaviour
    {
        // Represents a data packet from the simulation
        private class SimulationData
        {
            public string DataID { get; private set; }
            public string Content { get; private set; }

            public SimulationData(string content)
            {
                DataID = System.Guid.NewGuid().ToString();
                Content = content;
            }
        }

        // List to store collected data
        private List<SimulationData> collectedData = new List<SimulationData>();

        // Timer for automated data collection
        private float dataCollectionTimer = 0f;
        private const float dataCollectionInterval = 3f; // Collect new data every 3 seconds

        // Add new data from the simulation
        public void CollectSimulationData(string content)
        {
            SimulationData data = new SimulationData(content);
            collectedData.Add(data);

            Debug.Log($"Simulation Data Collected - ID: {data.DataID}, Content: {content}");
            ProcessData(data);
        }

        // Process the data and send it to the Matrix Cortex Mainframe
        private void ProcessData(SimulationData data)
        {
            Debug.Log($"Processing Data - ID: {data.DataID}, Content: {data.Content}");
            SendToMainframe(data);
        }

        // Send processed data to the mainframe
        private void SendToMainframe(SimulationData data)
        {
            Debug.Log($"Sending Data to Matrix Cortex Mainframe - ID: {data.DataID}, Content: {data.Content}");
            StoreData(data);
        }

        // Store data for refreshable usage
        private void StoreData(SimulationData data)
        {
            Debug.Log($"Data Stored in Mainframe - ID: {data.DataID}, Content: {data.Content}");
        }

        // Retrieve all collected data
        public void RetrieveCollectedData()
        {
            Debug.Log("Retrieving all collected simulation data...");
            foreach (var data in collectedData)
            {
                Debug.Log($"Data - ID: {data.DataID}, Content: {data.Content}");
            }

            if (collectedData.Count == 0)
            {
                Debug.Log("No simulation data collected.");
            }
        }

        private void Start()
        {
            Debug.Log("Matrix Simulation Analyzer Initialized.");
        }

        private void Update()
        {
            // Increment timer
            dataCollectionTimer += Time.deltaTime;

            // Automate data collection
            if (dataCollectionTimer >= dataCollectionInterval)
            {
                CollectSimulationData($"Simulation Data Input at {Time.time:F2}s.");
                dataCollectionTimer = 0f; // Reset timer
            }
        }
    }
}
