using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexahedron : MonoBehaviour
{
    public GameObject vertexPrefab; // Prefab for vertices (small spheres or cubes)
    public float radius = 1f;       // Distance between the circles in the Flower of Life

    void Start()
    {
        CreateHexahedron();
    }

    void CreateHexahedron()
    {
        // Define vertex positions relative to the Flower of Life grid
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-radius, -radius, -radius),  // Bottom-left-front
            new Vector3(radius, -radius, -radius),   // Bottom-right-front
            new Vector3(radius, radius, -radius),    // Top-right-front
            new Vector3(-radius, radius, -radius),   // Top-left-front
            new Vector3(-radius, -radius, radius),   // Bottom-left-back
            new Vector3(radius, -radius, radius),    // Bottom-right-back
            new Vector3(radius, radius, radius),     // Top-right-back
            new Vector3(-radius, radius, radius),    // Top-left-back
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
            { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 0 }, // Front face
            { 4, 5 }, { 5, 6 }, { 6, 7 }, { 7, 4 }, // Back face
            { 0, 4 }, { 1, 5 }, { 2, 6 }, { 3, 7 }  // Connecting edges
        };

        for (int i = 0; i < edges.GetLength(0); i++)
        {
            Debug.DrawLine(transform.position + vertices[edges[i, 0]],
                           transform.position + vertices[edges[i, 1]], Color.red, 100f);
        }
    }
}
