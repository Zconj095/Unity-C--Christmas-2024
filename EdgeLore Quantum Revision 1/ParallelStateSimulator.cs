using System.Threading.Tasks;

public class ParallelStateSimulator : QuantumSimulator
{
    public ParallelStateSimulator(QuantumCircuit circuit, QuantumVisualizer visualizer)
        : base(circuit, visualizer) { }

    public virtual void Simulate()
    {
        Parallel.ForEach(Circuit.Gates, gate =>
        {
            State.ApplyGate(gate); // Directly access the protected state field
        });

        UnityEngine.Debug.Log("Parallel simulation complete.");
    }
}
