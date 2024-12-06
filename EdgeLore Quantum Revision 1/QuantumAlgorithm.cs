public abstract class QuantumAlgorithm
{
    public string Name { get; private set; }

    protected QuantumAlgorithm(string name)
    {
        Name = name;
    }

    public abstract void Execute(QuantumCircuit circuit, QuantumSimulator simulator);
}
