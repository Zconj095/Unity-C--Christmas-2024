# AerManager

## Overview
The `AerManager` class is responsible for managing and executing jobs related to quantum circuit simulations using the `AerSimulator`. It maintains a queue of jobs, adds new jobs to the queue, and processes them sequentially. This class acts as an intermediary between the job submission and the simulator execution, ensuring that jobs are handled efficiently and results are displayed appropriately.

## Variables

- `simulator`: An instance of the `AerSimulator` class, which performs the actual simulation of quantum circuits.
- `jobQueue`: A list that stores instances of `AerJob`. Each job represents a quantum circuit to be simulated, along with its associated parameters.

## Functions

- `void Start()`: This Unity lifecycle method is called when the script instance is being loaded. It initializes the `simulator` by attempting to retrieve an existing `AerSimulator` component from the GameObject. If none exists, it adds a new `AerSimulator` component. A log message confirms the initialization.

- `public void AddJob(string jobId, List<string> circuit, int numQubits)`: This method allows the addition of a new job to the `jobQueue`. It creates a new `AerJob` instance using the provided job ID, quantum circuit, and number of qubits, and then adds it to the queue. A log message indicates that the job has been added.

- `public async Task ExecuteJobs()`: This asynchronous method processes each job in the `jobQueue`. It awaits the execution of each job using the `simulator` and calls `DisplayJobResult` to show the results of each job. After all jobs have been executed, it clears the `jobQueue`.

- `private void DisplayJobResult(AerJob job)`: This method is responsible for displaying the results of a completed job. It checks if the job's result is null and logs an error if it is. Otherwise, it logs the results, showing each state and its associated probability.