using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitOfLife : MonoBehaviour
{
    public GameObject circlePrefab; // Prefab for a circle
    public float radius = 1f;       // Radius of each circle

    void Start()
    {
        CreateFruitOfLife();
    }

    void CreateFruitOfLife()
    {
        float distance = radius * Mathf.Sqrt(3); // Distance between circle centers
        Vector3[] offsets = new Vector3[]
        {
            Vector3.zero,                                           // Center
            new Vector3(-radius, 0, 0),                             // Left
            new Vector3(radius, 0, 0),                              // Right
            new Vector3(-radius / 2, distance / 2, 0),              // Top-left
            new Vector3(radius / 2, distance / 2, 0),               // Top-right
            new Vector3(-radius / 2, -distance / 2, 0),             // Bottom-left
            new Vector3(radius / 2, -distance / 2, 0),              // Bottom-right
            new Vector3(0, distance, 0),                           // Top
            new Vector3(0, -distance, 0),                          // Bottom
            new Vector3(-radius, distance, 0),                     // Top-left outer
            new Vector3(radius, distance, 0),                      // Top-right outer
            new Vector3(-radius, -distance, 0),                    // Bottom-left outer
            new Vector3(radius, -distance, 0),                     // Bottom-right outer
        };

        foreach (var offset in offsets)
        {
            Instantiate(circlePrefab, transform.position + offset, Quaternion.identity, this.transform);
        }
    }
}
