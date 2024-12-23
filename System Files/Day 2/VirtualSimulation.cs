using System;
using MathNet.Numerics.LinearAlgebra;
using UnityEngine;

public class VirtualSimulation
{
    // Define the transition function
    public delegate Vector<double> TransitionFunction(Vector<double> currentState, Vector<double> inputs, Vector<double> actions);

    // Simulation State
    public Vector<double> CurrentState { get; private set; }

    public VirtualSimulation(Vector<double> initialState)
    {
        CurrentState = initialState;
    }

    public void UpdateState(Vector<double> inputs, Vector<double> actions, TransitionFunction transitionFunction)
    {
        if (transitionFunction == null)
            throw new ArgumentNullException(nameof(transitionFunction));

        // Update current state based on the transition function
        CurrentState = transitionFunction(CurrentState, inputs, actions);

        // Output updated state to the Unity Console
        Debug.Log("Updated State: " + CurrentState.ToString());
    }
}

// Example Usage
public class VirtualSimulationExample : MonoBehaviour
{
    private void Start()
    {
        // Initialize the state vector (example: [0, 0, 0])
        var initialState = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 0 });

        // Initialize the simulation
        var simulation = new VirtualSimulation(initialState);

        // Define the transition function (example: linear transition for demonstration)
        VirtualSimulation.TransitionFunction transitionFunction = (currentState, inputs, actions) =>
        {
            // Example: F(S_t, I_t, A_t) = S_t + I_t + A_t
            return currentState + inputs + actions;
        };

        // Inputs at time t (example: [1, 2, 3])
        var inputs = Vector<double>.Build.DenseOfArray(new double[] { 1, 2, 3 });

        // Actions at time t (example: [0.5, 0.5, 0.5])
        var actions = Vector<double>.Build.DenseOfArray(new double[] { 0.5, 0.5, 0.5 });

        // Run the simulation for a single step
        simulation.UpdateState(inputs, actions, transitionFunction);

        // Additional steps can be run by calling `UpdateState` with new inputs and actions.
    }
}
