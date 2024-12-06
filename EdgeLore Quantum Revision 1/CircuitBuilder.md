# CircuitBuilder

## Overview
The `CircuitBuilder` script is responsible for creating and managing a quantum circuit within a Unity application. It provides functionality to add qubits and quantum gates to the circuit and simulate the quantum operations. This script acts as a user interface controller, allowing users to interact with the quantum circuit through buttons and dropdowns in the Unity environment.

## Variables

- **QubitPrefab**: A `GameObject` that serves as the template for creating new qubit instances in the circuit.
- **GateDropdown**: A `Dropdown` UI component that allows users to select different quantum gates to be added to the circuit.
- **AddQubitButton**: A `Button` UI component that triggers the addition of a new qubit when clicked.
- **AddGateButton**: A `Button` UI component that triggers the addition of a selected quantum gate when clicked.
- **SimulateButton**: A `Button` UI component that initiates the simulation of the current quantum circuit when clicked.
- **QubitContainer**: A `Transform` that serves as the parent object for all qubits, determining their position in the scene.
- **qubits**: A `List<GameObject>` that holds all the qubit instances currently in the circuit.
- **circuit**: An instance of the `QuantumCircuit` class that represents the current state of the quantum circuit.

## Functions

- **Start()**: This function is called when the script instance is being loaded. It initializes the quantum circuit and sets up listeners for the button clicks to trigger the corresponding actions (adding qubits, adding gates, simulating the circuit).

- **AddQubit()**: This function is called when the user clicks the "Add Qubit" button. It instantiates a new qubit using the `QubitPrefab`, positions it in the scene based on the current count of qubits, and updates the `circuit` to reflect the new qubit addition.

- **AddGate()**: This function is called when the user clicks the "Add Gate" button. It checks if there are any qubits available; if not, it logs a warning. If qubits are present, it retrieves the selected gate from the `GateDropdown` and adds it to the `circuit`. It currently supports adding Hadamard and CNOT gates, defaulting to the first qubit(s) for simplicity.

- **SimulateCircuit()**: This function is called when the user clicks the "Simulate" button. It finds an instance of the `QuantumVisualizer` in the scene, creates a `QuantumSimulator` with the current circuit and visualizer, and calls the `Simulate` method to perform the simulation of the quantum operations.