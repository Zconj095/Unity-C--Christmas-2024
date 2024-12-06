using UnityEngine;

public class BigDataHandler : MonoBehaviour
{
    private QuantumHypervectorManager quantumManager;

    void Start()
    {
        quantumManager = FindObjectOfType<QuantumHypervectorManager>();
        ActivateFoliage();
    }

    private void ActivateFoliage()
    {
        foreach (var quadrant in quantumManager.QuadrantClassification)
        {
            Debug.Log($"Activating Quadrant {quadrant.Key} with {quadrant.Value.Count} vectors.");
        }

        transform.localScale = Vector3.one * Random.Range(0.5f, 1.5f); // Example scaling
    }
}
