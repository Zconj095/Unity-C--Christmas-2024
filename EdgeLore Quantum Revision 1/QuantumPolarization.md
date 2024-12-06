# QuantumPolarization

## Overview
The `QuantumPolarization` script is designed to manage the polarization states of qubits within a quantum computing framework in Unity. It initializes a specified number of qubits, each represented by a polarization vector, and provides methods to set and retrieve the polarization states of these qubits. This script serves as a foundational component for simulating quantum behavior in a Unity environment, making it easier to manipulate and observe qubit states.

## Variables

- `numQubits` (int): The number of qubits to be initialized. This determines how many polarization vectors will be created.
- `polarizationVectors` (Vector3[]): An array that holds the polarization vectors for each qubit. Each vector represents the state of a corresponding qubit.

## Functions

- `void Start()`: This is a Unity-specific method that is called when the script instance is being loaded. It initializes the polarization vectors by calling `InitializePolarization`.

- `public void InitializePolarization()`: This method initializes the `polarizationVectors` array based on the number of qubits specified by `numQubits`. Each qubit is defaulted to the |0⟩ state, represented by a vector pointing along the Z-axis (0, 0, 1). It logs a message indicating that the polarization vectors have been initialized.

- `public void SetPolarization(int qubit, Vector3 newVector)`: This method allows the user to set the polarization vector for a specific qubit. It takes an index (`qubit`) and a new vector (`newVector`) as parameters. If the specified qubit index is invalid, it logs an error message. If valid, it updates the polarization vector for the specified qubit and logs the new state.

- `public Vector3 GetPolarization(int qubit)`: This method retrieves the polarization vector for a specified qubit. It takes an index (`qubit`) as a parameter. If the index is invalid, it logs an error message and returns a zero vector. If valid, it returns the corresponding polarization vector.