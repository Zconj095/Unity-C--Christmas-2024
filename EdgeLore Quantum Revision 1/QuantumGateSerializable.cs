[System.Serializable]
public class QuantumGateSerializable
{
    public string Name;
    public int NumQubits;
    public int[] Qubits;

    public QuantumGateSerializable(QuantumGate gate)
    {
        Name = gate.Name;
        NumQubits = gate.NumQubits;
        Qubits = gate.Qubits;
    }

    public QuantumGate ToQuantumGate()
    {
        // Convert back to QuantumGate. You'll need to match the operation manually based on Name.
        // This is a placeholder and should be extended for all gate types.
        return new QuantumGate(Name, NumQubits, Qubits, state => state); 
    }
}
