# SystemThellia

## Overview
The `SystemThellia` script is designed to simulate a quantum computing process using qubits and quantum gates. It initializes qubits, applies quantum gates such as the Toffoli and Hadamard gates, and demonstrates quantum teleportation. The results are then stored in a quantum hyperstate and logged for output. This script fits within a larger codebase that likely includes definitions for qubits and quantum gates, allowing for the exploration of quantum mechanics in a Unity environment.

## Variables
- `qubit1`: An instance of the `Qubit` class, initialized to the state |0⟩ (1f, 0f).
- `qubit2`: An instance of the `Qubit` class, initialized to the state |1⟩ (0f, 1f).
- `target`: An instance of the `Qubit` class, initialized to the state |0⟩ (1f, 0f).
- `toffoliOutput`: An instance of the `Qubit` class that stores the output of the Toffoli gate applied to `qubit1`, `qubit2`, and `target`.
- `hadamardLeft`: An instance of the `Qubit` class that stores the result of applying a Hadamard gate to `toffoliOutput`.
- `hadamardRight`: An instance of the `Qubit` class that also stores the result of applying a Hadamard gate to `toffoliOutput`.
- `teleportedQubit`: An instance of the `Qubit` class that stores the result of simulating quantum teleportation on `toffoliOutput`.
- `hyperstate`: An instance of the `ThelliaQuantumHyperstate` class that holds a collection of qubits.

## Functions
- `Start()`: This is the main function that runs when the script is initialized. It performs the following tasks:
  1. Initializes three qubits in specific quantum states.
  2. Applies the Toffoli gate to the first two qubits and a target qubit, producing an output qubit.
  3. Applies the Hadamard gate to the output of the Toffoli gate twice, creating two new qubits.
  4. Simulates quantum teleportation on the output qubit from the Toffoli gate.
  5. Creates a `ThelliaQuantumHyperstate` instance and adds the results of the Hadamard gates and the teleported qubit to it.
  6. Outputs the string representation of the quantum hyperstate to the debug log.