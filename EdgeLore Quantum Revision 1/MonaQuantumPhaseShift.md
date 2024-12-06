# MonaQuantumPhaseShift

## Overview
The `MonaQuantumPhaseShift` script is a Unity component that manages a quantum state represented as a `Vector3`. The main function of this script is to apply a phase shift to the quantum state based on a given angle in radians. This phase shift alters the x and y components of the quantum state while keeping the z component unchanged. The script logs the quantum state before and after the phase shift, providing insight into the transformation that occurs.

## Variables
- **QuantumState (Vector3)**: This public variable represents the current quantum state of the object in three-dimensional space. It is a vector with three components (x, y, z) that can be manipulated through the phase shift operation.

## Functions
- **ApplyPhaseShift(float angle)**: 
  - This public method takes a single parameter, `angle`, which is a float representing the angle in radians to apply for the phase shift. 
  - It first logs the current quantum state and the angle being applied.
  - The method then calculates the new quantum state by applying the phase shift formula to the x and y components while keeping the z component the same. 
  - Finally, it logs the updated quantum state and returns the new `QuantumState` as a `Vector3`.