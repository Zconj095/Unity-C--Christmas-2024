# QuantumCircuitInteraction

## Overview
The `QuantumCircuitInteraction` script is designed to facilitate user interaction with quantum circuit elements in a Unity-based application. Its primary function is to detect mouse clicks on quantum objects within the scene, log the selected object's name, and display relevant details about the selected qubit. This script enhances the user experience by allowing users to interactively explore the quantum circuit's components.

## Variables
- `mainCamera`: A reference to the main camera in the scene. This variable is initialized in the `Start` method and is used to convert screen coordinates into a ray for detecting mouse clicks on 3D objects.

## Functions
- `Start()`: This Unity lifecycle method is called before the first frame update. It initializes the `mainCamera` variable by assigning it the main camera present in the scene.

- `Update()`: This method is called once per frame. It checks if the left mouse button has been clicked. If so, it casts a ray from the camera into the scene based on the mouse's position. If the ray intersects with a collider, it retrieves the selected game object, logs its name, and calls the `DisplayQubitDetails` method to show further information.

- `DisplayQubitDetails(GameObject qubit)`: This private method takes a `GameObject` representing a qubit as a parameter. It logs a message indicating that it is displaying details for the specified qubit. Additional UI or logic can be implemented here to present the qubit's state and associated gates to the user.