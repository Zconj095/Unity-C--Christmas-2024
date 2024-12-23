using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icosidodecahedron : MonoBehaviour
{
    public GameObject vertexPrefab; // Prefab for vertices (small spheres or cubes)
    public float radius = 1f;       // Scale factor for the icosidodecahedron

    void Start()
    {
        CreateIcosidodecahedron();
    }

    void CreateIcosidodecahedron()
    {
        // Generate the vertices of the icosidodecahedron
        List<Vector3> vertices = GenerateIcosidodecahedronVertices();

        // Instantiate the vertices
        foreach (var vertex in vertices)
        {
            Instantiate(vertexPrefab, transform.position + vertex * radius, Quaternion.identity, this.transform);
        }

        // Connect the vertices with edges
        DrawEdges(vertices);
    }

    List<Vector3> GenerateIcosidodecahedronVertices()
    {
        List<Vector3> vertices = new List<Vector3>();

        // Golden Ratio (phi)
        float phi = (1 + Mathf.Sqrt(5)) / 2;

        // Generate the vertices of the icosidodecahedron
        vertices.AddRange(new Vector3[]
        {
            // Vertices of the Icosahedron
            new Vector3(0, 1, phi), new Vector3(0, -1, phi),
            new Vector3(0, 1, -phi), new Vector3(0, -1, -phi),
            new Vector3(1, phi, 0), new Vector3(-1, phi, 0),
            new Vector3(1, -phi, 0), new Vector3(-1, -phi, 0),
            new Vector3(phi, 0, 1), new Vector3(-phi, 0, 1),
            new Vector3(phi, 0, -1), new Vector3(-phi, 0, -1),

            // Midpoints of the Dodecahedron edges
            new Vector3(0.5f, 0.5f * phi, 0.5f * phi),
            new Vector3(-0.5f, 0.5f * phi, 0.5f * phi),
            new Vector3(0.5f, -0.5f * phi, 0.5f * phi),
            new Vector3(-0.5f, -0.5f * phi, 0.5f * phi),
            new Vector3(0.5f, 0.5f * phi, -0.5f * phi),
            new Vector3(-0.5f, 0.5f * phi, -0.5f * phi),
            new Vector3(0.5f, -0.5f * phi, -0.5f * phi),
            new Vector3(-0.5f, -0.5f * phi, -0.5f * phi)
        });

        return vertices;
    }

    void DrawEdges(List<Vector3> vertices)
    {
        // Define the edges of the icosidodecahedron using vertex indices
        int[,] edges = new int[,]
        {
            { 0, 1 }, { 0, 2 }, { 0, 4 }, { 0, 8 }, { 0, 9 },
            { 1, 3 }, { 1, 6 }, { 1, 8 }, { 1, 9 },
            { 2, 3 }, { 2, 4 }, { 2, 10 }, { 2, 11 },
            { 3, 6 }, { 3, 10 }, { 3, 11 },
            { 4, 5 }, { 4, 8 }, { 4, 10 },
            { 5, 7 }, { 5, 8 }, { 5, 9 }, { 5, 11 },
            { 6, 7 }, { 6, 9 }, { 6, 10 },
            { 7, 9 }, { 7, 11 },
            { 8, 10 }, { 9, 11 }
        };

        // Draw lines between vertices
        for (int i = 0; i < edges.GetLength(0); i++)
        {
            Vector3 start = transform.position + vertices[edges[i, 0]] * radius;
            Vector3 end = transform.position + vertices[edges[i, 1]] * radius;

            Debug.DrawLine(start, end, Color.cyan, 100f);
        }
    }
}
