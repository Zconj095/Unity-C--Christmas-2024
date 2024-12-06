# QuantumAlgorithm

## Overview
The `QuantumAlgorithm` class serves as an abstract base class for implementing various quantum algorithms in the codebase. It establishes a common structure that all derived quantum algorithm classes must follow. The primary purpose of this class is to define a standard interface for executing quantum algorithms on a quantum circuit using a quantum simulator. By doing so, it ensures consistency and interoperability among different quantum algorithms within the framework.

## Variables
- **Name**: A string that holds the name of the quantum algorithm. It is set through the constructor and is read-only outside the class, ensuring that the name cannot be modified after the object is created.

## Functions
- **QuantumAlgorithm(string name)**: Constructor that initializes a new instance of the `QuantumAlgorithm` class. It takes a single parameter, `name`, which is used to set the `Name` property of the algorithm.
  
- **abstract void Execute(QuantumCircuit circuit, QuantumSimulator simulator)**: An abstract method that must be implemented by any derived class. This method is responsible for executing the quantum algorithm on the provided `QuantumCircuit` and using the specified `QuantumSimulator`. The implementation details will vary depending on the specific algorithm being executed.