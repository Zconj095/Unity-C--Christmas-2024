using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwelvePointedStarTetrahedron : MonoBehaviour
{
    public GameObject vertexPrefab; // Prefab for vertices (small spheres or cubes)
    public float radius = 1f;       // Scale factor for the structure

    void Start()
    {
        CreateStarTetrahedron();
    }

    void CreateStarTetrahedron()
    {
        // Generate the positions of the vertices
        List<Vector3> vertices = GenerateStarVertices();

        // Instantiate vertices at calculated positions
        foreach (var vertex in vertices)
        {
            Instantiate(vertexPrefab, transform.position + vertex * radius, Quaternion.identity, this.transform);
        }

        // Connect vertices using debug lines
        DrawEdges(vertices);
    }

    List<Vector3> GenerateStarVertices()
    {
        List<Vector3> vertices = new List<Vector3>();

        // Twelve points arranged for a star tetrahedron structure
        float factor = Mathf.Sqrt(2f) / 2;
        vertices.AddRange(new Vector3[]
        {
            new Vector3(1, 1, 1), new Vector3(-1, 1, 1), new Vector3(1, -1, 1), new Vector3(-1, -1, 1),
            new Vector3(1, 1, -1), new Vector3(-1, 1, -1), new Vector3(1, -1, -1), new Vector3(-1, -1, -1),

            // Secondary vertices for the star
            new Vector3(0, factor, -factor), new Vector3(0, -factor, factor),
            new Vector3(factor, 0, factor), new Vector3(-factor, 0, -factor)
        });

        return vertices;
    }

    void DrawEdges(List<Vector3> vertices)
    {
        // Connect vertices to form the star
        for (int i = 0; i < vertices.Count; i++)
        {
            for (int j = i + 1; j < vertices.Count; j++)
            {
                Debug.DrawLine(transform.position + vertices[i] * radius,
                               transform.position + vertices[j] * radius, Color.yellow, 100f);
            }
        }
    }
}
