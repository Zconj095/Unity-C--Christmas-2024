using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetrahedron : MonoBehaviour
{
    public GameObject vertexPrefab; // Prefab for vertices (small spheres or cubes)
    public float radius = 1f;       // Scale factor for the tetrahedron

    void Start()
    {
        CreateTetrahedron();
    }

    void CreateTetrahedron()
    {
        // Define vertex positions
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0, radius, 0),                            // Top vertex
            new Vector3(-radius * Mathf.Sqrt(3) / 2, -radius / 2, 0), // Bottom-left
            new Vector3(radius * Mathf.Sqrt(3) / 2, -radius / 2, 0),  // Bottom-right
            new Vector3(0, -radius / 2, radius * Mathf.Sqrt(2)),  // Back vertex
        };

        // Instantiate vertices
        foreach (var vertex in vertices)
        {
            Instantiate(vertexPrefab, transform.position + vertex, Quaternion.identity, this.transform);
        }

        // Connect vertices using debug lines
        DrawEdges(vertices);
    }

    void DrawEdges(Vector3[] vertices)
    {
        int[,] edges = new int[,]
        {
            { 0, 1 }, { 0, 2 }, { 0, 3 }, // Edges from the top vertex
            { 1, 2 }, { 2, 3 }, { 3, 1 }  // Base edges
        };

        for (int i = 0; i < edges.GetLength(0); i++)
        {
            Debug.DrawLine(transform.position + vertices[edges[i, 0]],
                           transform.position + vertices[edges[i, 1]], Color.green, 100f);
        }
    }
}
