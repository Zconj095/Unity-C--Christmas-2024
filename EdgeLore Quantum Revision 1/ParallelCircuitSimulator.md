# ParallelCircuitSimulator

## Overview
The `ParallelCircuitSimulator` class is designed to simulate multiple electrical circuits in parallel using Unity's MonoBehaviour framework. It takes a list of circuits, each represented as an array of strings (where each string denotes a gate in the circuit), and simulates them concurrently. This allows for efficient processing of multiple circuits, making it suitable for applications where performance is critical, such as in game development or real-time simulations.

## Variables
- `numCircuits`: An integer that specifies the number of circuits to simulate. This variable is serialized, allowing it to be set from the Unity Editor.

## Functions
- `SimulateCircuits(List<string[]> circuits)`: This method initiates the simulation of all the provided circuits. It uses `Parallel.For` to iterate through each circuit in the list and calls `SimulateSingleCircuit` for each one. After all circuits have been simulated, it logs a message indicating completion.

- `SimulateSingleCircuit(string[] circuit, int circuitIndex)`: This private method simulates a single circuit. It takes an array of strings representing the gates in the circuit and the index of the circuit being simulated. For each gate in the circuit, it logs the execution of the gate. The actual simulation logic can be added where indicated in the comments.