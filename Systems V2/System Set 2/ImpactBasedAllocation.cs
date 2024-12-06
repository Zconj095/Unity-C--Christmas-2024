using System.Collections.Generic;
using UnityEngine;

public class ImpactBasedAllocation : MonoBehaviour
{
    [System.Serializable]
    public class Area
    {
        public string name;
        public float impactScore;  // Raw impact score
        public float normalizedScore; // Normalized score (calculated)
        public float allocatedResources; // Final allocation (calculated)
    }

    public List<Area> areas = new List<Area>();
    public float totalResources = 100.0f; // Total resources to distribute
    public float fineTuneFactor = 0.1f;   // Adjustment factor for fine-tuning

    void Start()
    {
        InitializeAreas();
        NormalizeImpactScores();
        AllocateResources();
        FineTuneAllocations();
        PrintAllocations();
    }

    void InitializeAreas()
    {
        // Example initialization (you can replace this with dynamic data)
        areas.Add(new Area { name = "Area 1", impactScore = 10 });
        areas.Add(new Area { name = "Area 2", impactScore = 20 });
        areas.Add(new Area { name = "Area 3", impactScore = 15 });
    }

    void NormalizeImpactScores()
    {
        float totalImpact = 0f;

        // Calculate the total impact
        foreach (var area in areas)
        {
            totalImpact += area.impactScore;
        }

        // Normalize scores
        foreach (var area in areas)
        {
            area.normalizedScore = area.impactScore / totalImpact;
        }
    }

    void AllocateResources()
    {
        // Allocate resources based on normalized scores
        foreach (var area in areas)
        {
            area.allocatedResources = area.normalizedScore * totalResources;
        }
    }

    void FineTuneAllocations()
    {
        // Example fine-tuning logic: Adjust resources based on external factors
        foreach (var area in areas)
        {
            float adjustment = Random.Range(-fineTuneFactor, fineTuneFactor);
            area.allocatedResources += area.allocatedResources * adjustment;

            // Ensure allocation stays within bounds
            area.allocatedResources = Mathf.Clamp(area.allocatedResources, 0, totalResources);
        }
    }

    void PrintAllocations()
    {
        Debug.Log("Final Allocations:");
        foreach (var area in areas)
        {
            Debug.Log($"Area: {area.name}, Resources: {area.allocatedResources:F2}, Normalized Score: {area.normalizedScore:F2}");
        }
    }
}
