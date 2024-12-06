# CircuitInitializer

## Overview
The `CircuitInitializer` script is responsible for setting up a quantum circuit environment within a Unity application. It initializes the number of qubits and ancillas, sets the initial state of the circuit, and manages the addition of quantum gates to the circuit. This script acts as a foundational component in the codebase, providing essential functionality for circuit management and analysis.

## Variables
- `numQubits`: An integer representing the number of qubits in the circuit. It is configurable in the Unity Inspector.
- `numAncillas`: An integer representing the number of ancilla qubits in the circuit. It is also configurable in the Unity Inspector.
- `initialState`: A string representing the initial state of the quantum circuit, defaulting to "|0⟩". This can be modified in the Unity Inspector.
- `ancillaManager`: An instance of `AncillaManager`, responsible for managing ancilla qubits and their associated functions.
- `analysisPass`: An instance of `CircuitAnalysisPass`, which handles the analysis and processing of the circuit.

## Functions
- `Start()`: Unity's built-in method that is called before the first frame update. It invokes the `InitializeCircuit` method to set up the circuit when the script is first run.

- `InitializeCircuit()`: This private method initializes the circuit by creating instances of `AncillaManager` and `CircuitAnalysisPass`. It sets the primary and ancilla qubit counts based on the serialized fields and calls the `InitializeRegisters` method of `ancillaManager`. It also logs the initialization details to the console.

- `AddGate(string type, string qubit)`: This public method allows the addition of a quantum gate to the circuit. It takes in a string `type` representing the type of gate and a string `qubit` indicating which qubit the gate will operate on. It calls the `AddGate` method of `analysisPass` and logs the action to the console.

- `RunAnalysis()`: This public method triggers the analysis of the circuit by calling the `AnalyzeCircuit` method from the `analysisPass` instance. It does not return any value but is used to execute the analysis process on the initialized circuit.