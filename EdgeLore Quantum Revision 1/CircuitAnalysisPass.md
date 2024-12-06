# CircuitAnalysisPass

## Overview
The `CircuitAnalysisPass` script is responsible for analyzing a quantum circuit represented by a list of gates. It provides functionalities to count the occurrences of different types of gates and calculate the depth of the circuit. This script is integrated within a Unity-based application, allowing developers to simulate and analyze quantum circuits visually and interactively. 

## Variables
- **quantumCircuit**: A private serialized list that stores instances of the `Gate` class, representing the gates in the quantum circuit.

## Functions
- **AnalyzeCircuit()**: This public method performs the analysis of the quantum circuit. It counts the number of each type of gate in the circuit and calculates the circuit's depth. The results are logged to the console for easy viewing.
  
- **CalculateCircuitDepth()**: This private method calculates the depth of the circuit. For simplicity, it currently assumes that each gate contributes a depth of 1, returning the total count of gates as the circuit depth.

- **AddGate(string type, List<string> qubits)**: This public method allows the addition of a new gate to the quantum circuit. It takes in the type of the gate and a list of qubits associated with that gate, creating a new `Gate` instance and adding it to the `quantumCircuit` list.