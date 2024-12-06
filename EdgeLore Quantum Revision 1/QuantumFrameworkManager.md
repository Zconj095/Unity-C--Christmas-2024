# QuantumFrameworkManager

## Overview
The `QuantumFrameworkManager` script is designed to manage the initialization of a quantum circuit within a Unity application. It serves as a starting point for setting up quantum visualizations by creating a `QuantumCircuit` object and configuring its properties. This script is intended to be used in conjunction with a `QuantumVisualizer`, which is expected to be assigned through the Unity Inspector. This ensures that the quantum framework can be visualized properly within the Unity environment.

## Variables
- `public QuantumVisualizer visualizer`: This variable holds a reference to a `QuantumVisualizer` component. It is assigned via the Unity Inspector and is essential for visualizing the quantum circuit.

## Functions
- `void Start()`: This is a Unity lifecycle method that is called when the script instance is being loaded. It performs the following tasks:
  - Logs a message indicating that the `QuantumFrameworkManager` is starting.
  - Checks if the `visualizer` variable is assigned. If not, it logs an error and exits the method.
  - Creates a new GameObject named "QuantumCircuit" and adds a `QuantumCircuit` component to it.
  - Sets the `NumQubits` property of the `QuantumCircuit` to 2, initializing it for use.
  - Logs a message confirming that the `QuantumCircuit` has been initialized successfully.