# AdvancedCircuitBuilder

## Overview
The `AdvancedCircuitBuilder` script is designed to facilitate the creation and manipulation of quantum circuits within a Unity environment. It allows users to add qubits and quantum gates to a visual circuit representation, providing functionality for undoing and redoing actions. This script serves as a core component of a quantum computing simulation tool, enabling users to experiment with quantum gates and their effects on qubits.

## Variables

- **QubitPrefab**: A reference to the prefab used to instantiate qubit game objects in the scene.
- **GatePrefab**: A reference to the prefab used for quantum gate game objects, although it is not directly instantiated in the provided code.
- **QubitContainer**: A transform that acts as a parent for all qubit game objects, organizing them in the scene.
- **GateContainer**: A transform intended to hold quantum gate game objects, though it is not utilized in the current implementation.
- **GateDropdown**: A dropdown UI element that allows users to select which quantum gate to add to the circuit.
- **AddQubitButton**: A button UI element that triggers the addition of a new qubit when clicked.
- **AddGateButton**: A button UI element that triggers the addition of a selected quantum gate when clicked.
- **UndoButton**: A button UI element that allows the user to undo the last action performed on the circuit.
- **RedoButton**: A button UI element that allows the user to redo the last undone action.
- **qubits**: A list that stores the instantiated qubit game objects.
- **circuitGates**: A list that keeps track of the quantum gates added to the circuit.
- **undoStack**: A stack that stores previous states of the `circuitGates` list for undo functionality.
- **redoStack**: A stack that stores states that can be redone after an undo action.

## Functions

- **Start()**: Initializes the script by adding listeners to the button UI elements, linking user interactions to their corresponding functions (adding qubits, adding gates, undoing, and redoing actions).

- **AddQubit()**: Instantiates a new qubit at a calculated position based on the current count of qubits. It names the qubit and adds it to the `qubits` list. A debug message is logged to confirm the addition.

- **AddGate()**: Checks if there are any qubits available before allowing the user to add a gate. It retrieves the selected gate type from the dropdown and creates a corresponding `QuantumGate` object. If the gate is successfully created, it adds the gate to the `circuitGates` list and saves the current state for undo functionality.

- **Undo()**: Checks if there are any states in the `undoStack`. If so, it pushes the current state of `circuitGates` onto the `redoStack` and pops the last state from the `undoStack`, restoring it. A debug message is logged to indicate that the undo action was performed.

- **Redo()**: Checks if there are any states in the `redoStack`. If so, it pushes the current state of `circuitGates` onto the `undoStack` and pops the last state from the `redoStack`, restoring it. A debug message is logged to indicate that the redo action was performed.

- **SaveStateToUndoStack()**: Saves the current state of `circuitGates` to the `undoStack` and clears the `redoStack` to ensure that redo actions are only available after an undo operation. This function is called whenever a new gate is added.