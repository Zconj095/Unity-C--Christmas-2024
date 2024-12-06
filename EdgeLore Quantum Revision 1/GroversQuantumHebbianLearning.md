# GroversQuantumHebbianLearning

## Overview
The `GroversQuantumHebbianLearning` script is designed to implement a Hebbian learning model combined with Grover's search algorithm within a Unity environment. The primary function of this script is to train a network of neurons based on provided training patterns and retrieve patterns using quantum-like search techniques. This script fits into a larger codebase that may involve neural network simulations or quantum computing applications, providing an interface for training and retrieving patterns based on Hebbian learning principles.

## Variables
- `numNeurons`: An integer representing the number of neurons in the network.
- `trainingPatterns`: A list of integer arrays that store the patterns used for training the neural network.
- `learningRate`: A float that determines the rate at which the network learns during the training phase.
- `maxIterations`: An integer that sets the maximum number of iterations for pattern retrieval.
- `quantumIterations`: An integer that specifies how many iterations Grover's search algorithm will perform.
- `weightMatrix`: A 2D float array that holds the Hebbian weight values between neurons.
- `currentState`: An integer array representing the current state of the network, which is used during the pattern retrieval process.

## Functions
- `TrainNetwork()`: This method initializes the weight matrix and updates it based on the provided training patterns using Hebbian learning rules. It logs a message once the learning process is complete.

- `RetrievePattern()`: This method retrieves a pattern based on the current state of the network. It checks if the current state is valid and then calls `ApplyGroversSearch()` to find and log the most probable training pattern.

- `ApplyGroversSearch(int[] state)`: This private method implements Grover's search algorithm. It calculates the probabilities of each training pattern based on their overlap with the current state and iteratively updates these probabilities to enhance the likelihood of retrieving the most relevant pattern.

- `CalculateOverlap(int[] state, int[] pattern)`: This private method computes the overlap between the current state and a given training pattern, returning a float value that represents the degree of similarity.

- `FindMaxIndex(float[] array)`: This private method finds and returns the index of the maximum value in a given float array, which is useful for determining the most probable pattern during the retrieval process.