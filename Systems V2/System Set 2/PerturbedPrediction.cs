using UnityEngine;

public class PerturbedPrediction : MonoBehaviour
{
    public int dimensions = 2;
    public float lambda = 0.1f;

    private float[] W;  // Weights
    private float[] X;  // Input
    private float epsilon = 0.01f;  // Perturbation noise

    void Start()
    {
        // Initialize weights and input
        W = InitializeWeights(dimensions);
        X = InitializeInput(dimensions);

        // Compute perturbed prediction
        float prediction = ComputePrediction(W, X);
        Debug.Log($"Perturbed Prediction: {prediction}");

        // Compute expected loss
        float y = 1.0f;  // Ground truth
        float expectedLoss = ComputeExpectedLoss(y, prediction, W, lambda);
        Debug.Log($"Expected Loss: {expectedLoss}");
    }

    private float[] InitializeWeights(int size)
    {
        float[] weights = new float[size];
        for (int i = 0; i < size; i++) weights[i] = Random.Range(-1.0f, 1.0f);
        return weights;
    }

    private float[] InitializeInput(int size)
    {
        float[] input = new float[size];
        for (int i = 0; i < size; i++) input[i] = Random.Range(-1.0f, 1.0f);
        return input;
    }

    private float ComputePrediction(float[] W, float[] X)
    {
        float dotProduct = 0.0f;
        for (int i = 0; i < W.Length; i++) dotProduct += W[i] * X[i];
        return dotProduct + Mathf.Sqrt(lambda) * Random.Range(-epsilon, epsilon);
    }

    private float ComputeExpectedLoss(float y, float prediction, float[] W, float lambda)
    {
        float squaredError = Mathf.Pow(y - prediction, 2);
        float regularization = lambda * DotProduct(W, W);
        return squaredError + regularization;
    }

    private float DotProduct(float[] a, float[] b)
    {
        float sum = 0.0f;
        for (int i = 0; i < a.Length; i++) sum += a[i] * b[i];
        return sum;
    }

    private float ComputeRegularizedLoss(float[] W, float lambda)
    {
        float regularization = 0.0f;
        foreach (float weight in W) regularization += Mathf.Abs(weight);
        return lambda * regularization;
    }

    private float[] Backpropagate(float[] activations, float[] weights, float delta)
    {
        float[] newDelta = new float[weights.Length];
        for (int i = 0; i < weights.Length; i++)
        {
            newDelta[i] = delta * weights[i] * activations[i];
        }
        return newDelta;
    }

    private float[] ComputeEnsembleProbabilities(float[][] modelPredictions)
    {
        int numModels = modelPredictions.Length;
        int numClasses = modelPredictions[0].Length;

        float[] combinedProbabilities = new float[numClasses];

        // Compute geometric mean
        for (int i = 0; i < numClasses; i++)
        {
            float product = 1.0f;
            for (int j = 0; j < numModels; j++) product *= modelPredictions[j][i];
            combinedProbabilities[i] = Mathf.Pow(product, 1.0f / numModels);
        }

        // Normalize probabilities
        float sum = 0.0f;
        foreach (float prob in combinedProbabilities) sum += prob;
        for (int i = 0; i < numClasses; i++) combinedProbabilities[i] /= sum;

        return combinedProbabilities;
    }


}
