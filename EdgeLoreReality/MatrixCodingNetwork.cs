using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixCodingNetwork : MonoBehaviour
    {
        // Represents a coding data packet
        private class CodingData
        {
            public string ID { get; private set; }
            public string SourcePath { get; private set; }
            public string DestinationPath { get; private set; }
            public float EnergyStrength { get; private set; }
            public float CodeRate { get; private set; }

            public CodingData(string sourcePath, string destinationPath, float energyStrength, float codeRate)
            {
                ID = System.Guid.NewGuid().ToString();
                SourcePath = sourcePath;
                DestinationPath = destinationPath;
                EnergyStrength = energyStrength;
                CodeRate = codeRate;
            }
        }

        // Path definitions and active coding data
        private Dictionary<string, List<CodingData>> codingPaths = new Dictionary<string, List<CodingData>>();
        private Dictionary<string, CodingData> activeCodings = new Dictionary<string, CodingData>();

        // Timers for automation
        private float addCodingDataTimer = 0f;
        private float processCodingDataTimer = 0f;
        private float manipulateQuantumPathsTimer = 0f;

        // Automation intervals
        private const float addCodingDataInterval = 5f; // Add data every 5 seconds
        private const float processCodingDataInterval = 7f; // Process data every 7 seconds
        private const float manipulateQuantumPathsInterval = 10f; // Manipulate paths every 10 seconds

        // Initialize network paths
        private void InitializePaths()
        {
            codingPaths["PathA"] = new List<CodingData>();
            codingPaths["PathB"] = new List<CodingData>();
            codingPaths["PathC"] = new List<CodingData>();

            Debug.Log("Matrix Coding Network Initialized with Paths: PathA, PathB, PathC.");
        }

        // Add coding data to a path
        public void AddCodingData(string sourcePath, string destinationPath, float energyStrength, float codeRate)
        {
            if (!codingPaths.ContainsKey(sourcePath) || !codingPaths.ContainsKey(destinationPath))
            {
                Debug.LogWarning("Invalid source or destination path.");
                return;
            }

            var data = new CodingData(sourcePath, destinationPath, energyStrength, codeRate);
            codingPaths[sourcePath].Add(data);
            activeCodings[data.ID] = data;

            Debug.Log($"Added Coding Data - ID: {data.ID}, Source: {sourcePath}, Destination: {destinationPath}, Energy: {energyStrength}, Rate: {codeRate}");
        }

        // Adapt coding paths to avoid collisions
        private void AdaptCodingPaths()
        {
            Debug.Log("Adapting coding paths to avoid collisions...");

            foreach (var source in codingPaths)
            {
                for (int i = 0; i < source.Value.Count; i++)
                {
                    var data = source.Value[i];
                    string newPath = CalculateNewPath(data);

                    if (newPath != data.DestinationPath)
                    {
                        source.Value.Remove(data);
                        data = new CodingData(data.SourcePath, newPath, data.EnergyStrength, data.CodeRate);
                        codingPaths[newPath].Add(data);
                        activeCodings[data.ID] = data;

                        Debug.Log($"Adapted Coding Data - ID: {data.ID}, New Destination Path: {newPath}");
                    }
                }
            }
        }

        // Calculate a new path dynamically
        private string CalculateNewPath(CodingData data)
        {
            if (data.EnergyStrength > 50f)
                return "PathB";
            else if (data.CodeRate > 5f)
                return "PathC";
            return "PathA";
        }

        // Process and analyze active coding data
        public void ProcessCodingData()
        {
            Debug.Log("Processing all active coding data...");
            foreach (var data in activeCodings.Values)
            {
                Debug.Log($"Processing Coding - ID: {data.ID}, Energy: {data.EnergyStrength}, Rate: {data.CodeRate}");
            }
        }

        // Quantum path manipulation
        public void ManipulateQuantumPaths()
        {
            Debug.Log("Performing quantum path manipulation...");
            AdaptCodingPaths();
        }

        private void Start()
        {
            InitializePaths();
        }

        private void Update()
        {
            // Increment timers
            addCodingDataTimer += Time.deltaTime;
            processCodingDataTimer += Time.deltaTime;
            manipulateQuantumPathsTimer += Time.deltaTime;

            // Automate adding coding data
            if (addCodingDataTimer >= addCodingDataInterval)
            {
                AddCodingData("PathA", "PathC", Random.Range(10f, 100f), Random.Range(1f, 10f));
                addCodingDataTimer = 0f;
            }

            // Automate processing coding data
            if (processCodingDataTimer >= processCodingDataInterval)
            {
                ProcessCodingData();
                processCodingDataTimer = 0f;
            }

            // Automate quantum path manipulation
            if (manipulateQuantumPathsTimer >= manipulateQuantumPathsInterval)
            {
                ManipulateQuantumPaths();
                manipulateQuantumPathsTimer = 0f;
            }
        }
    }
}
