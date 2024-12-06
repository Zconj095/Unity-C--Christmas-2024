# QuantumHiddenMarkovModel

## Overview
The `QuantumHiddenMarkovModel` script is a Unity component that implements a Quantum Hidden Markov Model (HMM) designed to work in conjunction with Grover's Algorithm. The script initializes the HMM with specified configurations, runs Grover's Algorithm to adjust the model's probabilities, and provides methods for generating random observations and decoding the most likely state sequence using the Viterbi algorithm. This script is crucial for simulating quantum algorithms and probabilistic modeling within a Unity environment.

## Variables
- `numStates`: (int) The number of hidden states in the HMM.
- `numObservations`: (int) The number of possible observation symbols.
- `numQubits`: (int) The number of qubits utilized in Grover's Algorithm.
- `targetState`: (int) The target state for Grover's Algorithm, indexed from zero.
- `groverIterations`: (int) The number of iterations Grover's Algorithm will run.
- `transitionProbabilities`: (float[,]) A 2D array representing the transition probabilities between states.
- `emissionProbabilities`: (float[,]) A 2D array representing the emission probabilities from states to observations.
- `initialStateProbabilities`: (float[]) An array representing the initial probabilities of each state.
- `grover`: (GroversAlgorithm) A reference to the Grover's Algorithm component attached to the same GameObject.

## Functions
- `Awake()`: Initializes the script and attempts to auto-assign the Grover's Algorithm component. Logs an error if the component is not found.
  
- `Start()`: Initializes the HMM with random probabilities and runs Grover's Algorithm to adjust these probabilities.

- `InitializeHMM()`: Sets up the HMM parameters with normalized probabilities. Initializes transition and emission probabilities randomly and normalizes them.

- `NormalizeProbabilities(float[,] array, int row, int jMax)`: Normalizes the probabilities for a given row in a 2D array, ensuring that they sum to one. Logs warnings if the sum is zero and assigns uniform probabilities.

- `RunGroversAlgorithm()`: Executes Grover's Algorithm and adjusts the HMM probabilities based on the result. Logs the most probable state returned by Grover's Algorithm.

- `AdjustProbabilitiesBasedOnGrover(int mostProbableState)`: Modifies the transition and emission probabilities based on the most probable state determined by Grover's Algorithm. Normalizes probabilities after adjustment and logs the changes.

- `GenerateRandomObservations(int length)`: Generates a random sequence of observations of specified length. Returns an array of observations and logs each generated observation.

- `Viterbi(int[] observations)`: Implements the Viterbi Algorithm to decode the most likely state sequence for a given sequence of observations. Validates observations, initializes dynamic programming tables, performs recursion, and backtracking steps, and logs the most likely states.

- `FindMostProbableLastState(float[,] dp, int sequenceLength)`: Finds the most probable state at the last time step from the dynamic programming table.

- `GetRow(float[,] array, int row)`: A helper method that retrieves a specified row from a 2D float array.

- `GetArrayElement(float[,] array, int row, int col)`: Safely accesses an element in a 2D array with bounds checking, throwing an exception if indices are out of bounds.