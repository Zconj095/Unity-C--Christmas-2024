# BlochSphereVisualizer

## Overview
The `BlochSphereVisualizer` script is designed to visualize the states of qubits using spheres in a 3D space, specifically within the Unity game engine. This script allows for the representation of qubit states on a Bloch sphere, a common model used in quantum mechanics to depict the state of a qubit. It provides two main functions for visualizing qubit states: `VisualizeState0`, which accepts an array of `Vector3` positions representing qubit states, and `VisualizeState1`, which takes a `QuantumState` object that contains a state vector for the qubits. This script integrates with other components of the codebase that handle quantum state representations and visualizations.

## Variables

- **SpherePrefab**: A `GameObject` reference to a prefab that represents the visual model of a Bloch sphere. It is used to instantiate spheres for each qubit in the visualization.
  
- **numQubits**: An `int` that defines the number of qubits to visualize. It determines how many qubit states will be represented in the visualization functions. This variable is serialized, allowing it to be set in the Unity editor.

## Functions

- **VisualizeState0(Vector3[] qubitStates)**: This function visualizes the states of qubits based on an array of `Vector3` positions. It checks for a mismatch between the number of qubits and the provided states. For each qubit, it creates a small sphere at the specified position, customizes its appearance (size and color), and logs the visualization details.

- **VisualizeState1(QuantumState state)**: This function visualizes the Bloch sphere for each qubit based on a given `QuantumState` object. It iterates over the state vector of the quantum state, instantiates a sphere prefab for each qubit, and spreads them along the X-axis. The color of each sphere is determined by the real and imaginary parts of the state vector, providing a visual representation of the quantum state.