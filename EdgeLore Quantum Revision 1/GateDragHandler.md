# GateDragHandler

## Overview
The `GateDragHandler` script is responsible for handling the drag-and-drop functionality of quantum gates within a Unity-based user interface. This script allows users to visually manipulate gates and attach them to specific qubits in a quantum circuit. It integrates with the broader codebase by interacting with a `QuantumCircuit` object to add gates based on user actions, ensuring that the quantum logic can be visually constructed and modified in real-time.

## Variables
- `private RectTransform rectTransform`: A reference to the RectTransform component of the GameObject that this script is attached to, used for positioning during drag operations.
- `private Canvas canvas`: A reference to the parent Canvas component, used to adjust the drag movement according to the canvas's scale.
- `public QuantumCircuit circuit`: An instance of the `QuantumCircuit` class that the script interacts with to add gates. This variable is assumed to be assigned from another part of the codebase.

## Functions
- `void Start()`: Initializes the `rectTransform` and `canvas` variables by retrieving the respective components from the GameObject and its parent.

- `public void OnBeginDrag(PointerEventData eventData)`: Triggered when the user starts dragging the gate. It logs a message indicating that the drag has begun.

- `public void OnDrag(PointerEventData eventData)`: Called continuously while the user is dragging the gate. It updates the position of the gate based on the drag movement, adjusting for the canvas scale.

- `public void OnEndDrag(PointerEventData eventData)`: Invoked when the user releases the drag. It checks for a valid qubit under the drop location and attempts to attach the gate to the circuit if a valid qubit is detected.

- `private GameObject DetectQubit(PointerEventData eventData)`: Casts a ray from the camera to detect if the user has dropped the gate over a GameObject tagged as "Qubit". Returns the detected qubit GameObject or null if none is found.

- `private int GetQubitIndex(GameObject qubit)`: Parses the name of the qubit GameObject to extract its index. It assumes the naming convention is "Qubit X" where X is the index. Returns the index or -1 if parsing fails.

- `private void AttachGateToCircuit(string gateName, int qubitIndex)`: Creates and adds a quantum gate to the circuit based on the gate's name and the specified qubit index. It handles various gate types and ensures that the circuit has the required number of qubits before attaching the gate. Logs messages indicating the outcome of the operation.