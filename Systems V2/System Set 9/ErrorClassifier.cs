using System.Linq;
using UnityEngine; // Required for LINQ methods (e.g., Zip, Average, Sum, Select, Take)

public class ErrorClassifier : MonoBehaviour
{
    public static string ClassifyError(float[] errors)
    {
        // Example classification: Low, Medium, High error
        float meanError = errors.Average();
        if (meanError < 0.1f) return "Low";
        if (meanError < 0.5f) return "Medium";
        return "High";
    }
}
