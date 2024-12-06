# QuantumExecutor

## Overview
The `QuantumExecutor` class is a Unity script that simulates the execution of a quantum circuit. It allows users to define a number of qubits and a series of operations that can be applied to those qubits. The main function, `ExecuteCircuit`, logs the execution of the quantum circuit and the operations being performed. This class fits into a larger codebase that likely includes other components for managing quantum simulations, providing a way to manipulate quantum states and operations in a game or simulation environment.

## Variables
- `numQubits` (int): This variable holds the number of qubits that the quantum circuit will utilize. It is serialized so that it can be set in the Unity Inspector.
- `operations` (List<string>): This list stores the operations that will be executed on the qubits. It allows for dynamic addition of operations through the `AddOperation` method.

## Functions
- `ExecuteCircuit()`: This method is responsible for executing the defined quantum circuit. It logs the number of qubits and each operation being executed. It serves as the main entry point for running the quantum simulation.
  
- `AddOperation(string operation)`: This method allows users to add a new operation to the list of operations. It takes a string parameter representing the operation and logs a message indicating that the operation has been added. This function facilitates the dynamic building of the circuit before execution.