using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodecahedron : MonoBehaviour
{
    public GameObject vertexPrefab; // Prefab for vertices (small spheres or cubes)
    public float radius = 1f;       // Scale factor for the dodecahedron

    void Start()
    {
        CreateDodecahedron();
    }

    void CreateDodecahedron()
    {
        // Generate positions of the 20 vertices
        List<Vector3> vertices = GenerateDodecahedronVertices();

        // Instantiate vertices at calculated positions
        foreach (var vertex in vertices)
        {
            Instantiate(vertexPrefab, transform.position + vertex * radius, Quaternion.identity, this.transform);
        }

        // Connect vertices using debug lines
        DrawEdges(vertices);
    }

    List<Vector3> GenerateDodecahedronVertices()
    {
        List<Vector3> vertices = new List<Vector3>();

        // Golden Ratio (phi)
        float phi = (1 + Mathf.Sqrt(5)) / 2f;

        // Create 20 vertices of the Dodecahedron
        vertices.AddRange(new Vector3[]
        {
            new Vector3(-1, -1, -1),
            new Vector3(-1, -1,  1),
            new Vector3(-1,  1, -1),
            new Vector3(-1,  1,  1),
            new Vector3( 1, -1, -1),
            new Vector3( 1, -1,  1),
            new Vector3( 1,  1, -1),
            new Vector3( 1,  1,  1),
            new Vector3( 0, -1 / phi, -phi),
            new Vector3( 0, -1 / phi,  phi),
            new Vector3( 0,  1 / phi, -phi),
            new Vector3( 0,  1 / phi,  phi),
            new Vector3(-1 / phi, -phi,  0),
            new Vector3(-1 / phi,  phi,  0),
            new Vector3( 1 / phi, -phi,  0),
            new Vector3( 1 / phi,  phi,  0),
            new Vector3(-phi,  0, -1 / phi),
            new Vector3(-phi,  0,  1 / phi),
            new Vector3( phi,  0, -1 / phi),
            new Vector3( phi,  0,  1 / phi),
        });

        return vertices;
    }

    void DrawEdges(List<Vector3> vertices)
    {
        // Define the edges of the dodecahedron by connecting vertex indices
        int[,] edges = new int[,]
        {
            { 0, 1 }, { 0, 2 }, { 0, 4 }, { 0, 8 }, { 0, 10 },
            { 1, 3 }, { 1, 5 }, { 1, 9 }, { 1, 11 },
            { 2, 3 }, { 2, 6 }, { 2, 10 }, { 2, 12 },
            { 3, 7 }, { 3, 11 }, { 3, 13 },
            { 4, 5 }, { 4, 6 }, { 4, 8 }, { 4, 14 },
            { 5, 7 }, { 5, 9 }, { 5, 15 },
            { 6, 7 }, { 6, 12 }, { 6, 14 },
            { 7, 13 }, { 7, 15 },
            { 8, 9 }, { 8, 14 },
            { 9, 15 },
            { 10, 11 }, { 10, 12 },
            { 11, 13 },
            { 12, 13 }, { 12, 14 },
            { 13, 15 },
            { 14, 15 },
        };

        // Draw edges between vertices
        for (int i = 0; i < edges.GetLength(0); i++)
        {
            Vector3 start = transform.position + vertices[edges[i, 0]] * radius;
            Vector3 end = transform.position + vertices[edges[i, 1]] * radius;

            Debug.DrawLine(start, end, Color.blue, 100f);
        }
    }
}
