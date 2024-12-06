# EntangledChaosCircuit

## Overview
The `EntangledChaosCircuit` class is responsible for creating a quantum circuit that generates an entangled state with a specified number of qubits and introduces chaos by applying random quantum gates to each qubit. This class fits within a larger codebase focused on simulating quantum mechanics, specifically dealing with quantum entanglement and gate operations in Unity.

## Variables
- `random`: An instance of `System.Random` used to generate random numbers for selecting quantum gates. This ensures that the gates applied to the qubits are random, introducing chaos into the circuit.

## Functions
- `CreateEntangledChaos(int numQubits)`: This public method takes an integer parameter `numQubits`, which specifies how many qubits will be involved in the entangled state. It first logs the creation process, then creates an initial entangled state using an instance of `EntangledStateCircuit`. After that, it applies a random gate to each qubit, logging the gate applied for each qubit. Finally, it logs the completion of the entangled chaos state creation.

- `GetRandomGate()`: This private method returns a random quantum gate from a predefined array of gates, which includes "X", "Y", "Z", "H", "S", and "T". It uses the `random` variable to select an index from the gates array, ensuring a different gate can be chosen each time it is called.