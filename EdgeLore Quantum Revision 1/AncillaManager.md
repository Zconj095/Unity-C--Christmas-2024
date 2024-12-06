# AncillaManager

## Overview
The `AncillaManager` script is responsible for managing quantum registers in a Unity application, specifically focusing on primary and ancilla qubits. It initializes these registers, allows for the retrieval, addition, and removal of ancilla qubits, and provides logging for debugging purposes. This script fits within a larger codebase that likely involves quantum computing simulations or educational tools, enabling users to manage and manipulate quantum states effectively.

## Variables

- **primaryQubitCount**: An integer representing the number of primary qubits. This variable is public, allowing it to be modified from the Unity Inspector.
  
- **ancillaQubitCount**: An integer representing the number of ancilla qubits. This variable is also public, providing flexibility for configuration in the Unity Inspector.
  
- **primaryRegisters**: A list of strings that stores the names of the primary qubits. It is initialized as an empty list and populated during the register initialization process.

- **ancillaRegisters**: A list of strings that stores the names of the ancilla qubits. Similar to primaryRegisters, it starts empty and is filled based on the number of ancilla qubits.

## Functions

- **InitializeRegisters()**: This public method clears any existing entries in `primaryRegisters` and `ancillaRegisters`, then populates them with names based on the specified counts for primary and ancilla qubits. It logs the names of the registers to the console for verification.

- **GetAncilla(int index)**: This public method retrieves the name of an ancilla qubit at the specified index. If the index is out of bounds, it logs an error message and returns `null`.

- **AddAncilla()**: This public method adds a new ancilla qubit to the `ancillaRegisters` list, naming it based on the current count of existing ancilla qubits. It logs the addition of the new ancilla qubit to the console.

- **RemoveAncilla(int index)**: This public method removes an ancilla qubit from the `ancillaRegisters` list at the specified index. If the index is invalid, it logs an error message. Upon successful removal, it logs the name of the removed ancilla qubit.