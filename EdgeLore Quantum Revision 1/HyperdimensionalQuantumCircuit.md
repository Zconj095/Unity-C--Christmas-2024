# HyperdimensionalQuantumCircuit

## Overview
The `HyperdimensionalQuantumCircuit` class represents a simplified model of a quantum circuit within a Unity game environment. It allows for the application of quantum gates to a specified number of qubits, which are fundamental units of quantum information. This class is essential for simulating quantum operations and provides functionality to log the gates applied in the circuit. It integrates into the broader codebase by enabling quantum circuit simulations, which can be useful for educational purposes, game mechanics, or research simulations.

## Variables
- `numQubits` (int): This variable specifies the number of qubits present in the quantum circuit. It determines the capacity of the circuit to perform quantum operations.
- `gates` (List<string>): This list stores the descriptions of the quantum gates that have been applied to the circuit. Each entry in the list represents a gate operation along with the target qubits.

## Functions
- `ApplyGate(string gate, int[] targetQubits)`: This function accepts a quantum gate (as a string) and an array of integers representing the target qubits. It formats a string that indicates which gate was applied to which qubits, adds this operation to the `gates` list, and logs the operation to the console for debugging purposes.

- `PrintCircuit()`: This function outputs the current state of the quantum circuit to the console. It logs a header followed by each gate operation that has been recorded in the `gates` list, providing a comprehensive view of the circuit's history.