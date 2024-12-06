# RealTimePulseVisualizer

## Overview
The `RealTimePulseVisualizer` script is designed to create and animate a pulse effect in a Unity game environment. It allows for the instantiation of a pulse visual using a prefab, where the size of the pulse can be adjusted based on amplitude, frequency, and duration parameters. This script is particularly useful for visualizing real-time data or events in a dynamic way, enhancing the interactivity and visual feedback of the application.

## Variables
- **PulsePrefab**: A `GameObject` reference to the prefab that represents the pulse visual. This prefab is instantiated whenever a new pulse is created.

## Functions
- **PlayPulse(double amplitude, double frequency, double duration)**: This public method is responsible for creating a new pulse visual. It takes three parameters:
  - `amplitude`: A `double` that determines the scale of the pulse in the x-axis.
  - `frequency`: A `double` that determines the scale of the pulse in the y-axis.
  - `duration`: A `double` that specifies how long the pulse will animate before being destroyed.
  
  The method instantiates the pulse prefab at the origin, scales it according to the provided parameters, and starts the animation coroutine.

- **AnimatePulse(GameObject pulse, float duration)**: This private coroutine method animates the pulse over a specified duration. It gradually increases the scale of the pulse object over time:
  - `pulse`: The `GameObject` instance of the pulse that is being animated.
  - `duration`: A `float` that indicates how long the animation should last.
  
  The method runs a loop that continues until the elapsed time reaches the duration, applying a slight growth effect to the pulse each frame. Once the animation is complete, the pulse object is destroyed to free up resources.