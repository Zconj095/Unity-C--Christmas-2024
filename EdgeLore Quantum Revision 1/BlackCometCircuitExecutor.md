# BlackCometCircuitExecutor

## Overview
The `BlackCometCircuitExecutor` script is designed to manage and execute various quantum circuit operations within a Unity environment. It serves as a central point for creating and manipulating different quantum states and circuits, such as entangled states, superposition fields, and chaos states. By leveraging this script, developers can easily add quantum computing functionalities to their Unity projects, providing a foundation for simulations or games that involve quantum mechanics concepts.

## Variables
- `numQubits` (int): This variable indicates the number of qubits to be used in the quantum circuits. It is serialized so that it can be modified directly in the Unity Inspector.
- `imposedField` (Vector3): This variable defines the vector representing the imposed field applied to the superpositioned circuit. It is also serialized for easy adjustment in the Unity Inspector.
- `initialPosition` (Vector3): This variable specifies the initial position of the particle state within the 3D space. It can be modified in the Unity Inspector to set the starting location of the particle.

## Functions
- `Start()`: This Unity lifecycle method is called before the first frame update. It triggers the execution of the quantum circuits by calling the `ExecuteNewCircuits()` method.

- `ExecuteNewCircuits()`: This public method orchestrates the creation and execution of various quantum circuits. It performs the following actions:
  - Instantiates an `EntangledStateCircuit` and creates an entangled state using the specified number of qubits.
  - Instantiates a `SuperpositionedImposedFieldCircuit` and applies the imposed field to the superposition of the specified number of qubits.
  - Instantiates an `EntangledChaosCircuit` and creates an entangled chaos state using the specified number of qubits.
  - Instantiates a `ChaosStateCircuit` and creates a chaos state with the specified number of qubits.
  - Instantiates a `ParticleStateCircuit` and establishes a particle state using the specified number of qubits and initial position. 

This method effectively connects multiple quantum circuit functionalities, enabling a comprehensive execution of quantum operations in the game or simulation.