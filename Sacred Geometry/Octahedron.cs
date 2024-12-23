using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octahedron : MonoBehaviour
{
    public GameObject vertexPrefab; // Prefab for vertices (small spheres or cubes)
    public float radius = 1f;       // Scale factor for the Octahedron

    void Start()
    {
        CreateOctahedron();
    }

    void CreateOctahedron()
    {
        // Define positions for the six vertices of the octahedron
        List<Vector3> vertices = GenerateOctahedronVertices();

        // Instantiate vertices at calculated positions
        foreach (var vertex in vertices)
        {
            Instantiate(vertexPrefab, transform.position + vertex * radius, Quaternion.identity, this.transform);
        }

        // Connect vertices with edges
        DrawEdges(vertices);
    }

    List<Vector3> GenerateOctahedronVertices()
    {
        List<Vector3> vertices = new List<Vector3>();

        // Define the vertices of the Octahedron
        vertices.Add(new Vector3(0, 1, 0));   // Top vertex
        vertices.Add(new Vector3(0, -1, 0));  // Bottom vertex
        vertices.Add(new Vector3(1, 0, 0));   // Right vertex
        vertices.Add(new Vector3(-1, 0, 0));  // Left vertex
        vertices.Add(new Vector3(0, 0, 1));   // Front vertex
        vertices.Add(new Vector3(0, 0, -1));  // Back vertex

        return vertices;
    }

    void DrawEdges(List<Vector3> vertices)
    {
        // Define the edges of the Octahedron using vertex indices
        int[,] edges = new int[,]
        {
            { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, // Top to all other vertices
            { 1, 2 }, { 1, 3 }, { 1, 4 }, { 1, 5 }, // Bottom to all other vertices
            { 2, 4 }, { 4, 3 }, { 3, 5 }, { 5, 2 }  // Edges between the base vertices
        };

        // Draw edges between vertices
        for (int i = 0; i < edges.GetLength(0); i++)
        {
            Vector3 start = transform.position + vertices[edges[i, 0]] * radius;
            Vector3 end = transform.position + vertices[edges[i, 1]] * radius;

            Debug.DrawLine(start, end, Color.cyan, 100f);
        }
    }
}
