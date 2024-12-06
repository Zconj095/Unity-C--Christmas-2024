using System;
using UnityEngine;

public class GatedRecurrentUnit : MonoBehaviour
{
    // Parameters (Weights and Biases)
    public Matrix W_f, U_f, W_h, U_h; // Weight matrices
    public Vector b_f, b_h;           // Bias vectors

    // States
    private Vector h_prev;            // Previous hidden state
    private Vector x_t;               // Input vector at time t

    // Initialize dimensions and example weights
    void Start()
    {
        int stateSize = 3;   // Size of the hidden state
        int inputSize = 3;   // Size of the input vector

        // Initialize weights, biases, and states (example values)
        W_f = Matrix.Random(stateSize, inputSize);
        U_f = Matrix.Random(stateSize, stateSize);
        W_h = Matrix.Random(stateSize, inputSize);
        U_h = Matrix.Random(stateSize, stateSize);

        b_f = Vector.Random(stateSize);
        b_h = Vector.Random(stateSize);

        h_prev = Vector.Zeros(stateSize);
        x_t = Vector.Random(inputSize);

        // Compute GRU update for one step
        Vector h_t = ComputeGRUStep(x_t, h_prev);

        // Log the result
        Debug.Log($"Updated Hidden State (h_t): {h_t}");
    }

    /// <summary>
    /// Computes a single GRU step based on the input and previous hidden state.
    /// </summary>
    Vector ComputeGRUStep(Vector x_t, Vector h_prev)
    {
        // Forget Gate: f_t = σ_g(W_f x_t + U_f h_{t-1} + b_f)
        Vector f_t = Sigmoid(W_f.Multiply(x_t) + U_f.Multiply(h_prev) + b_f);

        // Candidate State: ĥ_t = φ_h(W_h x_t + U_h (f_t ⊙ h_{t-1}) + b_h)
        Vector candidate_h = Tanh(W_h.Multiply(x_t) + U_h.Multiply(f_t.Hadamard(h_prev)) + b_h);

        // Final State: h_t = (1 - f_t) ⊙ h_{t-1} + f_t ⊙ ĥ_t
        Vector h_t = (Vector.Ones(f_t.Size) - f_t).Hadamard(h_prev) + f_t.Hadamard(candidate_h);

        return h_t;
    }

    /// <summary>
    /// Sigmoid activation function.
    /// </summary>
    Vector Sigmoid(Vector v)
    {
        Vector result = Vector.Zeros(v.Size);
        for (int i = 0; i < v.Size; i++)
        {
            result[i] = 1.0f / (1.0f + Mathf.Exp(-v[i]));
        }
        return result;
    }

    /// <summary>
    /// Tanh activation function.
    /// </summary>
    Vector Tanh(Vector v)
    {
        Vector result = Vector.Zeros(v.Size);
        for (int i = 0; i < v.Size; i++)
        {
            result[i] = (float)Math.Tanh(v[i]);
        }
        return result;
    }
}

/// <summary>
/// Helper class for vectors.
/// </summary>
public class Vector
{
    public float[] Values;
    public int Size => Values.Length;

    public Vector(int size)
    {
        Values = new float[size];
    }

    public static Vector Zeros(int size)
    {
        return new Vector(size);
    }

    public static Vector Ones(int size)
    {
        Vector v = new Vector(size);
        for (int i = 0; i < size; i++) v.Values[i] = 1.0f;
        return v;
    }

    public static Vector Random(int size)
    {
        Vector v = new Vector(size);
        for (int i = 0; i < size; i++) v.Values[i] = UnityEngine.Random.value;
        return v;
    }

    public float this[int i]
    {
        get => Values[i];
        set => Values[i] = value;
    }

    public Vector Hadamard(Vector other)
    {
        Vector result = new Vector(Size);
        for (int i = 0; i < Size; i++)
        {
            result[i] = this[i] * other[i];
        }
        return result;
    }

    public static Vector operator +(Vector a, Vector b)
    {
        if (a.Size != b.Size) throw new Exception("Vector size mismatch");
        Vector result = new Vector(a.Size);
        for (int i = 0; i < a.Size; i++) result[i] = a[i] + b[i];
        return result;
    }

    public static Vector operator -(Vector a, Vector b)
    {
        if (a.Size != b.Size) throw new Exception("Vector size mismatch");
        Vector result = new Vector(a.Size);
        for (int i = 0; i < a.Size; i++) result[i] = a[i] - b[i];
        return result;
    }
}

/// <summary>
/// Helper class for matrices.
/// </summary>
public class Matrix
{
    public float[,] Values;
    public int Rows => Values.GetLength(0);
    public int Columns => Values.GetLength(1);

    public Matrix(int rows, int columns)
    {
        Values = new float[rows, columns];
    }

    public static Matrix Random(int rows, int columns)
    {
        Matrix m = new Matrix(rows, columns);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                m.Values[i, j] = UnityEngine.Random.value;
            }
        }
        return m;
    }

    public Vector Multiply(Vector v)
    {
        if (Columns != v.Size) throw new Exception("Matrix-Vector size mismatch");

        Vector result = new Vector(Rows);
        for (int i = 0; i < Rows; i++)
        {
            float sum = 0f;
            for (int j = 0; j < Columns; j++)
            {
                sum += Values[i, j] * v[j];
            }
            result[i] = sum;
        }
        return result;
    }
}
