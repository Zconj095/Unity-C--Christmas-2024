# StateLogger

## Overview
The `StateLogger` class is a Unity component designed to log the evolution of a quantum state in a human-readable format. It provides a method to output the state vector of a `QuantumState` object to the Unity console, making it easier for developers to visualize and debug quantum state changes during the execution of a program. This class fits within a larger codebase that likely involves quantum computing simulations or visualizations, aiding in the analysis of quantum states.

## Variables
- **none**: This class does not contain any member variables.

## Functions
- **LogState(QuantumState state)**: 
  - This method takes a `QuantumState` object as a parameter and logs its state vector to the Unity console. It iterates through each element of the state vector, converting the index to a binary string representation, and displays the corresponding value in the state vector. The output format is designed to reflect the quantum state in a standard notation, aiding in understanding the current state of the quantum system.