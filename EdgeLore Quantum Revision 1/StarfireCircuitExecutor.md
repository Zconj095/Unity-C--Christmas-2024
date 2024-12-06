# StarfireCircuitExecutor

## Overview
The `StarfireCircuitExecutor` class is responsible for executing quantum circuits within a Unity environment. It integrates with a visualizer, `BlochSphereVisualizer`, to provide a graphical representation of the quantum states as circuits are executed. This class is designed to manage multiple quantum circuit executions, including Fuzed Boltzmann-Grover, Fusion State, Chaos State, and Quantum Fourier Transform (QFT) circuits. It helps visualize the evolution of quantum states while performing complex quantum computations.

## Variables
- `numQubits`: An integer that represents the number of qubits to be used in the quantum circuits. Default value is 5.
- `markedState`: An integer that indicates the specific state to be marked in the Fuzed Boltzmann-Grover circuit. Default value is 2.
- `boltzmannFactor`: A float that determines the Boltzmann factor used in the Fuzed Boltzmann-Grover circuit. Default value is 0.5f.
- `hardwareTarget`: A string that specifies the target quantum hardware for transpilation. Default value is "IBM-Q".
- `blochSphereVisualizer`: A reference to the `BlochSphereVisualizer` component that is used to visualize the quantum states.

## Functions
- `void Start()`: Initializes the `blochSphereVisualizer` component. If it is not found, the function adds a new instance to the game object. It also sets the number of qubits in the visualizer and calls `ExecuteCircuitsWithVisualization()` to start executing the circuits.

- `public void ExecuteAdvancedCircuits()`: Executes a series of advanced quantum circuits, including Fuzed Boltzmann-Grover, Quantum Fusion State, Quantum Barebones Kernel, Quantum Hypervector, and Quantum Qubit Transpiler circuits. Each circuit is instantiated and executed with the relevant parameters.

- `public void ExecuteCircuitsWithVisualization()`: Initializes the quantum states and visualizes them through the `blochSphereVisualizer`. It sequentially applies circuit transformations for the Fuzed Boltzmann-Grover, Fusion State, and Chaos State circuits, while logging the execution progress.

- `private Vector3[] ApplyCircuitTransform(Vector3[] states, string circuitType)`: Applies a transformation to the given quantum states based on the circuit type. This function scales down the x, y, and z components of each state by 10% and returns the modified states.

- `private Vector3[] InitializeStates(int qubits)`: Initializes an array of `Vector3` states for the specified number of qubits. Each state is initialized to the vector (0, 0, 1), representing a specific quantum state in the Bloch sphere.

- `public void ExecuteCircuitsWithQFT()`: Logs the commencement of QFT circuit execution, calls `ExecuteAdvancedCircuits()`, and applies the Quantum Fourier Transform circuit to the qubits using the `QuantumFourierTransformCircuit` component.