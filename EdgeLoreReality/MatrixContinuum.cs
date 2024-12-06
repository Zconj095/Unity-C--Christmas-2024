using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixContinuum : MonoBehaviour
    {
        // Represents a node in the continuum
        private class ContinuumNode
        {
            public string ID { get; private set; }
            public string Domain { get; private set; }
            public string MemoryType { get; private set; } // Past, Present, Future
            public string Data { get; private set; }

            public ContinuumNode(string domain, string memoryType, string data)
            {
                ID = System.Guid.NewGuid().ToString();
                Domain = domain;
                MemoryType = memoryType;
                Data = data;
            }
        }

        // Stores all nodes in the continuum
        private List<ContinuumNode> continuumGrid = new List<ContinuumNode>();

        // Timers for automation
        private float addNodeTimer = 0f;
        private float retrieveNodesTimer = 0f;
        private float simulateFeedTimer = 0f;

        // Automation intervals
        private const float addNodeInterval = 5f; // Add a node every 5 seconds
        private const float retrieveNodesInterval = 7f; // Retrieve nodes every 7 seconds
        private const float simulateFeedInterval = 10f; // Simulate feed every 10 seconds

        // Add a node to the continuum
        public void AddNode(string domain, string memoryType, string data)
        {
            if (memoryType != "Past" && memoryType != "Present" && memoryType != "Future")
            {
                Debug.LogWarning("Invalid memory type. Must be 'Past', 'Present', or 'Future'.");
                return;
            }

            ContinuumNode node = new ContinuumNode(domain, memoryType, data);
            continuumGrid.Add(node);

            Debug.Log($"Node Added - ID: {node.ID}, Domain: {domain}, MemoryType: {memoryType}, Data: {data}");
        }

        // Retrieve all nodes of a specific memory type
        public void RetrieveNodes(string memoryType)
        {
            if (memoryType != "Past" && memoryType != "Present" && memoryType != "Future")
            {
                Debug.LogWarning("Invalid memory type. Must be 'Past', 'Present', or 'Future'.");
                return;
            }

            Debug.Log($"Retrieving nodes with MemoryType: {memoryType}");
            foreach (var node in continuumGrid)
            {
                if (node.MemoryType == memoryType)
                {
                    Debug.Log($"Node - ID: {node.ID}, Domain: {node.Domain}, Data: {node.Data}");
                }
            }
        }

        // Simulate spatial feed of nodes
        public void SimulateSpatialFeed()
        {
            Debug.Log("Simulating spatial feed of the continuum...");
            foreach (var node in continuumGrid)
            {
                Debug.Log($"Streaming Node - ID: {node.ID}, Domain: {node.Domain}, MemoryType: {node.MemoryType}, Data: {node.Data}");
            }
        }

        private void Start()
        {
            Debug.Log("Matrix Continuum Initialized.");
        }

        private void Update()
        {
            // Increment timers
            addNodeTimer += Time.deltaTime;
            retrieveNodesTimer += Time.deltaTime;
            simulateFeedTimer += Time.deltaTime;

            // Automate adding nodes
            if (addNodeTimer >= addNodeInterval)
            {
                string memoryType = Random.value > 0.66f ? "Past" : (Random.value > 0.33f ? "Present" : "Future");
                AddNode("Automated Domain", memoryType, $"Sample data for {memoryType}.");
                addNodeTimer = 0f;
            }

            // Automate retrieving nodes
            if (retrieveNodesTimer >= retrieveNodesInterval)
            {
                string memoryType = Random.value > 0.5f ? "Past" : "Future"; // Alternate between "Past" and "Future"
                RetrieveNodes(memoryType);
                retrieveNodesTimer = 0f;
            }

            // Automate spatial feed simulation
            if (simulateFeedTimer >= simulateFeedInterval)
            {
                SimulateSpatialFeed();
                simulateFeedTimer = 0f;
            }
        }
    }
}
