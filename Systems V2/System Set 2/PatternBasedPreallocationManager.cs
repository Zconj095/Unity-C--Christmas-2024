using System.Collections.Generic;
using UnityEngine;
public enum AllocationPattern
{
    Grid,       // Allocations follow a grid pattern
    Circular,   // Allocations are distributed in a circular pattern
    Radial,     // Allocations radiate outward from a central point
    Random      // Allocations are randomized
}



/// <summary>
/// Manages pattern-based preallocations for entities in a Unity scene.
/// </summary>
public class PatternBasedPreallocationManager : MonoBehaviour
{
    [System.Serializable]
    public class AllocationData
    {
        public GameObject entity;   // Entity being allocated
        public Vector3 allocatedPosition; // Position allocated to the entity
    }

    public AllocationPattern allocationPattern = AllocationPattern.Grid; // Selected pattern
    public Vector3 centerPoint = Vector3.zero;   // Center for Circular/Radial patterns
    public Vector2 gridSpacing = new Vector2(2, 2); // Spacing for Grid pattern
    public float circleRadius = 5f;              // Radius for Circular pattern
    public int radialSegments = 8;              // Number of segments for Radial pattern
    public float randomAreaSize = 10f;          // Area size for Random pattern

    private List<AllocationData> allocations = new List<AllocationData>();

    void Start()
    {
        PerformAllocations();
        ApplyAllocations();
    }

    /// <summary>
    /// Performs the allocation based on the selected pattern.
    /// </summary>
    void PerformAllocations()
    {
        GameObject[] allEntities = FindObjectsOfType<GameObject>();

        foreach (GameObject entity in allEntities)
        {
            // Skip pre-allocated entities
            if (entity.GetComponent<PreAllocated>() != null)
                continue;

            // Create allocation data
            AllocationData data = new AllocationData
            {
                entity = entity,
                allocatedPosition = GetPositionForPattern(allEntities.Length, allocations.Count)
            };

            allocations.Add(data);
        }
    }

    /// <summary>
    /// Determines the position for the entity based on the selected pattern.
    /// </summary>
    /// <param name="totalEntities">Total number of entities to allocate.</param>
    /// <param name="index">Index of the current entity.</param>
    /// <returns>The allocated position for the entity.</returns>
    Vector3 GetPositionForPattern(int totalEntities, int index)
    {
        switch (allocationPattern)
        {
            case AllocationPattern.Grid:
                int row = index / Mathf.CeilToInt(Mathf.Sqrt(totalEntities));
                int column = index % Mathf.CeilToInt(Mathf.Sqrt(totalEntities));
                return new Vector3(centerPoint.x + column * gridSpacing.x, centerPoint.y, centerPoint.z + row * gridSpacing.y);

            case AllocationPattern.Circular:
                float angle = 2 * Mathf.PI * index / totalEntities;
                return new Vector3(centerPoint.x + Mathf.Cos(angle) * circleRadius, centerPoint.y, centerPoint.z + Mathf.Sin(angle) * circleRadius);

            case AllocationPattern.Radial:
                int segment = index % radialSegments;
                float radiusStep = circleRadius * (index / (float)radialSegments);
                float radialAngle = 2 * Mathf.PI * segment / radialSegments;
                return new Vector3(centerPoint.x + Mathf.Cos(radialAngle) * radiusStep, centerPoint.y, centerPoint.z + Mathf.Sin(radialAngle) * radiusStep);

            case AllocationPattern.Random:
                return new Vector3(centerPoint.x + Random.Range(-randomAreaSize, randomAreaSize), centerPoint.y, centerPoint.z + Random.Range(-randomAreaSize, randomAreaSize));

            default:
                return centerPoint;
        }
    }

    /// <summary>
    /// Applies the allocated positions to the entities.
    /// </summary>
    void ApplyAllocations()
    {
        foreach (var allocation in allocations)
        {
            allocation.entity.transform.position = allocation.allocatedPosition;
        }
    }

    /// <summary>
    /// Visualizes the allocation positions in the scene (for debugging).
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        foreach (var allocation in allocations)
        {
            Gizmos.DrawSphere(allocation.allocatedPosition, 0.2f);
        }
    }
}
