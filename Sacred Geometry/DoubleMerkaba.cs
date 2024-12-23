using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleMerkaba : MonoBehaviour
{
    public GameObject vertexPrefab; // Prefab for vertices (small spheres or cubes)
    public float radius = 1f;       // Scale factor for the Double Merkaba

    void Start()
    {
        CreateDoubleMerkaba();
    }

    void CreateDoubleMerkaba()
    {
        // Generate the positions of the vertices
        List<Vector3> vertices = GenerateMerkabaVertices();

        // Instantiate vertices
        foreach (var vertex in vertices)
        {
            Instantiate(vertexPrefab, transform.position + vertex * radius, Quaternion.identity, this.transform);
        }

        // Connect vertices to form the structure
        DrawEdges(vertices);
    }

    List<Vector3> GenerateMerkabaVertices()
    {
        List<Vector3> vertices = new List<Vector3>();

        // Primary Star Tetrahedron
        vertices.Add(new Vector3(0, 1, 0));                     // Top
        vertices.Add(new Vector3(-Mathf.Sqrt(3) / 2, -0.5f, 0));  // Bottom-left
        vertices.Add(new Vector3(Mathf.Sqrt(3) / 2, -0.5f, 0));   // Bottom-right
        vertices.Add(new Vector3(0, -0.5f, Mathf.Sqrt(2)));     // Front

        // Inverted Star Tetrahedron
        vertices.Add(new Vector3(0, -1, 0));                   // Bottom
        vertices.Add(new Vector3(-Mathf.Sqrt(3) / 2, 0.5f, 0));   // Top-left
        vertices.Add(new Vector3(Mathf.Sqrt(3) / 2, 0.5f, 0));    // Top-right
        vertices.Add(new Vector3(0, 0.5f, -Mathf.Sqrt(2)));    // Back

        // Add second inverted star for the "Double" Merkaba
        vertices.AddRange(new List<Vector3>
        {
            new Vector3(0, 2, 0),
            new Vector3(-Mathf.Sqrt(3) / 2, 1.5f, 0),
            new Vector3(Mathf.Sqrt(3) / 2, 1.5f, 0),
            new Vector3(0, 1.5f, Mathf.Sqrt(2)),

            new Vector3(0, -2, 0),
            new Vector3(-Mathf.Sqrt(3) / 2, -1.5f, 0),
            new Vector3(Mathf.Sqrt(3) / 2, -1.5f, 0),
            new Vector3(0, -1.5f, -Mathf.Sqrt(2))
        });

        return vertices;
    }

    void DrawEdges(List<Vector3> vertices)
    {
        int[,] edges = new int[,]
        {
            // Primary Star Tetrahedron
            { 0, 1 }, { 0, 2 }, { 0, 3 }, { 1, 2 }, { 2, 3 }, { 3, 1 },

            // Inverted Star Tetrahedron
            { 4, 5 }, { 4, 6 }, { 4, 7 }, { 5, 6 }, { 6, 7 }, { 7, 5 },

            // Connections between both
            { 0, 4 }, { 1, 5 }, { 2, 6 }, { 3, 7 }
        };

        for (int i = 0; i < edges.GetLength(0); i++)
        {
            Vector3 start = transform.position + vertices[edges[i, 0]] * radius;
            Vector3 end = transform.position + vertices[edges[i, 1]] * radius;

            Debug.DrawLine(start, end, Color.blue, 100f);
        }
    }
}
