using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeOfLife : MonoBehaviour
{
    public GameObject circlePrefab; // Prefab for the spheres (Tree of Life nodes)
    public GameObject linePrefab;   // Optional prefab for the connecting lines
    public float radius = 1f;       // Distance between circles

    private List<Vector3> circlePositions;

    void Start()
    {
        CreateTreeOfLife();
    }

    void CreateTreeOfLife()
    {
        // Define the positions of the 10 spheres in the Tree of Life
        circlePositions = GenerateTreeOfLifePositions();

        // Instantiate the circles
        foreach (var position in circlePositions)
        {
            Instantiate(circlePrefab, transform.position + position * radius, Quaternion.identity, this.transform);
        }

        // Connect the circles with lines
        DrawEdges();
    }

    List<Vector3> GenerateTreeOfLifePositions()
    {
        List<Vector3> positions = new List<Vector3>();

        // Define positions for the Tree of Life nodes
        positions.Add(new Vector3(0, 4, 0)); // Top sphere
        positions.Add(new Vector3(0, 3, 0));
        positions.Add(new Vector3(-1, 2, 0));
        positions.Add(new Vector3(1, 2, 0));
        positions.Add(new Vector3(0, 1, 0));
        positions.Add(new Vector3(-1, 0, 0));
        positions.Add(new Vector3(1, 0, 0));
        positions.Add(new Vector3(0, -1, 0));
        positions.Add(new Vector3(-0.5f, -2, 0));
        positions.Add(new Vector3(0.5f, -2, 0)); // Bottom spheres

        return positions;
    }

    void DrawEdges()
    {
        // Define the connections between the spheres
        int[,] edges = new int[,]
        {
            { 0, 1 }, { 1, 2 }, { 1, 3 },
            { 2, 4 }, { 3, 4 }, { 4, 5 },
            { 4, 6 }, { 5, 7 }, { 6, 7 },
            { 7, 8 }, { 7, 9 }
        };

        // Draw lines between the spheres
        for (int i = 0; i < edges.GetLength(0); i++)
        {
            Vector3 start = transform.position + circlePositions[edges[i, 0]] * radius;
            Vector3 end = transform.position + circlePositions[edges[i, 1]] * radius;

            Debug.DrawLine(start, end, Color.green, 100f);

            // Optional: Instantiate a line prefab
            if (linePrefab != null)
            {
                GameObject line = Instantiate(linePrefab, (start + end) / 2, Quaternion.identity, this.transform);
                line.transform.LookAt(end);
                line.transform.localScale = new Vector3(0.1f, 0.1f, Vector3.Distance(start, end));
            }
        }
    }
}
