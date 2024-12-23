using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra; // For matrix operations
using UnityEngine;

public class ArchitectonicCortex : MonoBehaviour
{
    // 3D Blueprint Data Structure
    private class Blueprint
    {
        public List<Vector<double>> Nodes; // Nodes represented as 3D vectors
        public List<(int, int)> Connections; // Connections between nodes (edges)

        public Blueprint()
        {
            Nodes = new List<Vector<double>>();
            Connections = new List<(int, int)>();
        }
    }

    private Blueprint _currentBlueprint; // Active holographic blueprint
    private GameObject _blueprintVisualizer; // GameObject for visualizing the blueprint

    // Initialize Blueprint
    private void InitializeBlueprint(int numNodes)
    {
        _currentBlueprint = new Blueprint();

        // Generate nodes dynamically
        for (int i = 0; i < numNodes; i++)
        {
            // Create random 3D positions for nodes
            var randomNode = Vector<double>.Build.DenseOfArray(new[]
            {
                (double)Random.Range(-5f, 5f), // Convert float to double
                (double)Random.Range(-5f, 5f),
                (double)Random.Range(-5f, 5f)
            });
            _currentBlueprint.Nodes.Add(randomNode);
        }

        // Create random connections
        for (int i = 0; i < numNodes - 1; i++)
        {
            _currentBlueprint.Connections.Add((i, i + 1));
        }
    }

    // Visualize the Blueprint
    private void VisualizeBlueprint()
    {
        // Clean up any existing visual elements
        if (_blueprintVisualizer != null)
        {
            Destroy(_blueprintVisualizer);
        }

        // Create a new parent object for visualization
        _blueprintVisualizer = new GameObject("BlueprintVisualizer");

        // Visualize nodes
        foreach (var node in _currentBlueprint.Nodes)
        {
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = new UnityEngine.Vector3((float)node[0], (float)node[1], (float)node[2]); // Specify UnityEngine.Vector3
            sphere.transform.localScale = UnityEngine.Vector3.one * 0.3f; // Specify UnityEngine.Vector3
            sphere.transform.parent = _blueprintVisualizer.transform;
        }

        // Visualize connections
        foreach (var connection in _currentBlueprint.Connections)
        {
            var startNode = _currentBlueprint.Nodes[connection.Item1];
            var endNode = _currentBlueprint.Nodes[connection.Item2];

            var line = new GameObject("Connection").AddComponent<LineRenderer>();
            line.startWidth = 0.1f;
            line.endWidth = 0.1f;
            line.positionCount = 2;
            line.SetPosition(0, new UnityEngine.Vector3((float)startNode[0], (float)startNode[1], (float)startNode[2])); // Specify UnityEngine.Vector3
            line.SetPosition(1, new UnityEngine.Vector3((float)endNode[0], (float)endNode[1], (float)endNode[2])); // Specify UnityEngine.Vector3
            line.transform.parent = _blueprintVisualizer.transform;
        }
    }

    // Dynamically Adjust the Blueprint
    private void AdjustBlueprint()
    {
        // Example adjustment: Apply a transformation matrix to all nodes
        var rotationMatrix = Matrix<double>.Build.DenseOfArray(new[,]
        {
            { 0.866, -0.5, 0 },
            { 0.5, 0.866, 0 },
            { 0, 0, 1 }
        }); // 30-degree rotation around Z-axis

        for (int i = 0; i < _currentBlueprint.Nodes.Count; i++)
        {
            _currentBlueprint.Nodes[i] = rotationMatrix * _currentBlueprint.Nodes[i];
        }
    }

    // Unity Lifecycle
    void Start()
    {
        // Initialize with 10 nodes
        InitializeBlueprint(10);

        // Visualize the initial blueprint
        VisualizeBlueprint();
    }

    void Update()
    {
        // Adjust and re-visualize the blueprint on key press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AdjustBlueprint();
            VisualizeBlueprint();
        }
    }
}
