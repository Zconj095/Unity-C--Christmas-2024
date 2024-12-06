# HadamardAlgorithm

## Overview
The `HadamardAlgorithm` class is a Unity script that implements the Hadamard transformation, a fundamental operation in quantum computing. This script is designed to apply the Hadamard transformation to a 2D vector, representing a quantum state. The transformation alters the input state and returns a new state that is a superposition of the original components. This functionality can be integrated into a larger quantum simulation or game environment within Unity, allowing for the exploration and visualization of quantum mechanics principles.

## Variables
- **initialState (Vector2)**: This variable represents the input quantum state as a 2D vector. The components of this vector are used in the Hadamard transformation calculation.

## Functions
- **ApplyHadamard(Vector2 initialState)**: This function takes a `Vector2` representing the initial quantum state and applies the Hadamard transformation to it. It calculates a new state using the formula derived from the Hadamard matrix and returns the resulting `Vector2`. Additionally, it logs the resulting state to the console for debugging purposes.