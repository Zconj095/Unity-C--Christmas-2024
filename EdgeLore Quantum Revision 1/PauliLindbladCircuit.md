# PauliLindbladCircuit

## Overview
The `PauliLindbladCircuit` class is designed to simulate the application of Pauli-Lindblad noise to a quantum state represented as a `Vector3`. This class is part of a Unity project and serves to introduce noise into quantum states by modifying their components based on predefined rates for each of the Pauli operators (X, Y, Z). The `ApplyPauliLindblad` method takes an initial quantum state and applies the noise, producing a new state that reflects the effects of this noise.

## Variables
- `pauliRates`: An array of doubles that holds the noise rates for the Pauli operators. The indices correspond to:
  - `pauliRates[0]`: Rate for the X noise.
  - `pauliRates[1]`: Rate for the Y noise.
  - `pauliRates[2]`: Rate for the Z noise.

## Functions
- `ApplyPauliLindblad(Vector3 initialState)`: 
  - This method applies the Pauli-Lindblad noise to the provided `initialState`. It calculates the new state by reducing each component of the initial state by its corresponding noise rate. The results are logged to the console for debugging purposes, allowing the user to see the effects of the noise on the quantum state.