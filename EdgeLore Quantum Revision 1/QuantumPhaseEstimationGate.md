# QuantumPhaseEstimationGate

## Overview
The `QuantumPhaseEstimationGate` class represents a quantum gate used in quantum computing, specifically for phase estimation. It extends the `QuantumGate` base class and is designed to apply a phase rotation to a target qubit based on the states of control qubits. This gate is essential in quantum algorithms that estimate the eigenvalues of unitary operators, making it a crucial component in the field of quantum computation.

## Variables
- `numControlQubits`: An integer that indicates the number of control qubits used in the gate. Control qubits determine whether the target qubit undergoes a phase rotation.
- `targetQubit`: An integer representing the index of the target qubit that will be affected by the gate's operation.
- `phase`: A float value that specifies the phase rotation to be applied to the target qubit if the control qubits are active.

## Functions
- **Constructor**: 
  ```csharp
  public QuantumPhaseEstimationGate(int numControlQubits, int targetQubit, float phase)
  ```
  Initializes a new instance of the `QuantumPhaseEstimationGate` class. It takes the number of control qubits, the target qubit index, and the phase value as parameters. It also calls the base class constructor to set the gate's name and other properties.

- **Apply**: 
  ```csharp
  public float[] Apply(float[] stateVector)
  ```
  Simulates the behavior of the quantum phase estimation gate by applying a phase rotation to the target qubit based on the states of the control qubits. It takes an array of floats representing the state vector (which includes both real and imaginary parts) and returns a new state vector after the gate operation.

- **IsQubitActive**: 
  ```csharp
  private bool IsQubitActive(int index, int qubit)
  ```
  Determines whether a specific qubit (target qubit) is active in the given state index. It returns a boolean value indicating the active status of the target qubit.

- **IsControlQubitsActive**: 
  ```csharp
  private bool IsControlQubitsActive(int index)
  ```
  Checks if all control qubits are active in the given state index. It iterates through the control qubits and returns true only if all are active.

- **ToString**: 
  ```csharp
  public override string ToString()
  ```
  Returns a string representation of the `QuantumPhaseEstimationGate` instance, detailing the number of control qubits, the target qubit, and the phase value. This is useful for debugging and logging purposes.