using UnityEngine;
public static class QuantumGates
{
    // Hadamard Gate
    public static Qubit ApplyHadamard(Qubit qubit)
    {
        float newAmp0 = (qubit.Amplitude0 + qubit.Amplitude1) / Mathf.Sqrt(2);
        float newAmp1 = (qubit.Amplitude0 - qubit.Amplitude1) / Mathf.Sqrt(2);
        return new Qubit(newAmp0, newAmp1);
    }

    // Toffoli Gate (Simplified for this context)
    public static Qubit ApplyToffoli(Qubit control1, Qubit control2, Qubit target)
    {
        // Only flip the target if both controls are |1⟩
        if (control1.Amplitude1 > 0.5f && control2.Amplitude1 > 0.5f)
        {
            return new Qubit(target.Amplitude1, target.Amplitude0);
        }
        return target;
    }

    // Simulate Quantum Teleportation
    public static Qubit QuantumTeleport(Qubit qubit)
    {
        // Teleportation here is just a passthrough for simplicity
        return new Qubit(qubit.Amplitude0, qubit.Amplitude1);
    }
}
