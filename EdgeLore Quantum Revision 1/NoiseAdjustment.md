# NoiseAdjustment

## Overview
The `NoiseAdjustment` script is designed to manage the adjustment of noise parameters in a quantum simulation environment within a Unity application. It provides a user interface through two sliders: one for adjusting the depolarizing noise and another for adjusting the amplitude damping noise. The script listens for changes to the slider values and logs the updated probabilities, which can then be used to influence the behavior of a `QuantumSimulator` object.

## Variables

- **DepolarizingNoiseSlider**: A `Slider` UI component that allows the user to adjust the probability of depolarizing noise in the quantum simulation.
  
- **AmplitudeDampingNoiseSlider**: A `Slider` UI component that allows the user to adjust the probability of amplitude damping noise in the quantum simulation.
  
- **simulator**: A reference to the `QuantumSimulator` object that is responsible for handling the quantum simulation logic. This is initialized in the `Start` method.

## Functions

- **Start()**: This method is called when the script is first run. It initializes the `simulator` variable by finding the `QuantumSimulator` object in the scene. Additionally, it sets up listeners for the slider components, so that changes in their values trigger the corresponding update functions.

- **UpdateDepolarizingNoise(float value)**: This method is called whenever the value of the `DepolarizingNoiseSlider` changes. It logs the updated probability of depolarizing noise to the console.

- **UpdateAmplitudeDampingNoise(float value)**: This method is called whenever the value of the `AmplitudeDampingNoiseSlider` changes. It logs the updated probability of amplitude damping noise to the console.