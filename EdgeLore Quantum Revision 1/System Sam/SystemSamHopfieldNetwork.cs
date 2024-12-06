using UnityEngine;

public class SystemSamHopfieldNetwork : MonoBehaviour
{
    public Vector3[] Patterns; // Stored patterns
    public Vector3 InputPattern; // Input to the network

    public Vector3 Recall()
    {
        if (Patterns == null || Patterns.Length == 0)
        {
            Debug.LogError("Patterns array is null or empty. Cannot recall a pattern.");
            return Vector3.zero; // Default fallback value
        }

        Debug.Log("Recalling pattern using Hopfield Network...");
        Vector3 closestPattern = Patterns[0];
        float minDistance = float.MaxValue;

        foreach (var pattern in Patterns)
        {
            float distance = Vector3.Distance(InputPattern, pattern);
            if (distance < minDistance)
            {
                closestPattern = pattern;
                minDistance = distance;
            }
        }

        Debug.Log($"Recalled Pattern: {closestPattern}");
        return closestPattern;
    }
}