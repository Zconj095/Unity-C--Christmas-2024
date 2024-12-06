# QuantumFluxManager

## Overview
The `QuantumFluxManager` script is a Unity component that manages and computes the total quantum flux based on three predefined flux states. It aggregates the values of these flux states and outputs the total flux value to the console. This functionality could be part of a larger system in a game or simulation where quantum properties are relevant, allowing for easy adjustments and monitoring of quantum flux values.

## Variables
- `flux1`: A float representing the first quantum flux state. Default value is `1.0f`.
- `flux2`: A float representing the second quantum flux state. Default value is `0.5f`.
- `flux3`: A float representing the third quantum flux state. Default value is `0.8f`.

## Functions
- `ComputeFlux()`: This function calculates the total quantum flux by summing `flux1`, `flux2`, and `flux3`. It logs the computed flux output to the console and returns the total flux value as a float.