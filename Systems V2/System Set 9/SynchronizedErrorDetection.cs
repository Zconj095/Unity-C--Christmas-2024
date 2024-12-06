using System.Linq;
using UnityEngine; // Required for LINQ methods (e.g., Zip, Average, Sum, Select, Take)

public class SynchronizedErrorDetection : MonoBehaviour
{
    public static void DetectAndClassify(float[][] vectors, int historyWindow, float errorThreshold, float diffusionFactor, float learningRate)
    {
        foreach (var vector in vectors)
        {
            // Step 1: Predict and detect errors
            float[] predicted = VectorErrorDetector.PredictNextState(vector, historyWindow);
            float[] errors = VectorErrorDetector.CalculateError(predicted, vector);
            bool[] anomalies = VectorErrorDetector.DetectAnomalies(errors, errorThreshold);

            // Step 2: Diffuse errors
            float[] diffusedErrors = StableDiffusion.DiffuseErrors(errors, diffusionFactor);

            // Step 3: Adjust vectors
            float[] adjustedVector = Backpropagation.AdjustVectors(vector, diffusedErrors, learningRate);

            // Step 4: Classify errors
            string classification = ErrorClassifier.ClassifyError(errors);

            // Output results
            UnityEngine.Debug.Log($"Vector: {string.Join(", ", vector.Take(5))}...");
            UnityEngine.Debug.Log($"Errors: {string.Join(", ", errors.Take(5))}...");
            UnityEngine.Debug.Log($"Anomalies: {string.Join(", ", anomalies)}");
            UnityEngine.Debug.Log($"Classification: {classification}");
        }
    }
}
