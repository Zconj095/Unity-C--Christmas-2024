# SmoothBlochSphere

## Overview
The `SmoothBlochSphere` script is designed to manage and visualize quantum states represented by qubits in a 3D environment using Unity. It creates a series of spherical representations (Bloch spheres) for each qubit and updates their visual state based on the quantum state information provided. This script fits within a larger codebase that likely deals with quantum computing simulations or visualizations, allowing users to observe changes in quantum states dynamically.

## Variables
- `SpherePrefab`: A public `GameObject` that serves as the template for creating individual qubit spheres. It is expected to be assigned in the Unity editor.
- `qubitSpheres`: A private `Dictionary<int, GameObject>` that maps an integer index (representing the qubit number) to its corresponding `GameObject`. This allows for easy access and manipulation of each qubit's visual representation.

## Functions
- `InitializeQubits(int numQubits)`: This method initializes the specified number of qubits by creating a Bloch sphere for each. It positions each sphere in a line along the x-axis, assigns a unique name to each sphere (e.g., "Qubit 0 Bloch Sphere"), and stores the reference to each sphere in the `qubitSpheres` dictionary.

- `UpdateState(QuantumState state)`: This method updates the visual representation of each qubit based on the provided `QuantumState`. It iterates through the state vector, retrieves the corresponding sphere, and smoothly interpolates its color based on the quantum state's real and imaginary components. The color transition is achieved using `Color.Lerp`, which creates a smooth visual effect as the quantum state changes.