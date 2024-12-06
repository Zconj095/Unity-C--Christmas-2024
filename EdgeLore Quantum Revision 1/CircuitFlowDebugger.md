# CircuitFlowDebugger

## Overview
The `CircuitFlowDebugger` class is responsible for visually highlighting quantum gates in a Unity-based simulation. This functionality is essential for debugging and understanding the flow of quantum circuits, allowing developers or users to see which gate is currently being applied. The main function of this script is to change the color of a gate's visual representation to yellow when it is activated and then reset it back to white after a brief delay. This script integrates with the broader codebase by providing a debugging tool that enhances the visualization of quantum gate operations.

## Variables
- **gateObject**: A `GameObject` that represents the visual representation of the quantum gate identified by its name. It is used to access and modify the gate's material properties for highlighting.
- **renderer**: A `Renderer` component of the `gateObject`. It is used to change the color of the gate's visual representation.

## Functions
- **HighlightGate(QuantumGate gate)**: 
  - This public method takes a `QuantumGate` object as a parameter. It logs the name of the gate being applied and attempts to find the corresponding `GameObject` in the scene. If the gate is found, it changes the gate's color to yellow and starts a coroutine to reset the color back to white after a short delay.

- **ResetColor(Renderer renderer)**: 
  - This private coroutine method takes a `Renderer` as a parameter. It pauses execution for one second and then resets the color of the renderer's material back to white. This is used to revert the visual change after highlighting the gate.