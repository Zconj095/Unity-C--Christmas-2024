using System.Linq;
using UnityEngine; // Required for LINQ methods (e.g., Zip, Average, Sum, Select, Take)

public class StableDiffusion : MonoBehaviour
{
    public static float[] DiffuseErrors(float[] errors, float diffusionFactor)
    {
        // Normalize errors to ensure they are distributed stably
        float errorSum = errors.Sum();
        return errors.Select(e => e / errorSum * diffusionFactor).ToArray();
    }
}
