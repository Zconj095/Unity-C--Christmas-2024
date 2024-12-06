# QuantumFourierTransformCircuit

## Overview
The `QuantumFourierTransformCircuit` class is a Unity script that implements the Quantum Fourier Transform (QFT) algorithm for a specified number of qubits. The QFT is a crucial component in quantum computing, primarily used for quantum algorithms that require frequency analysis. This script logs the steps taken during the application of QFT, making it easier to understand the transformation process applied to the qubits.

## Variables
- **numQubits**: An integer parameter that specifies the number of qubits to which the Quantum Fourier Transform will be applied.

## Functions
- **ApplyQFT(int numQubits)**: This public method executes the Quantum Fourier Transform on the specified number of qubits. It performs the following steps:
  - Logs the initiation of the QFT application.
  - Applies a Hadamard (H) gate to each qubit sequentially.
  - For each qubit, it calculates the angle for the Controlled R (rotation) gate based on the difference in indices between the current qubit and all subsequent qubits, logging each application of the Controlled R gate.
  - Finally, it logs the completion of the Quantum Fourier Transform process.