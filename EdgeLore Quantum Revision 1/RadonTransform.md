# RadonTransform

## Overview
The `RadonTransform` script is a Unity component that computes a Radon Transform based on two specified states, `LeftState` and `RightState`. This script is designed to be attached to a GameObject in a Unity scene, allowing it to calculate the average of the two vector states and log the results. This functionality can be useful in scenarios where you need to blend or average two positions or orientations in 3D space, such as in animations or simulations.

## Variables
- `LeftState` (Vector3): A public variable that represents the left state vector in 3D space. This vector is one of the inputs used for computing the Radon Transform.
  
- `RightState` (Vector3): A public variable that represents the right state vector in 3D space. This vector is the second input used for computing the Radon Transform.

## Functions
- `ComputeRadonTransform()`: This public method computes the Radon Transform by averaging the `LeftState` and `RightState` vectors. It logs the input states and the resulting Radon Transform to the console. The method returns a `Vector3` representing the computed Radon Transform.