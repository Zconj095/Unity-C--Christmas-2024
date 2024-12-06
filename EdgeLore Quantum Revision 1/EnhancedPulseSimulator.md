# EnhancedPulseSimulator

## Overview
The `EnhancedPulseSimulator` class extends the `QuantumSimulator` class, providing functionality to simulate the effects of applying a pulse to a quantum state. It initializes a quantum state based on the number of qubits in the provided quantum circuit and visualizes the pulse application through a visualizer. The core functionality of this class revolves around applying a pulse characterized by its amplitude, frequency, and duration, which modifies the quantum state through a combination of rotation and phase shift.

## Variables
- **state**: An instance of `QuantumState` that represents the current state of the quantum system being simulated. It is initialized with the number of qubits from the provided quantum circuit.

## Functions
- **EnhancedPulseSimulator(QuantumCircuit circuit, QuantumVisualizer visualizer)**: 
  - Constructor that initializes the `EnhancedPulseSimulator` with a quantum circuit and a visualizer. It checks for null arguments and initializes the quantum state based on the number of qubits in the circuit.

- **void ApplyPulse(double amplitude, double frequency, double duration)**: 
  - This method applies a pulse to the quantum state. It takes in the amplitude, frequency, and duration of the pulse, logs the parameters, and calls the visualizer to visualize the pulse. It calculates the rotation angle and phase shift based on the amplitude, duration, and frequency, and then applies these transformations to the quantum state.

- **private void ApplyRotationAndPhase(double rotationAngle, double phaseShift)**: 
  - This private method performs the actual modification of the quantum state by applying a rotation and phase shift. It iterates through the state vector, computes the new state for each component based on the rotation and phase shift, and updates the state vector accordingly. If the state or its vector is null, it logs an error and exits without making changes.