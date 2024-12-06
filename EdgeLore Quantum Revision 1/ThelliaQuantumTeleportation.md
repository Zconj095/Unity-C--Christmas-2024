# ThelliaQuantumTeleportation

## Overview
The `ThelliaQuantumTeleportation` script is designed to handle the teleportation of a quantum state within a Unity game environment. This script allows an object to teleport its current quantum state to a designated hyperstate without any alteration. It serves as a component that can be attached to game objects that require teleportation functionality, ensuring that their quantum states can be managed effectively.

## Variables

- `QuantumState` (Vector3): This public variable holds the current quantum state of the object, represented as a three-dimensional vector. It defines the position in the quantum space from which the object will teleport.

## Functions

- `TeleportToHyperstate()` (Vector3): This public method is responsible for performing the teleportation of the object's quantum state. It logs a message indicating the current quantum state being teleported and returns the `QuantumState` without any modification. This function can be called to execute the teleportation process in the game.