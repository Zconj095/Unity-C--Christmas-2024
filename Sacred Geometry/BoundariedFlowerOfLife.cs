using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundariedFlowerOfLife : MonoBehaviour
{
    public GameObject circlePrefab; // Prefab for circles
    public float radius = 1f;       // Radius of each circle
    public int gridSize = 5;        // Size of the grid
    public float boundaryRadius = 5f; // Radius of the boundary circle

    void Start()
    {
        CreateBoundariedFlowerOfLife();
    }

    void CreateBoundariedFlowerOfLife()
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

                if (position.magnitude <= boundaryRadius)
                {
                    Instantiate(circlePrefab, transform.position + position, Quaternion.identity, this.transform);
                }
            }
        }
    }
}
