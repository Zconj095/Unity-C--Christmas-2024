# QuantumVisualizer

## Overview
The `QuantumVisualizer` script is designed to create and visualize qubits and quantum gates within a Unity environment. It allows for the initialization of qubit objects, visual representation of quantum pulses and gates, and the simulation of noise effects on specific qubits. This script serves as a crucial component of a larger quantum simulation codebase, providing visual feedback and interaction capabilities for users exploring quantum concepts.

## Variables

- **QubitPrefab**: A `GameObject` representing the prefab used to instantiate qubit objects in the scene.
- **GatePrefab**: A `GameObject` representing the prefab used to instantiate quantum gate objects in the scene.
- **qubits**: An array of `GameObject` that holds references to the instantiated qubit objects.

## Functions

- **InitializeQubits(int numQubits)**: 
  - This method initializes the qubit array by creating a specified number of qubit instances. Each qubit is positioned along the x-axis, with a distance of 2 units between them. The method also names each qubit for easy identification.

- **VisualizePulse(double amplitude, double frequency, double duration)**: 
  - This method is intended to visualize a quantum pulse based on its amplitude, frequency, and duration. Currently, it logs the parameters to the Unity console, with a placeholder for future visualization logic.

- **VisualizeGate(QuantumGate gate)**: 
  - This method visualizes a quantum gate by calculating the average position of the qubits involved in the gate operation. It then instantiates a gate prefab at this calculated position.

- **ShowNoiseEffect(int qubitIndex)**: 
  - This method applies a visual noise effect to a specified qubit by changing its color to red. It first checks if the provided index is valid and, if so, retrieves the qubit's renderer component to change its material color, indicating the presence of noise.