using System.Linq;
using UnityEngine; // Required for LINQ methods (e.g., Zip, Average, Sum, Select, Take)

public class Backpropagation : MonoBehaviour
{
    public static float[] AdjustVectors(float[] vector, float[] errors, float learningRate)
    {
        // Adjust vectors based on errors
        return vector.Zip(errors, (v, e) => v - learningRate * e).ToArray();
    }
}