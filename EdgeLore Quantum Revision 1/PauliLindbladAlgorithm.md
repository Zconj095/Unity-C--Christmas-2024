# PauliLindbladAlgorithm

## Overview
The `PauliLindbladAlgorithm` script is designed to simulate the effects of Pauli noise on a quantum state represented in a three-dimensional vector format (Vector3). This algorithm is particularly relevant in quantum computing contexts, where it is used to model how quantum states degrade due to noise. The script allows for the adjustment of noise rates associated with the Pauli X, Y, and Z operations, enabling users to explore the impact of these noise factors on a given initial quantum state.

## Variables
- `pauliRates`: An array of doubles that specifies the noise rates for the Pauli X, Y, and Z operations. The default values are set to 0.1 for each operation, indicating a 10% chance of noise affecting the state along each respective axis.
- `numQubits`: An integer that indicates the number of qubits being simulated. Currently, it defaults to 1, but it can be adjusted to simulate multiple qubits as needed.

## Functions
- `ApplyPauliLindblad(Vector3 initialState)`: This function takes an initial quantum state as a Vector3 input and applies the Pauli noise based on the specified rates. It calculates the new state by reducing each component of the initial state according to the corresponding noise rate. The resulting noisy state is logged for debugging purposes and then returned. This function encapsulates the core functionality of the script, simulating the degradation of the quantum state due to noise.