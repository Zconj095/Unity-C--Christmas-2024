# GroversAlgorithm

## Overview
The `GroversAlgorithm` class implements Grover's search algorithm within a Unity environment. Its primary function is to search for a specific target state among a set of quantum states represented by qubits. The algorithm iteratively enhances the probability of the target state being selected by applying an oracle and a diffusion operator. This class also provides visualization capabilities to display the probabilities of each state during the algorithm's execution. 

## Variables
- `numQubits`: (int) The number of qubits used in the algorithm, influencing the total number of states (2^numQubits).
- `targetState`: (int) The specific state that the algorithm is searching for.
- `iterations`: (int) The number of iterations the algorithm will perform to enhance the target state's probability.
- `logIntermediateStates`: (bool) A flag to determine whether to log the probabilities of states before and after each iteration.
- `enableVisualization`: (bool) A flag to toggle the visualization of probabilities during the algorithm's execution.
- `probabilityBarPrefab`: (GameObject) The prefab used for visualizing the probability bars.
- `visualizationContainer`: (Transform) The parent object in which probability bars will be instantiated.
- `probabilities`: (List<float>) A list that holds the probabilities of each quantum state.
- `probabilityBars`: (List<GameObject>) A list that holds the instantiated GameObjects representing the probability bars.

## Functions
- `RunGrover()`: Executes Grover's algorithm. It initializes quantum states, applies the oracle and diffusion operator iteratively, logs intermediate probabilities, and returns the most probable state as a `Vector3` representation.

- `ApplyOracle()`: Modifies the amplitude of the target state by flipping its value in the probabilities list.

- `ApplyDiffusionOperator()`: Calculates the mean amplitude of all states and reflects each state's amplitude about this mean, enhancing the probabilities of the target state.

- `GetMostProbableState()`: Identifies and returns the index of the state with the highest probability.

- `LogProbabilities(string message)`: Logs the current probabilities of each state to the console, along with a provided message.

- `InitializeVisualization(int totalStates)`: Sets up the visualization components by creating probability bars for each quantum state and ensuring they match the number of states.

- `UpdateVisualization()`: Updates the scale of the probability bars to reflect the current probabilities of each state, ensuring they visually represent the algorithm's state.