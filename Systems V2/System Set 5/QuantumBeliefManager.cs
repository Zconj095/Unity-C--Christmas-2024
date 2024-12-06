using UnityEngine;
public class QuantumBeliefManager : MonoBehaviour
{
    public BeliefNetwork network;
    public float learningRate = 0.01f;
    public float mutationRate = 0.1f;

    void Start()
    {
        network = new BeliefNetwork();
        network.InitializeNetwork(new int[] { 5, 10, 5 }); // Example: 5-input, 10-hidden, 5-output layers
    }

    void Update()
    {
        float[] inputs = { UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value }; // Example inputs
        float[] outputs = network.ForwardPropagation(inputs);

        // Apply Hebbian Learning
        network.HebbianLearning(inputs, learningRate);

        // Apply Evolution
        network.ApplyEvolution(mutationRate);

        // Log outputs
        Debug.Log("Outputs: " + string.Join(", ", outputs));
    }
}
