# AerSimulatorExample

## Overview
The `AerSimulatorExample` script is designed to demonstrate the usage of the `AerManager` component within a Unity environment. It initializes the `AerManager`, defines quantum circuits, adds jobs to the manager, and executes those jobs asynchronously. This script serves as an example of how to set up and utilize quantum job management in a simulation context, fitting into a larger codebase that likely involves quantum computing simulations.

## Variables
- `aerManager`: An instance of the `AerManager` class, responsible for managing and executing quantum jobs. It is initialized in the `Start` method, either by retrieving an existing component or by creating a new one if none exists.

## Functions
- `Start()`: This Unity lifecycle method is called before the first frame update. It initializes the `aerManager`, defines two quantum circuits (`circuit1` and `circuit2`), adds these circuits as jobs to the `aerManager`, and triggers the execution of all jobs.

- `ExecuteAllJobs()`: This private asynchronous method calls the `ExecuteJobs` method of the `aerManager`. It ensures that all jobs added to the manager are executed in a non-blocking manner, allowing the Unity application to remain responsive during job execution.