using UnityEngine;

public class QuantumPhaseEstimationGate : QuantumGate
{
    private int numControlQubits;
    private int targetQubit;
    private float phase;

    public QuantumPhaseEstimationGate(int numControlQubits, int targetQubit, float phase)
        : base("Quantum Phase Estimation Gate", numControlQubits + 1, new[] { targetQubit }, null) // Proper constructor call
    {
        this.numControlQubits = numControlQubits;
        this.targetQubit = targetQubit;
        this.phase = phase;
    }

    // Apply method to simulate the behavior of the gate
    public float[] Apply(float[] stateVector)
    {
        Debug.Log($"Applying {Name} with {numControlQubits} control qubits, target qubit {targetQubit}, and phase {phase}");

        int stateSize = stateVector.Length / 2; // Half size since real and imaginary parts are interleaved
        float[] newStateVector = new float[stateVector.Length]; // Array for the updated state vector

        for (int i = 0; i < stateSize; i++)
        {
            int realIndex = i * 2;
            int imagIndex = i * 2 + 1;

            if (IsQubitActive(i, targetQubit))
            {
                // Extract real and imaginary parts
                float realPart = stateVector[realIndex];
                float imagPart = stateVector[imagIndex];

                // Check if control qubits are active
                float rotation = IsControlQubitsActive(i) ? phase : 0;

                // Compute phase rotation: exp(i * theta) = cos(theta) + i * sin(theta)
                float cosTheta = Mathf.Cos(rotation);
                float sinTheta = Mathf.Sin(rotation);

                // Apply the rotation
                newStateVector[realIndex] = realPart * cosTheta - imagPart * sinTheta; // Real part
                newStateVector[imagIndex] = realPart * sinTheta + imagPart * cosTheta; // Imaginary part
            }
            else
            {
                // Copy state unchanged for qubits not affected
                newStateVector[realIndex] = stateVector[realIndex];
                newStateVector[imagIndex] = stateVector[imagIndex];
            }
        }

        return newStateVector;
    }

    private bool IsQubitActive(int index, int qubit)
    {
        return (index & (1 << qubit)) != 0; // Check if the target qubit is active in this state index
    }

    private bool IsControlQubitsActive(int index)
    {
        for (int i = 0; i < numControlQubits; i++)
        {
            if ((index & (1 << i)) == 0) // If any control qubit is inactive
                return false;
        }
        return true; // All control qubits are active
    }

    public override string ToString()
    {
        return $"{Name}: Control Qubits={numControlQubits}, Target Qubit={targetQubit}, Phase={phase}";
    }
}
