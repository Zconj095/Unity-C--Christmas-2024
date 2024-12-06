# OptimizedQuantumState

## Overview
The `OptimizedQuantumState` class is designed to represent the state of a quantum system consisting of qubits. It initializes a quantum state vector based on the number of qubits specified and provides functionality to apply quantum gates to alter the state of the system. The class integrates parallel processing to enhance the performance of gate operations, making it suitable for simulations involving multiple qubits.

## Variables
- `StateVector`: An array of `Complex` numbers that represents the quantum state of the system. It is initialized to represent the |0...0⟩ state when the object is created.

## Functions
- `OptimizedQuantumState(int numQubits)`: Constructor that initializes the `StateVector` based on the number of qubits specified. It calculates the size of the state vector as \(2^{\text{numQubits}}\) and sets the initial state to |0...0⟩.

- `void ApplyGate(QuantumGate gate)`: This method applies a quantum gate operation to the current state vector. It checks if the gate operation is defined and, if so, calls the `ParallelGateOperation` method to perform the operation. If the operation is not defined, it logs a message to the console.

- `private Complex[] ParallelGateOperation(Func<Complex[], Complex[]> operation, Complex[] inputState)`: This private method executes the provided quantum gate operation in parallel on the input state vector. It creates a new output state array and uses `Parallel.For` to apply the operation to each element of the input state, improving performance for large state vectors. The resultant state is then returned.