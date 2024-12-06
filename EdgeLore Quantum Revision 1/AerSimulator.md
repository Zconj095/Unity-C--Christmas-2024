# AerSimulator

## Overview
The `AerSimulator` class is designed to simulate a quantum circuit by generating random probabilities for quantum states based on the number of qubits specified. This class fits into a larger codebase that likely involves quantum computing simulations, providing a simplified method to visualize the behavior of quantum circuits through state probabilities.

## Variables
- **gates**: A `List<string>` that represents the quantum gates to be applied in the simulation. While the current implementation does not utilize this variable directly, it is intended to represent the operations that would affect the quantum states.
- **numQubits**: An `int` that specifies the number of qubits in the quantum circuit. This determines the number of possible states that can be generated and simulated.
- **stateProbabilities**: A `Dictionary<string, float>` that holds the binary representation of each quantum state as keys and their corresponding probabilities as values.
- **numStates**: An `int` calculated as \(2^{\text{numQubits}}\), representing the total number of quantum states based on the number of qubits.
- **total**: A `float` used to store the sum of all generated probabilities, which is necessary for normalizing the probabilities.

## Functions
- **SimulateCircuit(List<string> gates, int numQubits)**: This function simulates a quantum circuit by generating random probabilities for each possible quantum state based on the number of qubits. It:
  - Logs the start of the simulation.
  - Creates a dictionary to store the probabilities of each quantum state.
  - Loops through all possible states, generating a random probability for each and storing it in the dictionary.
  - Normalizes the probabilities so that they sum to 1, ensuring they represent valid probabilities.
  - Logs the completion of the simulation and returns the dictionary of state probabilities.