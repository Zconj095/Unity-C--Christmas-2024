using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icosahedron : MonoBehaviour
{
    public GameObject vertexPrefab; // Prefab for vertices (small spheres or cubes)
    public float radius = 1f;       // Scale factor for the icosahedron

    void Start()
    {
        CreateIcosahedron();
    }

    void CreateIcosahedron()
    {
        // Generate the positions of the vertices
        List<Vector3> vertices = GenerateIcosahedronVertices();

        // Instantiate vertices at calculated positions
        foreach (var vertex in vertices)
        {
            Instantiate(vertexPrefab, transform.position + vertex * radius, Quaternion.identity, this.transform);
        }

        // Connect vertices using debug lines
        DrawEdges(vertices);
    }

    List<Vector3> GenerateIcosahedronVertices()
    {
        List<Vector3> vertices = new List<Vector3>();

        // Golden Ratio (phi)
        float phi = (1 + Mathf.Sqrt(5)) / 2;

        // Define the 12 vertices of the Icosahedron
        vertices.AddRange(new Vector3[]
        {
            new Vector3(-1,  phi,  0),
            new Vector3( 1,  phi,  0),
            new Vector3(-1, -phi,  0),
            new Vector3( 1, -phi,  0),

            new Vector3( 0, -1,  phi),
            new Vector3( 0,  1,  phi),
            new Vector3( 0, -1, -phi),
            new Vector3( 0,  1, -phi),

            new Vector3( phi,  0, -1),
            new Vector3( phi,  0,  1),
            new Vector3(-phi,  0, -1),
            new Vector3(-phi,  0,  1),
        });

        return vertices;
    }

    void DrawEdges(List<Vector3> vertices)
    {
        // Define edges of the Icosahedron using vertex indices
        int[,] edges = new int[,]
        {
            { 0, 1 }, { 0, 5 }, { 0, 7 }, { 0, 10 }, { 0, 11 },
            { 1, 5 }, { 1, 7 }, { 1, 8 }, { 1, 9 },
            { 2, 3 }, { 2, 4 }, { 2, 6 }, { 2, 10 }, { 2, 11 },
            { 3, 4 }, { 3, 6 }, { 3, 8 }, { 3, 9 },
            { 4, 5 }, { 4, 9 },
            { 5, 11 },
            { 6, 7 }, { 6, 10 },
            { 7, 8 },
            { 8, 9 },
            { 10, 11 }
        };

        // Draw edges
        for (int i = 0; i < edges.GetLength(0); i++)
        {
            Debug.DrawLine(transform.position + vertices[edges[i, 0]] * radius,
                           transform.position + vertices[edges[i, 1]] * radius, Color.magenta, 100f);
        }
    }
}
