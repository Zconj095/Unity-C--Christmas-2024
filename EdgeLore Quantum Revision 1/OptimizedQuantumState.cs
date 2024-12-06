using System;
using System.Threading.Tasks;

public class OptimizedQuantumState
{
    public Complex[] StateVector { get; private set; }

    public OptimizedQuantumState(int numQubits)
    {
        int stateSize = (int)Math.Pow(2, numQubits);
        StateVector = new Complex[stateSize];
        StateVector[0] = new Complex(1, 0); // Start in |0...0⟩ state
    }

    public void ApplyGate(QuantumGate gate)
    {
        if (gate.Operation != null)
        {
            StateVector = ParallelGateOperation(gate.Operation, StateVector);
        }
        else
        {
            Console.WriteLine($"Operation for {gate.Name} gate is not defined.");
        }
    }

    private Complex[] ParallelGateOperation(Func<Complex[], Complex[]> operation, Complex[] inputState)
    {
        Complex[] outputState = new Complex[inputState.Length];

        Parallel.For(0, inputState.Length, i =>
        {
            outputState[i] = operation(inputState)[i];
        });

        return outputState;
    }
}
