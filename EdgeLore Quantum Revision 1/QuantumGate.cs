using System;
using System.Numerics;

public class QuantumGate
{
    public string Name { get; private set; }
    public int NumQubits { get; private set; }
    public int[] Qubits { get; private set; }
    public Func<Complex[], Complex[]> Operation { get; private set; }

    public QuantumGate(string name, int numQubits, int[] qubits, Func<Complex[], Complex[]> operation)
    {
        Name = name;
        NumQubits = numQubits;
        Qubits = qubits;
        Operation = operation;
    }
    public static QuantumGate Rotation(int qubit, double angle, string axis = "Z")
    {
        return new QuantumGate($"R{axis}({angle})", 1, new[] { qubit }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            int dim = state.Length;
            Complex rotationFactor = new Complex(Math.Cos(angle), Math.Sin(angle)); // General rotation factor

            switch (axis.ToUpper())
            {
                case "X":
                    // Apply RX rotation
                    for (int i = 0; i < dim; i++)
                    {
                        int targetBit = (i >> qubit) & 1;
                        int flippedIndex = i ^ (1 << qubit);
                        double cos = Math.Cos(angle / 2);
                        double sin = Math.Sin(angle / 2);

                        // Update newState[i] and newState[flippedIndex]
                        newState[i] = new Complex(
                            cos * state[i].Real - sin * state[flippedIndex].Imaginary,
                            cos * state[i].Imaginary + sin * state[flippedIndex].Real
                        );
                        newState[flippedIndex] = new Complex(
                            cos * state[flippedIndex].Real + sin * state[i].Imaginary,
                            cos * state[flippedIndex].Imaginary - sin * state[i].Real
                        );
                    }
                    break;

                case "Y":
                    // Apply RY rotation
                    for (int i = 0; i < dim; i++)
                    {
                        int targetBit = (i >> qubit) & 1;
                        int flippedIndex = i ^ (1 << qubit);
                        double cos = Math.Cos(angle / 2);
                        double sin = Math.Sin(angle / 2);

                        // Update based on the RY transformation
                        newState[i] = targetBit == 0
                            ? new Complex(cos * state[i].Real + sin * state[flippedIndex].Real, cos * state[i].Imaginary + sin * state[flippedIndex].Imaginary)
                            : new Complex(-sin * state[i].Real + cos * state[flippedIndex].Real, -sin * state[i].Imaginary + cos * state[flippedIndex].Imaginary);
                    }
                    break;

                case "Z":
                    // Apply RZ rotation
                    for (int i = 0; i < dim; i++)
                    {
                        int targetBit = (i >> qubit) & 1;
                        Complex phaseFactor = targetBit == 0 ? Complex.One : rotationFactor;
                        newState[i] = state[i] * phaseFactor;
                    }
                    break;

                default:
                    UnityEngine.Debug.LogWarning($"Unknown axis: {axis}. Defaulting to Z-axis rotation.");
                    for (int i = 0; i < dim; i++)
                    {
                        int targetBit = (i >> qubit) & 1;
                        Complex phaseFactor = targetBit == 0 ? Complex.One : rotationFactor;
                        newState[i] = state[i] * phaseFactor;
                    }
                    break;
            }

            return newState;
        });
    }

    public static QuantumGate Hadamard(int qubit)
    {
        return new QuantumGate("Hadamard", 1, new int[] { qubit }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            int halfSize = state.Length / 2;
            for (int i = 0; i < halfSize; i++)
            {
                newState[i] = new Complex(state[i].Real / Math.Sqrt(2), state[i].Imaginary / Math.Sqrt(2)) +
                              new Complex(state[i + halfSize].Real / Math.Sqrt(2), state[i + halfSize].Imaginary / Math.Sqrt(2));
                newState[i + halfSize] = new Complex(state[i].Real / Math.Sqrt(2), state[i].Imaginary / Math.Sqrt(2)) -
                                         new Complex(state[i + halfSize].Real / Math.Sqrt(2), state[i + halfSize].Imaginary / Math.Sqrt(2));
            }
            return newState;
        });
    }

    public static QuantumGate PauliZ(int qubit)
    {
        return new QuantumGate("PauliZ", 1, new[] { qubit }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                if (((i >> qubit) & 1) == 1)
                {
                    newState[i] = new Complex(-state[i].Real, -state[i].Imaginary);
                }
            }
            return newState;
        });
    }

    public static QuantumGate Phase(int qubit, double angle)
    {
        return new QuantumGate($"Phase({angle})", 1, new[] { qubit }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                if (((i >> qubit) & 1) == 1)
                {
                    double phaseReal = Math.Cos(angle);
                    double phaseImaginary = Math.Sin(angle);
                    Complex phaseFactor = new Complex(phaseReal, phaseImaginary);
                    newState[i] *= phaseFactor;
                }
            }
            return newState;
        });
    }

    public static QuantumGate CNOT(int control, int target)
    {
        return new QuantumGate("CNOT", 2, new[] { control, target }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                if (((i >> control) & 1) == 1)
                {
                    int flippedIndex = i ^ (1 << target);
                    newState[flippedIndex] = state[i];
                    newState[i] = state[flippedIndex];
                }
            }
            return newState;
        });
    }

    public static QuantumGate PauliX(int qubit)
    {
        return new QuantumGate("PauliX", 1, new[] { qubit }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                int flippedIndex = i ^ (1 << qubit);
                newState[flippedIndex] = state[i];
            }
            return newState;
        });
    }

    public static QuantumGate PauliY(int qubit)
    {
        return new QuantumGate("PauliY", 1, new[] { qubit }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                int flippedIndex = i ^ (1 << qubit);
                newState[flippedIndex] = new Complex(-state[i].Imaginary, state[i].Real);
            }
            return newState;
        });
    }

    public static QuantumGate RX(int qubit, double angle)
    {
        return new QuantumGate($"RX({angle})", 1, new[] { qubit }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                int targetBit = (i >> qubit) & 1;
                int flippedIndex = i ^ (1 << qubit);
                double cos = Math.Cos(angle / 2);
                double sin = Math.Sin(angle / 2);

                // RX Transformation
                newState[i] = new Complex(
                    cos * state[i].Real - sin * state[flippedIndex].Imaginary,
                    cos * state[i].Imaginary + sin * state[flippedIndex].Real
                );
                newState[flippedIndex] = new Complex(
                    cos * state[flippedIndex].Real + sin * state[i].Imaginary,
                    cos * state[flippedIndex].Imaginary - sin * state[i].Real
                );
            }
            return newState;
        });
    }

    public static QuantumGate RY(int qubit, double angle)
    {
        return new QuantumGate($"RY({angle})", 1, new[] { qubit }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                int targetBit = (i >> qubit) & 1;
                int flippedIndex = i ^ (1 << qubit);
                double cos = Math.Cos(angle / 2);
                double sin = Math.Sin(angle / 2);

                // RY Transformation
                newState[i] = targetBit == 0
                    ? new Complex(cos * state[i].Real + sin * state[flippedIndex].Real, cos * state[i].Imaginary + sin * state[flippedIndex].Imaginary)
                    : new Complex(-sin * state[i].Real + cos * state[flippedIndex].Real, -sin * state[i].Imaginary + cos * state[flippedIndex].Imaginary);
            }
            return newState;
        });
    }

    public static QuantumGate RZ(int qubit, double angle)
    {
        return new QuantumGate($"RZ({angle})", 1, new[] { qubit }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                int targetBit = (i >> qubit) & 1;
                double phaseReal = Math.Cos(angle / 2);
                double phaseImaginary = Math.Sin(angle / 2);
                Complex phaseFactor = targetBit == 0 ? new Complex(1, 0) : new Complex(phaseReal, -phaseImaginary);
                newState[i] = state[i] * phaseFactor;
            }
            return newState;
        });
    }

    public static QuantumGate SWAP(int qubit1, int qubit2)
    {
        return new QuantumGate("SWAP", 2, new[] { qubit1, qubit2 }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                int swappedIndex = i ^ ((1 << qubit1) | (1 << qubit2));
                newState[swappedIndex] = state[i];
            }
            return newState;
        });
    }

    public static QuantumGate Toffoli(int control1, int control2, int target)
    {
        return new QuantumGate("Toffoli", 3, new[] { control1, control2, target }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                int controlBits = ((i >> control1) & 1) & ((i >> control2) & 1);
                if (controlBits == 1)
                {
                    int flippedIndex = i ^ (1 << target);
                    newState[flippedIndex] = state[i];
                    newState[i] = state[flippedIndex];
                }
            }
            return newState;
        });
    }

    public static QuantumGate Fredkin(int control, int swap1, int swap2)
    {
        return new QuantumGate("Fredkin", 3, new[] { control, swap1, swap2 }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                if (((i >> control) & 1) == 1)
                {
                    int swappedIndex = i ^ ((1 << swap1) | (1 << swap2));
                    newState[swappedIndex] = state[i];
                    newState[i] = state[swappedIndex];
                }
            }
            return newState;
        });
    }


    // Other gate implementations (e.g., CNOT, RX, RY, RZ, SWAP) remain the same.
}
