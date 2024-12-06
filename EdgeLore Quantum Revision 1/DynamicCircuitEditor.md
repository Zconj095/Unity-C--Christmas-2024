# DynamicCircuitEditor

## Overview
The `DynamicCircuitEditor` script is designed to facilitate the manipulation of a quantum circuit within a Unity environment. It allows users to dynamically add, remove, and reorder quantum gates in a quantum circuit. The script initializes a quantum circuit with a specified number of qubits and provides methods to alter the circuit's structure. This functionality is essential for developers working on quantum computing simulations or educational tools that require interactive circuit editing.

## Variables
- `public QuantumCircuit circuit`: This variable holds the instance of the `QuantumCircuit` class, representing the quantum circuit being edited. It is initialized with a default of 2 qubits in the `Start` method.

## Functions
- `void Start()`: This method is called when the script is first run. It initializes the `circuit` variable with a new instance of `QuantumCircuit`, set to have 2 qubits.

- `public void AddGate(string gateType, int targetQubit, int controlQubit = -1)`: This method adds a quantum gate to the circuit. It takes in the type of gate (as a string), the index of the target qubit, and an optional control qubit index for gates that require it (like CNOT). It creates the appropriate `QuantumGate` object based on the gate type and adds it to the circuit.

- `public void RemoveGate(int gateIndex)`: This method removes a quantum gate from the circuit at the specified index. It checks if the index is valid and, if so, removes the gate and logs a message. If the index is invalid, it logs a warning.

- `public void ReorderGate(int fromIndex, int toIndex)`: This method allows the user to change the position of a gate within the circuit. It checks if both indices are valid, moves the gate from the `fromIndex` to the `toIndex`, and logs the reordering action. If the indices are invalid, it logs a warning.