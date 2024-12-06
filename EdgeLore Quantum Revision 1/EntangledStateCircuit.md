# EntangledStateCircuit

## Overview
The `EntangledStateCircuit` class is a Unity component responsible for creating an entangled quantum state with a specified number of qubits. It provides a method that simulates the process of applying quantum gates, specifically the Hadamard gate and controlled-X (CNOT) gates, to establish entanglement among the qubits. This functionality is essential for quantum computing simulations within the codebase, allowing for the exploration and demonstration of quantum entanglement concepts.

## Variables
- **None**: The class does not declare any instance variables. It relies solely on the method parameters for its functionality.

## Functions
- **CreateEntangledState(int numQubits)**: 
  - This public method takes an integer parameter `numQubits`, which specifies the number of qubits to be entangled. 
  - It logs the process of creating an entangled state, starting with the application of a Hadamard gate to the first qubit (Qubit 0) and then applying controlled-X gates (CNOT) between the first qubit and each of the subsequent qubits (from Qubit 1 to Qubit `numQubits - 1`).
  - The method concludes by logging that the entangled state has been created.