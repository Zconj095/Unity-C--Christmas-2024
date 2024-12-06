# QuantumState Script Documentation

## Overview
The `QuantumState` script is designed to represent a quantum state in a quantum computing simulation using Unity. It initializes a state vector based on the number of qubits specified and allows the application of quantum gates to modify the state vector. This script is essential for simulating quantum operations and understanding how quantum states evolve in response to these operations.

## Variables
- **StateVector**: An array of `Complex` numbers representing the quantum state. Each element corresponds to the amplitude of a specific state in the quantum system. The size of the array is determined by the number of qubits, specifically \(2^{\text{numQubits}}\).

## Functions
- **QuantumState(int numQubits)**: Constructor that initializes the quantum state. It takes the number of qubits as a parameter, calculates the size of the state vector, and sets the initial state to |0⟩ (represented by a complex number with a value of 1 at the first position of the state vector, and 0 elsewhere).

- **void ApplyGate(QuantumGate gate)**: This method applies a quantum gate operation to the current state vector. It checks if the gate has a valid operation defined; if so, it updates the `StateVector` by applying the operation. If the operation is not defined, it logs a warning message to the console.

- **override string ToString()**: This method overrides the default `ToString` method to provide a string representation of the quantum state. It formats the output to show each basis state in binary notation along with its corresponding amplitude in the state vector. This makes it easier to visualize the quantum state and understand the probabilities associated with each basis state.