# QuantumCircuitVisualizer

## Overview
The `QuantumCircuitVisualizer` script is designed to create a visual representation of a quantum circuit within a Unity environment. It initializes a specified number of qubits represented as spheres and allows the addition of quantum gates, which are visualized as cubes. The script also supports the visualization of controlled gates, connecting them to their control qubits with lines. This functionality is crucial for developers working on quantum computing simulations or educational tools, as it provides a clear and interactive way to understand quantum circuits.

## Variables
- `numQubits` (int): Specifies the number of qubits in the quantum circuit. This value is serialized, allowing it to be set in the Unity editor.
- `gateSpacing` (float): Defines the spacing between the visual representations of the gates in the circuit. This value is also serialized for easy adjustment in the Unity editor.
- `qubitSpheres` (GameObject[]): An array that holds the visual representations of the qubits, created as sphere GameObjects.

## Functions
- `void Start()`: This Unity lifecycle method is called before the first frame update. It invokes the `InitializeCircuitVisualization` method to set up the visual representation of the quantum circuit.

- `public void InitializeCircuitVisualization()`: Initializes the visualization of the quantum circuit by creating spheres for each qubit based on the `numQubits` variable. It positions the spheres vertically spaced apart according to `gateSpacing` and colors them blue. A debug message is logged upon completion.

- `public void AddGate(string gateType, int targetQubit, int controlQubit = -1)`: Adds a gate to the quantum circuit visualization. It takes the `gateType` (a string representing the type of gate), the `targetQubit` (the index of the qubit the gate acts upon), and an optional `controlQubit` (the index of a control qubit for controlled gates). If a control qubit is specified, it creates a red sphere for the control qubit and draws a yellow line connecting it to the gate. The target gate is visualized as a green cube.

- `private void DrawLine(Vector3 start, Vector3 end, Color color)`: Creates a visual line between two points in the 3D space. It uses Unity's `LineRenderer` component to visually represent the connection between the control qubit and the gate. The line's color and width are set based on the parameters provided.

This documentation serves as a guide for developers to understand the structure and functionality of the `QuantumCircuitVisualizer` script, facilitating easier integration and modifications within the codebase.