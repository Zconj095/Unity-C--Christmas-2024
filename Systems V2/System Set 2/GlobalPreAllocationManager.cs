using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages global pre-allocation of resources for entities in the scene.
/// </summary>
public class GlobalPreAllocationManager : MonoBehaviour
{
    [System.Serializable]
    public class AllocationData
    {
        public GameObject entity;       // Reference to the entity
        public bool isPreAllocated;     // True if the entity is pre-allocated
        public float allocationScore;  // Score for optimization
        public Vector2Int matrixIndex; // Index in the allocation matrix
    }

    public int matrixRows = 10;              // Rows in the allocation matrix
    public int matrixColumns = 10;           // Columns in the allocation matrix
    public float defaultAllocationScore = 1; // Default score for unallocated entities

    private AllocationData[,] allocationMatrix; // 2D allocation matrix

    void Start()
    {
        InitializeMatrix();
        PopulateMatrix();
        OptimizeAllocations();
        PrintMatrix();
    }

    /// <summary>
    /// Initializes the allocation matrix with null values.
    /// </summary>
    void InitializeMatrix()
    {
        allocationMatrix = new AllocationData[matrixRows, matrixColumns];

        for (int i = 0; i < matrixRows; i++)
        {
            for (int j = 0; j < matrixColumns; j++)
            {
                allocationMatrix[i, j] = null; // Initialize matrix with null
            }
        }
    }

    /// <summary>
    /// Populates the allocation matrix with unallocated entities in the scene.
    /// </summary>
    void PopulateMatrix()
    {
        GameObject[] allEntities = FindObjectsOfType<GameObject>();

        foreach (GameObject entity in allEntities)
        {
            // Skip entities already pre-allocated
            if (entity.GetComponent<PreAllocated>() != null)
                continue;

            // Create a new AllocationData for unallocated entities
            AllocationData data = new AllocationData
            {
                entity = entity,
                isPreAllocated = false,
                allocationScore = defaultAllocationScore,
                matrixIndex = GetAvailableIndex()
            };

            // Place in the matrix if there's space
            if (data.matrixIndex.x >= 0 && data.matrixIndex.y >= 0)
            {
                allocationMatrix[data.matrixIndex.x, data.matrixIndex.y] = data;
            }
        }
    }

    /// <summary>
    /// Finds the first available index in the allocation matrix.
    /// </summary>
    /// <returns>The available index as a Vector2Int or (-1, -1) if full.</returns>
    Vector2Int GetAvailableIndex()
    {
        for (int i = 0; i < matrixRows; i++)
        {
            for (int j = 0; j < matrixColumns; j++)
            {
                if (allocationMatrix[i, j] == null)
                    return new Vector2Int(i, j);
            }
        }
        return new Vector2Int(-1, -1); // Return invalid index if no space is available
    }

    /// <summary>
    /// Optimizes the allocation scores for all unallocated entities.
    /// </summary>
    void OptimizeAllocations()
    {
        foreach (var data in allocationMatrix)
        {
            if (data != null)
            {
                // Example optimization: Adjust score based on distance from the scene's center
                Vector3 sceneCenter = Vector3.zero; // Replace with actual center if needed
                float distance = Vector3.Distance(data.entity.transform.position, sceneCenter);

                // Higher score for entities closer to the center
                data.allocationScore = Mathf.Max(1.0f, 100.0f / (distance + 1.0f));
            }
        }
    }

    /// <summary>
    /// Prints the allocation matrix to the console for debugging.
    /// </summary>
    void PrintMatrix()
    {
        Debug.Log("Global Pre-Allocation Matrix:");
        for (int i = 0; i < matrixRows; i++)
        {
            string row = "";
            for (int j = 0; j < matrixColumns; j++)
            {
                if (allocationMatrix[i, j] != null)
                {
                    row += $"[{allocationMatrix[i, j].entity.name} ({allocationMatrix[i, j].allocationScore:F2})] ";
                }
                else
                {
                    row += "[EMPTY] ";
                }
            }
            Debug.Log(row);
        }
    }
}
