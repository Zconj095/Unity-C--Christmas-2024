# PulseVisualizer

## Overview
The `PulseVisualizer` script is designed to create and visualize pulse effects in a Unity environment. It instantiates a pulse object based on specified parameters such as amplitude, frequency, and duration. This script is intended to be used within a Unity project to enhance visual feedback for audio or other signals, making it easier for developers to represent data visually.

## Variables
- **PulsePrefab**: A public `GameObject` variable that holds a reference to the prefab used for visualizing the pulse. This prefab is the template from which pulse instances are created.

## Functions
- **VisualizePulse(double amplitude, double frequency, double duration)**: This public method takes three parameters: amplitude, frequency, and duration. It performs the following actions:
  - Instantiates a new pulse object from the `PulsePrefab` at the origin (0, 0, 0) with no rotation.
  - Names the instantiated pulse object using the provided amplitude, frequency, and duration for easy identification.
  - Adjusts the visual properties of the pulse object by setting its local scale based on the amplitude, frequency, and duration values passed as arguments. This scaling visually represents the characteristics of the pulse.