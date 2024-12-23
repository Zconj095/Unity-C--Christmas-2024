using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetatronsCube : MonoBehaviour
{
    public GameObject circlePrefab; // Prefab for circles
    public GameObject linePrefab;   // Prefab for lines (optional for rendering edges)
    public float radius = 1f;       // Radius of the circles

    private List<Vector3> circlePositions;

    void Start()
    {
        CreateMetatronsCube();
    }

    void CreateMetatronsCube()
    {
        // Generate the circle positions based on the Fruit of Life
        circlePositions = GenerateFruitOfLifePositions();

        // Instantiate the circles
        foreach (var position in circlePositions)
        {
            Instantiate(circlePrefab, transform.position + position, Quaternion.identity, this.transform);
        }

        // Draw lines connecting all circles
        DrawEdges();
    }

    List<Vector3> GenerateFruitOfLifePositions()
    {
        List<Vector3> positions = new List<Vector3>();

        float distance = radius * Mathf.Sqrt(3); // Distance between centers

        positions.Add(Vector3.zero); // Center

        // First ring
        positions.Add(new Vector3(-radius, 0, 0));          // Left
        positions.Add(new Vector3(radius, 0, 0));           // Right
        positions.Add(new Vector3(-radius / 2, distance / 2, 0));  // Top-left
        positions.Add(new Vector3(radius / 2, distance / 2, 0));   // Top-right
        positions.Add(new Vector3(-radius / 2, -distance / 2, 0)); // Bottom-left
        positions.Add(new Vector3(radius / 2, -distance / 2, 0));  // Bottom-right

        // Outer ring
        positions.Add(new Vector3(0, distance, 0));               // Top
        positions.Add(new Vector3(0, -distance, 0));              // Bottom
        positions.Add(new Vector3(-radius, distance, 0));         // Top-left outer
        positions.Add(new Vector3(radius, distance, 0));          // Top-right outer
        positions.Add(new Vector3(-radius, -distance, 0));        // Bottom-left outer
        positions.Add(new Vector3(radius, -distance, 0));         // Bottom-right outer

        return positions;
    }

    void DrawEdges()
    {
        // Connect each circle to every other circle
        for (int i = 0; i < circlePositions.Count; i++)
        {
            for (int j = i + 1; j < circlePositions.Count; j++)
            {
                Vector3 start = transform.position + circlePositions[i];
                Vector3 end = transform.position + circlePositions[j];

                Debug.DrawLine(start, end, Color.blue, 100f);

                // Optional: Instantiate line prefabs
                if (linePrefab != null)
                {
                    GameObject line = Instantiate(linePrefab, (start + end) / 2, Quaternion.identity, this.transform);
                    line.transform.LookAt(end);
                    line.transform.localScale = new Vector3(0.1f, 0.1f, Vector3.Distance(start, end));
                }
            }
        }
    }
}
