# QuantumBayesianLearning

## Overview
The `QuantumBayesianLearning` script is designed to implement a quantum Bayesian learning algorithm within a Unity environment. It simulates the process of learning from observations by updating the probabilities of various hypotheses based on simulated data. This script is particularly useful for visualizing the dynamics of hypothesis probabilities as they evolve over iterations. The script integrates with Unity's MonoBehaviour, allowing for seamless interaction with the Unity game engine and its components.

## Variables

- **numHypotheses**: An integer representing the total number of hypotheses to consider during the learning process. Default is set to 8.
- **initialAmplitude**: A float that specifies the initial amplitude for each hypothesis, influencing their initial probability. Default is 0.1f.
- **learningRate**: A float that defines the initial learning rate, which affects how quickly probabilities are updated. Default is 0.05f.
- **numIterations**: An integer that indicates the number of iterations the learning process will run. Default is set to 10.
- **logUpdates**: A boolean that determines whether to log updates to the console for debugging purposes. Default is true.
- **hypothesisBarPrefab**: A GameObject that serves as a prefab for visualizing each hypothesis as a bar in the Unity scene.
- **visualizationContainer**: A Transform that acts as the parent for the hypothesis bars, organizing their placement in the scene.
- **amplitudes**: An array of floats that holds the amplitude values for each hypothesis, which are used to calculate their probabilities.
- **probabilities**: An array of floats that stores the calculated probabilities for each hypothesis based on their amplitudes.
- **hypothesisBars**: A list of GameObjects that represents the visual bars corresponding to each hypothesis in the Unity scene.

## Functions

- **RunQuantumBayesianLearning()**: The main function that orchestrates the quantum Bayesian learning process. It initializes hypotheses, simulates observations, updates probabilities, adjusts the learning rate, and updates the visualization over a defined number of iterations.

- **InitializeHypotheses()**: Initializes the amplitudes and probabilities for each hypothesis, setting their initial values and preparing the visualization components.

- **SimulateObservation()**: Simulates the observation of a hypothesis based on the current probabilities. It returns the index of the observed hypothesis.

- **UpdateProbabilities(int observedHypothesis)**: Updates the amplitudes and probabilities of the hypotheses based on the observed hypothesis. It increases the amplitude of the observed hypothesis and decreases the others, ensuring that amplitudes remain within valid bounds.

- **AdjustLearningRate()**: Dynamically adjusts the learning rate based on the entropy of the current probabilities. A lower entropy indicates greater confidence, leading to a smaller learning rate.

- **GetMostProbableHypothesis()**: Determines and returns the index of the hypothesis with the highest probability.

- **LogProbabilities(string message)**: Logs the current probabilities and amplitudes of all hypotheses to the console, prefixed with a custom message for clarity.

- **InitializeVisualization()**: Sets up the visual representation of hypotheses by instantiating bars in the Unity scene. It ensures that previous visualizations are cleared before creating new ones.

- **UpdateVisualization()**: Updates the scale of each hypothesis bar in the visualization based on the current probabilities, allowing for real-time feedback on the learning process.