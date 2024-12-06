# ThelliaQuantumHyperstate

## Overview
The `ThelliaQuantumHyperstate` class is designed to manage and process quantum states represented by qubits. It allows for the addition of qubits, calculates the combined output state, and forms a hyperstate from a set of input vectors. This class plays a crucial role in simulating quantum computations by providing functionality to manipulate and analyze quantum states within the context of a Unity game or application.

## Variables
- `List<Qubit> qubits`: A list that stores the qubits, which are the fundamental units of quantum information. Each qubit has associated amplitudes that contribute to the overall quantum state.

- `Vector3[] Inputs`: An array of 3D vectors that represent the inputs used to form the quantum hyperstate.

## Functions
- `public void AddQubit(Qubit qubit)`: This function adds a `Qubit` object to the `qubits` list, allowing the class to keep track of multiple qubits for further processing.

- `public Qubit CalculateOutput()`: This function calculates the combined output of all qubits in the `qubits` list. It sums the amplitudes of the `Amplitude0` and `Amplitude1` properties of each qubit and returns a new `Qubit` instance representing the resulting state.

- `public override string ToString()`: This function overrides the default `ToString` method to provide a string representation of the final quantum state by calling `CalculateOutput()` and formatting the result.

- `public Vector3 FormHyperstate()`: This function aggregates the input vectors stored in the `Inputs` array to form a single quantum hyperstate. It normalizes the resulting vector by dividing the sum by the number of inputs and logs the process to the console. The function returns the normalized hyperstate as a `Vector3`.