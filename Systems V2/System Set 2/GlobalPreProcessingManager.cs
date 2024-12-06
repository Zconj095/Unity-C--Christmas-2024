using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a preprocessing matrix for unallocated data in the scene.
/// </summary>
public class GlobalPreProcessingManager : MonoBehaviour
{
    [System.Serializable]
    public class PreProcessingData
    {
        public GameObject entity;       // Reference to the entity
        public bool isPreAllocated;     // True if the entity is pre-allocated
        public float optimizationScore; // Normalized score for optimization
        public Vector2Int matrixIndex; // Index in the preprocessing matrix
    }

    public int matrixRows = 10;              // Number of rows in the matrix
    public int matrixColumns = 10;           // Number of columns in the matrix
    public float normalizationFactor = 100;  // Factor for normalizing scores

    private PreProcessingData[,] preprocessingMatrix; // Preprocessing matrix

    void Start()
    {
        InitializeMatrix();
        PopulateMatrix();
        NormalizeScores();
        OptimizePreProcessingData();
        PrintMatrix();
    }

    /// <summary>
    /// Initializes the preprocessing matrix with null values.
    /// </summary>
    void InitializeMatrix()
    {
        preprocessingMatrix = new PreProcessingData[matrixRows, matrixColumns];

        for (int i = 0; i < matrixRows; i++)
        {
            for (int j = 0; j < matrixColumns; j++)
            {
                preprocessingMatrix[i, j] = null;
            }
        }
    }

    /// <summary>
    /// Populates the preprocessing matrix with unallocated entities in the scene.
    /// </summary>
    void PopulateMatrix()
    {
        GameObject[] allEntities = FindObjectsOfType<GameObject>();

        foreach (GameObject entity in allEntities)
        {
            // Skip entities that are pre-allocated
            if (entity.GetComponent<PreAllocated>() != null)
                continue;

            // Create a new PreProcessingData for unallocated entities
            PreProcessingData data = new PreProcessingData
            {
                entity = entity,
                isPreAllocated = false,
                optimizationScore = Random.Range(0.0f, 1.0f), // Example raw score
                matrixIndex = GetAvailableIndex()
            };

            // Place the data in the matrix
            if (data.matrixIndex.x >= 0 && data.matrixIndex.y >= 0)
            {
                preprocessingMatrix[data.matrixIndex.x, data.matrixIndex.y] = data;
            }
        }
    }

    /// <summary>
    /// Finds the first available index in the preprocessing matrix.
    /// </summary>
    /// <returns>The available index as a Vector2Int or (-1, -1) if full.</returns>
    Vector2Int GetAvailableIndex()
    {
        for (int i = 0; i < matrixRows; i++)
        {
            for (int j = 0; j < matrixColumns; j++)
            {
                if (preprocessingMatrix[i, j] == null)
                    return new Vector2Int(i, j);
            }
        }
        return new Vector2Int(-1, -1); // No space available
    }

    /// <summary>
    /// Normalizes scores in the preprocessing matrix.
    /// </summary>
    void NormalizeScores()
    {
        float maxScore = 0f;

        // Find the maximum score
        foreach (var data in preprocessingMatrix)
        {
            if (data != null && data.optimizationScore > maxScore)
            {
                maxScore = data.optimizationScore;
            }
        }

        // Normalize scores
        foreach (var data in preprocessingMatrix)
        {
            if (data != null)
            {
                data.optimizationScore /= maxScore;
                data.optimizationScore *= normalizationFactor; // Scale to desired range
            }
        }
    }

    /// <summary>
    /// Optimizes preprocessing data based on custom criteria.
    /// </summary>
    void OptimizePreProcessingData()
    {
        foreach (var data in preprocessingMatrix)
        {
            if (data != null)
            {
                // Example optimization: Increase score if entity is close to the scene center
                Vector3 sceneCenter = Vector3.zero; // Replace with actual center if needed
                float distance = Vector3.Distance(data.entity.transform.position, sceneCenter);

                // Adjust score (closer entities get higher priority)
                data.optimizationScore += Mathf.Max(0, normalizationFactor / (distance + 1));
            }
        }
    }

    /// <summary>
    /// Prints the preprocessing matrix to the console.
    /// </summary>
    void PrintMatrix()
    {
        Debug.Log("Global Preprocessing Matrix:");
        for (int i = 0; i < matrixRows; i++)
        {
            string row = "";
            for (int j = 0; j < matrixColumns; j++)
            {
                if (preprocessingMatrix[i, j] != null)
                {
                    row += $"[{preprocessingMatrix[i, j].entity.name} ({preprocessingMatrix[i, j].optimizationScore:F2})] ";
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
