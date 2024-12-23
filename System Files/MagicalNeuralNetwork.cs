using System;
using MathNet.Numerics.LinearAlgebra; // For matrix operations
using MathNet.Numerics.Distributions; // For random distributions
using Accord.Math; // For activation functions
using UnityEngine;
public class MagicalNeuralNetwork : MonoBehaviour
{
    // Magical Neural Network Layer
    public class Layer
    {
        public Matrix<double> Weights; // Weight matrix W[l]
        public Vector<double> Biases;  // Bias vector b[l]
        public Func<Vector<double>, Vector<double>> ActivationFunction; // Activation function σ

        // Constructor for the layer
        public Layer(int inputSize, int outputSize, Func<Vector<double>, Vector<double>> activationFunction)
        {
            // Use a uniform distribution to initialize weights and biases
            var uniformDistribution = new ContinuousUniform(-1.0, 1.0);

            // Initialize weights and biases
            Weights = Matrix<double>.Build.Random(outputSize, inputSize, uniformDistribution);
            Biases = Vector<double>.Build.Random(outputSize, uniformDistribution);
            ActivationFunction = activationFunction;
        }

        // Forward pass for this layer
        public Vector<double> Forward(Vector<double> input)
        {
            // z[l] = W[l] * a[l-1] + b[l]
            var z = Weights * input + Biases;

            // a[l] = σ(z[l])
            return ActivationFunction(z);
        }
    }

    // Neural Network Model
    private Layer[] layers;

    public MagicalNeuralNetwork(int[] layerSizes, Func<Vector<double>, Vector<double>> activationFunction)
    {
        layers = new Layer[layerSizes.Length - 1];
        for (int i = 0; i < layers.Length; i++)
        {
            layers[i] = new Layer(layerSizes[i], layerSizes[i + 1], activationFunction);
        }
    }

    // Forward pass through the entire network
    public Vector<double> Forward(Vector<double> input)
    {
        Vector<double> activations = input;
        foreach (var layer in layers)
        {
            activations = layer.Forward(activations);
        }
        return activations;
    }

    // Test the network
    [UnityEngine.ContextMenu("Run Magical Neural Network")]
    public static void TestMagicalNeuralNetwork()
    {
        // Define the activation function σ (ReLU enhanced by magical energy)
        Func<Vector<double>, Vector<double>> magicalActivation = (z) =>
        {
            // ReLU with magical scaling
            return z.Map(value => Math.Max(0, value * 1.5)); // Scale activations by 1.5
        };

        // Create a network with 3 layers: input(3) -> hidden(4) -> output(2)
        int[] layerSizes = { 3, 4, 2 };
        var network = new MagicalNeuralNetwork(layerSizes, magicalActivation);

        // Define an example input vector a[0]
        var input = Vector<double>.Build.DenseOfArray(new double[] { 0.5, -1.2, 0.3 });

        // Perform a forward pass
        var output = network.Forward(input);

        // Log the output
        UnityEngine.Debug.Log($"Input: {input}");
        UnityEngine.Debug.Log($"Output: {output}");
    }
}
