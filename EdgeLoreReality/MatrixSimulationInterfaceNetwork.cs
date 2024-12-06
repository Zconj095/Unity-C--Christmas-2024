using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixSimulationInterfaceNetwork : MonoBehaviour
    {
        // Represents a grid node in the network
        private class GridNode
        {
            public Vector3 Position { get; private set; }
            public string NodeID { get; private set; }

            public GridNode(Vector3 position)
            {
                Position = position;
                NodeID = System.Guid.NewGuid().ToString();
            }
        }

        // Represents a network grid
        private class NetworkGrid
        {
            public string GridID { get; private set; }
            public List<GridNode> Nodes { get; private set; }

            public NetworkGrid()
            {
                GridID = System.Guid.NewGuid().ToString();
                Nodes = new List<GridNode>();
            }

            public void AddNode(Vector3 position)
            {
                GridNode node = new GridNode(position);
                Nodes.Add(node);
                Debug.Log($"Node Added - GridID: {GridID}, NodeID: {node.NodeID}, Position: {position}");
            }

            public void SynchronizeNodes(float timing)
            {
                Debug.Log($"Synchronizing Nodes in Grid '{GridID}' with timing: {timing}ms");
                foreach (var node in Nodes)
                {
                    Debug.Log($"Node '{node.NodeID}' synchronized at position {node.Position}");
                }
            }
        }

        // List of grids
        private List<NetworkGrid> networkGrids = new List<NetworkGrid>();

        // Simultaneous Timing Synchronization Driver
        private float timingSynchronization = 50f; // Default timing in milliseconds

        // Automated task timer
        private float taskTimer = 0f;
        private const float taskInterval = 5f; // Interval for automated tasks

        // Add a new grid to the network
        public void AddGrid()
        {
            NetworkGrid grid = new NetworkGrid();
            networkGrids.Add(grid);
            Debug.Log($"Grid Added - GridID: {grid.GridID}");
        }

        // Add a node to a specific grid
        public void AddNodeToGrid(int gridIndex, Vector3 position)
        {
            if (gridIndex >= 0 && gridIndex < networkGrids.Count)
            {
                networkGrids[gridIndex].AddNode(position);
            }
            else
            {
                Debug.LogWarning($"Grid index '{gridIndex}' is out of range.");
            }
        }

        // Synchronize all grids
        public void SynchronizeAllGrids()
        {
            Debug.Log($"Synchronizing all grids with timing: {timingSynchronization}ms");
            foreach (var grid in networkGrids)
            {
                grid.SynchronizeNodes(timingSynchronization);
            }
        }

        // Adjust timing synchronization
        public void AdjustTiming(float newTiming)
        {
            timingSynchronization = Mathf.Clamp(newTiming, 10f, 500f); // Timing range in milliseconds
            Debug.Log($"Timing Synchronization adjusted to: {timingSynchronization}ms");
        }

        // Display all grids and their nodes
        public void DisplayAllGrids()
        {
            Debug.Log("Displaying all grids and their nodes...");
            for (int i = 0; i < networkGrids.Count; i++)
            {
                var grid = networkGrids[i];
                Debug.Log($"Grid {i + 1} - ID: {grid.GridID}, Nodes Count: {grid.Nodes.Count}");
                foreach (var node in grid.Nodes)
                {
                    Debug.Log($"  Node - ID: {node.NodeID}, Position: {node.Position}");
                }
            }
        }

        private void Start()
        {
            Debug.Log("Automated Matrix Simulation Interface Network Initialized.");
        }

        private void Update()
        {
            // Increment the task timer
            taskTimer += Time.deltaTime;

            // Perform automated tasks at regular intervals
            if (taskTimer >= taskInterval)
            {
                PerformAutomatedTasks();
                taskTimer = 0f; // Reset the timer
            }
        }

        // Perform automated tasks on the network
        private void PerformAutomatedTasks()
        {
            Debug.Log("Performing automated network tasks...");

            // Automatically add a grid if there are no grids
            if (networkGrids.Count == 0)
            {
                AddGrid();
            }

            // Automatically add a node to the last grid
            AddNodeToGrid(networkGrids.Count - 1, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));

            // Randomly adjust timing synchronization
            AdjustTiming(Random.Range(10f, 500f));

            // Synchronize all grids
            SynchronizeAllGrids();

            // Display the state of all grids
            DisplayAllGrids();
        }
    }
}
