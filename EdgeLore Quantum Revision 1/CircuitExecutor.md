# CircuitExecutor

## Overview
The `CircuitExecutor` script is responsible for executing a series of quantum circuits within a Unity game environment. It serves as a central controller that initializes and runs various quantum algorithms when the game starts. This script is designed to work with multiple quantum circuit components, allowing for the execution of circuits such as Hadamard, Shor's algorithm, Grover's algorithm, Radon transform, and Pauli-Lindblad noise. By managing these circuits, it integrates quantum computation functionalities into the broader codebase.

## Variables

- `numQubits`: An integer that specifies the number of qubits to be used in the quantum circuits. This variable is serialized, allowing it to be set in the Unity Editor.

## Functions

- `Start()`: This is a Unity lifecycle method that is called before the first frame update. In this method, the `ExecuteAllCircuits()` function is invoked to initiate the execution of all defined quantum circuits.

- `ExecuteAllCircuits()`: This method is responsible for creating and executing various quantum circuits. It performs the following actions:
  - Creates an instance of the `HadamardCircuit` class and applies the Hadamard transformation to the specified number of qubits.
  - Creates an instance of the `ShorCircuit` class and executes Shor's algorithm to factorize the number 15.
  - Creates an instance of the `GroverCircuit` class and executes Grover's algorithm to find a specific element (in this case, element 2) among the qubits.
  - Creates an instance of the `RadonCircuit` class and applies the Radon transform with a given matrix and angle (π/4).
  - Creates an instance of the `PauliLindbladCircuit` class and applies the Pauli-Lindblad noise model with a specified vector.