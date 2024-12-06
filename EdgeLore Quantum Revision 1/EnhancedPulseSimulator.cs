using UnityEngine;

public class EnhancedPulseSimulator : QuantumSimulator
{
    private QuantumState state;

    public EnhancedPulseSimulator(QuantumCircuit circuit, QuantumVisualizer visualizer)
        : base(circuit, visualizer)
    {
        if (circuit == null || visualizer == null)
        {
            Debug.LogError("EnhancedPulseSimulator: Null arguments passed.");
            throw new System.ArgumentNullException("Null argument in EnhancedPulseSimulator");
        }

        state = new QuantumState(circuit.NumQubits);
        Debug.Log($"EnhancedPulseSimulator initialized with {circuit.NumQubits} qubits.");
    }

    public void ApplyPulse(double amplitude, double frequency, double duration)
    {
        Debug.Log($"Applying Pulse: Amplitude={amplitude}, Frequency={frequency}, Duration={duration}");
        Visualizer.VisualizePulse(amplitude, frequency, duration); // Use protected property

        // Modify the quantum state with a rotation and phase shift
        double rotationAngle = amplitude * duration;
        double phaseShift = frequency * duration;

        ApplyRotationAndPhase(rotationAngle, phaseShift);
    }

    private void ApplyRotationAndPhase(double rotationAngle, double phaseShift)
    {
        if (state == null || state.StateVector == null)
        {
            Debug.LogError("EnhancedPulseSimulator: StateVector is null. Cannot apply rotation and phase.");
            return;
        }

        for (int i = 0; i < state.StateVector.Length; i++)
        {
            Complex currentState = state.StateVector[i];

            // Apply rotation: update amplitude
            double cosRotation = Mathf.Cos((float)rotationAngle);
            double sinRotation = Mathf.Sin((float)rotationAngle);

            double newReal = currentState.Real * cosRotation - currentState.Imaginary * sinRotation;
            double newImaginary = currentState.Real * sinRotation + currentState.Imaginary * cosRotation;

            // Apply phase shift
            double cosPhase = Mathf.Cos((float)(phaseShift * Mathf.PI * 2));
            double sinPhase = Mathf.Sin((float)(phaseShift * Mathf.PI * 2));
            Complex phaseFactor = new Complex(cosPhase, sinPhase);

            Complex newState = new Complex(
                newReal * phaseFactor.Real - newImaginary * phaseFactor.Imaginary,
                newReal * phaseFactor.Imaginary + newImaginary * phaseFactor.Real
            );

            // Update state
            state.StateVector[i] = newState;
        }
    }
}
