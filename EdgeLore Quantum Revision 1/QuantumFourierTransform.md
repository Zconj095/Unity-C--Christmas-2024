# QuantumFourierTransform

## Overview
The `QuantumFourierTransform` script is designed to apply a Quantum Fourier Transform (QFT) to a quantum state represented as a `Vector3`. This transformation is a fundamental operation in quantum computing, used to convert a quantum state into its frequency domain representation. The script integrates with Unity's MonoBehaviour, allowing it to be used in a Unity project where quantum simulations or visualizations may be required.

## Variables

- **QuantumState (Vector3)**: This public variable holds the current quantum state of the system. It is a three-dimensional vector where each component represents a different aspect of the quantum state.

## Functions

- **ApplyQFT() (Vector3)**: This public function applies the Quantum Fourier Transform to the `QuantumState`. It performs a mathematical transformation using cosine and sine functions on the x and y components of the `QuantumState`. The transformed state is then logged for debugging purposes and returned as the new quantum state.