# QuantumCircuit

## Overview
The `QuantumCircuit` class represents a quantum circuit, which is a model used in quantum computing to perform operations on qubits. This class manages a collection of quantum gates and the number of qubits in the circuit. It provides methods to add qubits and quantum gates, allowing for the dynamic construction of a quantum circuit. This class fits within a larger codebase focused on simulating quantum computing concepts and operations.

## Variables
- **Gates**: A list that stores instances of `QuantumGate`, representing the quantum gates that are part of the circuit.
- **NumQubits**: An integer that tracks the number of qubits present in the quantum circuit.

## Functions
- **QuantumCircuit(int numQubits)**: Constructor that initializes a new instance of the `QuantumCircuit` class with a specified number of qubits and an empty list of gates.
  
- **void AddQubit()**: Increases the number of qubits in the circuit by one. This allows the circuit to expand dynamically as needed.

- **void AddGate(QuantumGate gate)**: Adds a specified quantum gate to the list of gates in the circuit. This method enables the construction of the circuit by incorporating various quantum operations.

- **override string ToString()**: Returns a string representation of the quantum circuit, including the number of qubits and a list of all the gates in the circuit. This method is useful for debugging and visualizing the current state of the circuit.