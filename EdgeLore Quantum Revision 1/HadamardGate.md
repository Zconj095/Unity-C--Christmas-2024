# HadamardGate

The `HadamardGate` script is a Unity component that simulates the application of a Hadamard gate on a quantum state represented as a `Vector3`. The Hadamard gate is a fundamental operation in quantum computing that creates superpositions, allowing a quantum system to exist in multiple states simultaneously. This script is designed to transform an input state into two output states: one for a photon and one for an electron, and it logs the process for debugging purposes.

## Variables

- **InputState**: A `Vector3` representing the input quantum state. Each component of the vector corresponds to the amplitude of a particular state in the quantum system.

## Functions

- **ApplyHadamard()**: This function applies the Hadamard transformation to the `InputState`. It computes two new `Vector3` outputs:
  - **PhotonOutput**: A `Vector3` that represents the state of a photon after the Hadamard transformation.
  - **ElectronOutput**: A `Vector3` that represents the state of an electron after the Hadamard transformation.

  The function logs the input state and the resulting outputs to the console for easier debugging and understanding of the transformation process. It returns a tuple containing both the `PhotonOutput` and `ElectronOutput`.