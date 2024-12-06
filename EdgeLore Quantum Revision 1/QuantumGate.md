# QuantumGate

## Overview
The `QuantumGate` class represents a quantum gate in a quantum computing framework. Each instance of `QuantumGate` encapsulates the name of the gate, the number of qubits it operates on, the specific qubits involved, and the mathematical operation that defines the gate's behavior. This class serves as a foundational component of the codebase, allowing for the implementation and manipulation of various quantum gates, which are essential for quantum algorithms and computations.

## Variables

- `Name`: A string that holds the name of the quantum gate (e.g., "Hadamard", "PauliX").
- `NumQubits`: An integer that specifies how many qubits the gate operates on.
- `Qubits`: An array of integers representing the specific qubits that the gate affects.
- `Operation`: A function that takes an array of complex numbers (representing the quantum state) and returns a new array of complex numbers after applying the gate's operation.

## Functions

### Constructor
- `QuantumGate(string name, int numQubits, int[] qubits, Func<Complex[], Complex[]> operation)`: Initializes a new instance of the `QuantumGate` class with the specified name, number of qubits, qubit indices, and the operation to perform.

### Static Methods

- `static QuantumGate Hadamard(int qubit)`: Creates a Hadamard gate that operates on the specified qubit. The Hadamard gate transforms the state into a superposition.

- `static QuantumGate PauliZ(int qubit)`: Creates a Pauli-Z gate that flips the phase of the specified qubit's state if it's in the |1⟩ state.

- `static QuantumGate Phase(int qubit, double angle)`: Creates a phase gate that applies a phase shift defined by the angle to the specified qubit's state.

- `static QuantumGate CNOT(int control, int target)`: Creates a Controlled-NOT (CNOT) gate that flips the target qubit if the control qubit is in the |1⟩ state.

- `static QuantumGate PauliX(int qubit)`: Creates a Pauli-X gate (also known as a NOT gate) that flips the state of the specified qubit.

- `static QuantumGate PauliY(int qubit)`: Creates a Pauli-Y gate that flips the state of the specified qubit and applies a phase shift.

- `static QuantumGate RX(int qubit, double angle)`: Creates an RX rotation gate that rotates the state of the specified qubit around the X-axis by the given angle.

- `static QuantumGate RY(int qubit, double angle)`: Creates an RY rotation gate that rotates the state of the specified qubit around the Y-axis by the given angle.

- `static QuantumGate RZ(int qubit, double angle)`: Creates an RZ rotation gate that applies a phase shift to the specified qubit based on the given angle.

- `static QuantumGate SWAP(int qubit1, int qubit2)`: Creates a SWAP gate that swaps the states of the two specified qubits.

- `static QuantumGate Toffoli(int control1, int control2, int target)`: Creates a Toffoli gate (also known as a controlled-controlled-NOT) that flips the target qubit if both control qubits are in the |1⟩ state.

- `static QuantumGate Fredkin(int control, int swap1, int swap2)`: Creates a Fredkin gate (also known as a controlled-SWAP) that swaps the states of two qubits if the control qubit is in the |1⟩ state. 

This documentation provides a clear understanding of the `QuantumGate` class, its purpose, and its components, enabling developers to effectively utilize and extend its functionality within the quantum computing framework.