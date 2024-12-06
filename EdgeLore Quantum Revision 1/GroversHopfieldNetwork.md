# GroversHopfieldNetwork

## Overview
The `GroversHopfieldNetwork` script implements a Hopfield neural network that utilizes Grover's search algorithm for pattern retrieval. This script is designed to be used within the Unity game engine and allows users to train the network with specific patterns and retrieve the most likely pattern based on an input. The network's structure and functionality enable efficient pattern recognition through quantum-inspired techniques, making it a valuable component in applications requiring associative memory.

## Variables
- `numNeurons`: An integer representing the number of neurons in the network.
- `maxIterations`: An integer that sets the maximum number of iterations allowed during the retrieval process.
- `logSteps`: A boolean that determines whether to log intermediate steps during the retrieval process.
- `trainingPatterns`: A list of integer arrays that holds the patterns to be stored in the network for training.
- `quantumIterations`: An integer that specifies the number of Grover's iterations to be performed during the search.
- `neuronPrefab`: A GameObject representing the prefab used for visualizing neurons in the network.
- `visualizationContainer`: A Transform that acts as a parent object for visualizing the neurons.
- `weightMatrix`: A 2D array of floats that represents the weight matrix of the Hopfield network.
- `currentPattern`: An integer array that holds the current input pattern to be retrieved.

## Functions
- `TrainNetwork()`: Initializes the weight matrix and trains the network using the provided training patterns based on the Hebbian learning rule. It logs an error message if no training patterns are provided.
  
- `RetrievePattern()`: Retrieves a pattern based on the current input pattern. It logs an error if the current pattern is not set or is invalid. This function initiates the pattern retrieval process and visualizes the result.

- `QuantumSearch(int[] inputPattern)`: Executes the quantum search process for pattern retrieval. It iteratively applies Grover's search and checks for convergence, logging the current state if `logSteps` is enabled.

- `ApplyGroversSearch(int[] state)`: Applies Grover's search algorithm to find the most probable pattern based on the current state. It calculates the probabilities of stored patterns and performs the diffusion operator to enhance the search.

- `CalculateOverlap(int[] state, int[] pattern)`: Computes the overlap between the current state and a given pattern, returning a float value that represents the degree of similarity.

- `IsConverged(int[] state)`: Checks if the current state matches any of the stored patterns, returning true if a match is found and false otherwise.

- `FindMaxIndex(float[] array)`: Finds and returns the index of the maximum value in a float array.

- `VisualizePattern(int[] pattern)`: Visualizes the retrieved pattern by instantiating neuron prefabs in the Unity scene. It adjusts the position of each neuron based on its index and colors them based on their state (1 or 0). It also clears any previous visualizations before rendering the new pattern.