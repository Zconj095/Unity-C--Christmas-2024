using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnboundariedFlowerOfLife : MonoBehaviour
{
    public GameObject circlePrefab; // Prefab for circles
    public float radius = 1f;       // Radius of each circle
    public int gridSize = 5;        // Size of the grid (number of circles in each direction)

    void Start()
    {
        CreateUnboundariedFlowerOfLife();
    }

    void CreateUnboundariedFlowerOfLife()
    {
        float offset = radius * Mathf.Sqrt(3);

        for (int x = -gridSize; x <= gridSize; x++)
        {
            for (int y = -gridSize; y <= gridSize; y++)
            {
                Vector3 position = new Vector3(
                    x * radius * 1.5f,
                    y * offset + (x % 2 == 0 ? 0 : offset / 2),
                    0
                );

                Instantiate(circlePrefab, transform.position + position, Quaternion.identity, this.transform);
            }
        }
    }
}
