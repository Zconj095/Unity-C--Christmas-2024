using UnityEngine;
public class GateValidator
{
    public static bool IsValidGate(QuantumCircuit circuit, string gateType, int targetQubit, int controlQubit = -1)
    {
        if (targetQubit < 0 || targetQubit >= circuit.NumQubits)
        {
            UnityEngine.Debug.LogWarning("Invalid target qubit.");
            return false;
        }

        if (gateType == "CNOT" && (controlQubit < 0 || controlQubit >= circuit.NumQubits || controlQubit == targetQubit))
        {
            UnityEngine.Debug.LogWarning("Invalid control qubit for CNOT gate.");
            return false;
        }

        // Add other gate-specific validations here

        return true;
    }
}
