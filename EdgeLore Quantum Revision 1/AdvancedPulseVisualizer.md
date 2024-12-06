# AdvancedPulseVisualizer

## Overview
The `AdvancedPulseVisualizer` script is designed to create a visual representation of a pulse in a Unity game environment. It allows developers to instantiate a pulse object with specific properties such as amplitude, frequency, and duration. This is useful for visualizing data in a dynamic and interactive way, enhancing the user experience by providing visual feedback based on these parameters.

## Variables
- **PulsePrefab**: A `GameObject` that serves as the template for creating pulse instances. This prefab should contain the necessary components and visual assets to represent a pulse.

## Functions
- **VisualizePulse(double amplitude, double frequency, double duration)**: This method creates a new pulse instance based on the provided amplitude, frequency, and duration. It does the following:
  - Instantiates a new pulse object from the `PulsePrefab` at the origin (0, 0, 0) with no rotation.
  - Names the instantiated pulse using the format "Pulse: Amp={amplitude}, Freq={frequency}, Dur={duration}" for easy identification in the hierarchy.
  - Adjusts the scale of the pulse object according to the amplitude, frequency, and duration values, thereby visually representing these parameters.
  - Modifies the color of the pulse's material based on the amplitude, frequency, and duration, normalizing these values to ensure the color remains within valid ranges.