# HadamardCircuit

## Overview
The `HadamardCircuit` script is designed to simulate the application of Hadamard gates to a specified number of qubits in a quantum computing context. It serves as a part of a broader codebase that likely focuses on quantum circuit simulation or quantum computing concepts within the Unity game engine. The main function of this script is to log the process of applying Hadamard gates, which are fundamental in creating superposition states in quantum mechanics.

## Variables
- **numQubits**: An integer that represents the number of qubits to which the Hadamard gates will be applied. This variable is passed as an argument to the `ApplyHadamard` function.

## Functions
- **ApplyHadamard(int numQubits)**: This public method takes an integer parameter, `numQubits`, and logs the process of applying Hadamard gates to each qubit. It iterates through the range of qubits, logging a message for each one indicating that the Hadamard gate has been applied. Finally, it logs a message stating that all qubits are now in superposition.