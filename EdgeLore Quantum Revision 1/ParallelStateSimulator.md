# ParallelStateSimulator

## Overview
The `ParallelStateSimulator` class is a specialized type of `QuantumSimulator` that enhances the simulation of a quantum circuit by executing gate operations in parallel. This allows for improved performance when simulating circuits with multiple gates. The `Simulate` method iterates through each gate in the `Circuit` and applies them concurrently, utilizing multi-threading capabilities to speed up the simulation process. This class is designed to work seamlessly within the larger codebase that deals with quantum simulations, providing a more efficient way to handle quantum gate operations.

## Variables
- **Circuit**: An instance of `QuantumCircuit` that contains the quantum gates to be simulated.
- **visualizer**: An instance of `QuantumVisualizer` used for visual representation of the quantum simulation.
- **State**: A protected field inherited from the `QuantumSimulator` class that represents the current state of the quantum system.

## Functions
- **ParallelStateSimulator(QuantumCircuit circuit, QuantumVisualizer visualizer)**: Constructor that initializes a new instance of the `ParallelStateSimulator` class, taking a quantum circuit and a visualizer as parameters. It passes these parameters to the base `QuantumSimulator` class constructor.

- **void Simulate()**: This method executes the simulation of the quantum circuit. It uses `Parallel.ForEach` to iterate over each gate in the `Circuit.Gates` collection and applies each gate to the current state concurrently. After all gates have been processed, it logs a message indicating that the parallel simulation is complete.