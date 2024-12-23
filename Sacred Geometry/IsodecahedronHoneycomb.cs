using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsodecahedronHoneycomb : MonoBehaviour
{
    public GameObject vertexPrefab; // Prefab for vertices (spheres or cubes)
    public GameObject edgePrefab;   // Optional prefab for edges
    public float radius = 1f;       // Size of each isodecahedron
    public int gridSize = 3;        // Number of isodecahedrons along each axis

    void Start()
    {
        CreateHoneycomb();
    }

    void CreateHoneycomb()
    {
        Vector3[] offsets = GenerateHoneycombOffsets();

        foreach (Vector3 offset in offsets)
        {
            CreateIsodecahedron(transform.position + offset * radius);
        }
    }

    void CreateIsodecahedron(Vector3 center)
    {
        // Generate the vertices of a single isodecahedron
        List<Vector3> vertices = GenerateIsodecahedronVertices(center);

        // Instantiate vertices
        foreach (var vertex in vertices)
        {
            Instantiate(vertexPrefab, vertex, Quaternion.identity, this.transform);
        }

        // Draw edges between vertices
        DrawEdges(vertices);
    }

    List<Vector3> GenerateIsodecahedronVertices(Vector3 center)
    {
        List<Vector3> vertices = new List<Vector3>();

        // Golden ratio (phi)
        float phi = (1 + Mathf.Sqrt(5)) / 2;

        // Define the vertices for the isodecahedron
        vertices.AddRange(new Vector3[]
        {
            center + new Vector3(0, 1, phi), center + new Vector3(0, -1, phi),
            center + new Vector3(0, 1, -phi), center + new Vector3(0, -1, -phi),
            center + new Vector3(1, phi, 0), center + new Vector3(-1, phi, 0),
            center + new Vector3(1, -phi, 0), center + new Vector3(-1, -phi, 0),
            center + new Vector3(phi, 0, 1), center + new Vector3(-phi, 0, 1),
            center + new Vector3(phi, 0, -1), center + new Vector3(-phi, 0, -1)
        });

        return vertices;
    }

    void DrawEdges(List<Vector3> vertices)
    {
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

        // Draw edges between vertices
        for (int i = 0; i < edges.GetLength(0); i++)
        {
            Vector3 start = vertices[edges[i, 0]];
            Vector3 end = vertices[edges[i, 1]];

            Debug.DrawLine(start, end, Color.green, 100f);

            // Optional: Instantiate edge prefab
            if (edgePrefab != null)
            {
                GameObject edge = Instantiate(edgePrefab, (start + end) / 2, Quaternion.identity, this.transform);
                edge.transform.localScale = new Vector3(0.1f, 0.1f, Vector3.Distance(start, end));
                edge.transform.LookAt(end);
            }
        }
    }

    Vector3[] GenerateHoneycombOffsets()
    {
        List<Vector3> offsets = new List<Vector3>();
        float offset = radius * Mathf.Sqrt(3);

        for (int x = -gridSize; x <= gridSize; x++)
        {
            for (int y = -gridSize; y <= gridSize; y++)
            {
                for (int z = -gridSize; z <= gridSize; z++)
                {
                    // Avoid overlapping cells in the lattice
                    if ((x + y + z) % 2 == 0)
                    {
                        offsets.Add(new Vector3(
                            x * radius * 1.5f,
                            y * offset,
                            z * offset
                        ));
                    }
                }
            }
        }

        return offsets.ToArray();
    }
}
