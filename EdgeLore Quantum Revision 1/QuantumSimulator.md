# QuantumSimulator

## Overview
The `QuantumSimulator` class is responsible for simulating a quantum circuit using a specified visualizer. It manages the quantum state of the circuit and applies gates defined in the quantum circuit while visualizing each operation. This class forms a crucial part of the codebase by providing the functionality to simulate quantum operations and visualize their effects, which is essential for understanding quantum computing concepts.

## Variables

- **Circuit**: 
  - Type: `QuantumCircuit`
  - Description: Represents the quantum circuit to be simulated. It is initialized through the constructor and cannot be null.

- **Visualizer**: 
  - Type: `QuantumVisualizer`
  - Description: A protected property that provides access to the visualizer for derived classes, enabling them to visualize quantum operations.

- **visualizer**: 
  - Type: `QuantumVisualizer`
  - Description: An instance of the `QuantumVisualizer` that is used to visualize the gates being applied during the simulation. It is initialized through the constructor.

- **state**: 
  - Type: `QuantumState`
  - Description: Represents the current state of the quantum system, initialized with the number of qubits in the circuit.

- **noiseModels**: 
  - Type: `List<NoiseModel>`
  - Description: A list that holds various noise models that can be applied to the simulation. It is initialized as an empty list.

## Functions

- **QuantumSimulator(QuantumCircuit circuit, QuantumVisualizer visualizer)**: 
  - Description: Constructor that initializes the `QuantumSimulator` with a given quantum circuit and visualizer. It throws an `ArgumentNullException` if either the circuit or visualizer is null.

- **Simulate()**: 
  - Description: This method executes the simulation of the quantum circuit. It logs the start of the simulation, iterates through each gate in the circuit, applies the gate while logging its name, and visualizes the gate using the visualizer. Finally, it logs the completion of the simulation.

- **AddNoiseModel(NoiseModel noiseModel)**: 
  - Description: This method adds a specified noise model to the list of noise models. This allows for the inclusion of various types of noise in the simulation, which can be important for studying the effects of noise on quantum computations.