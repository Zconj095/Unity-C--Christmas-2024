# QuantumGates

## Overview
The `QuantumGates` class provides static methods for applying quantum gates to qubits in a quantum computing simulation. It includes implementations for the Hadamard gate, the Toffoli gate, and a simplified version of quantum teleportation. This class is essential for manipulating quantum states within the codebase, allowing developers to simulate quantum operations and understand their effects on qubit amplitudes.

## Variables
- **Qubit**: This is a custom data structure (not defined in the provided code) that represents a quantum bit. It contains at least two properties:
  - **Amplitude0**: The amplitude of the qubit in the |0⟩ state.
  - **Amplitude1**: The amplitude of the qubit in the |1⟩ state.

## Functions
- **ApplyHadamard(Qubit qubit)**: 
  - This function takes a single `Qubit` as input and applies the Hadamard gate to it. The Hadamard gate creates superposition by transforming the amplitudes of the input qubit. The output is a new `Qubit` with updated amplitudes calculated from the original amplitudes.

- **ApplyToffoli(Qubit control1, Qubit control2, Qubit target)**: 
  - This function implements the Toffoli gate, which is a controlled-controlled-NOT gate. It takes two control qubits and one target qubit as input. The target qubit is flipped (swapped its amplitudes) only if both control qubits are in the |1⟩ state (i.e., their Amplitude1 is greater than 0.5). If the condition is not met, the target qubit remains unchanged.

- **QuantumTeleport(Qubit qubit)**: 
  - This function simulates quantum teleportation. In this simplified context, it acts as a passthrough, returning a new `Qubit` with the same amplitudes as the input qubit. This function serves as a placeholder for more complex teleportation logic that could be implemented in a more detailed simulation.