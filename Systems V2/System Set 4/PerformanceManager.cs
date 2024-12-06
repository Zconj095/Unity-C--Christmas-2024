using UnityEngine;

public class PerformanceManager : MonoBehaviour
{
    private QuantumHypervectorManager quantumManager;

    void Start()
    {
        quantumManager = FindObjectOfType<QuantumHypervectorManager>();
        InvokeRepeating("AdjustPerformance", 1f, 5f); // Check performance every 5 seconds
    }

    private void AdjustPerformance()
    {
        float currentFrameRate = 1.0f / Time.deltaTime;

        Debug.Log($"Current Frame Rate: {currentFrameRate}");

        if (currentFrameRate < 30f)
        {
            Debug.LogWarning("Frame rate is low, reducing active hypervectors.");
            ReduceActiveHypervectors();
        }
        else
        {
            Debug.Log("Frame rate is stable.");
        }
    }

    private void ReduceActiveHypervectors()
    {
        // Example: Reduce active quadrants
        foreach (var quadrant in quantumManager.QuadrantClassification)
        {
            int reductionCount = Mathf.Max(0, quadrant.Value.Count / 2);
            quadrant.Value.RemoveRange(0, reductionCount);
        }
    }
}
