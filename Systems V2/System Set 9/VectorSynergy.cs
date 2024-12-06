using UnityEngine;
using System.Linq; // Required for LINQ methods (e.g., Zip, Average, Sum, Select, Take)

public class VectorSynergy : MonoBehaviour
{
    public static float CalculateSynergy(float[] v1, float[] v2)
    {
        // Cosine similarity to measure synergy between two vectors
        float dotProduct = v1.Zip(v2, (a, b) => a * b).Sum();
        float normV1 = Mathf.Sqrt(v1.Sum(x => x * x));
        float normV2 = Mathf.Sqrt(v2.Sum(x => x * x));
        return dotProduct / (normV1 * normV2);
    }

    
}
