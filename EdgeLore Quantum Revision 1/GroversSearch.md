# GroversSearch

## Overview
The `GroversSearch` class is an implementation of Grover's Search algorithm, which is a quantum algorithm designed to search an unsorted database with a quadratic speedup compared to classical algorithms. This script inherits from the `QuantumAlgorithm` base class and is intended to be executed within a quantum computing framework. The main function of this script is to prepare and execute the Grover's Search algorithm by applying the Hadamard gate to all qubits in the quantum circuit, setting the initial state for the algorithm.

## Variables
- **None**: This class does not define any member variables.

## Functions
- **GroversSearch()**: Constructor for the `GroversSearch` class. It initializes the base class `QuantumAlgorithm` with the name "Grover's Search".
  
- **Execute(QuantumCircuit circuit, QuantumSimulator simulator)**: This method overrides the `Execute` method from the `QuantumAlgorithm` base class. It performs the following actions:
  - Logs a message indicating that Grover's Search is being executed.
  - Applies the Hadamard gate to each qubit in the provided `QuantumCircuit` object to create a superposition of states.
  - Calls the `Simulate` method on the provided `QuantumSimulator` object to run the quantum circuit simulation.