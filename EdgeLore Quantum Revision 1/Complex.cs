using System;

public struct Complex
{
    public double Real { get; set; }
    public double Imaginary { get; set; }

    public Complex(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    public double Magnitude => Math.Sqrt(Real * Real + Imaginary * Imaginary);

    public double Phase => Math.Atan2(Imaginary, Real);

    public static readonly Complex Zero = new Complex(0, 0);
    public static readonly Complex One = new Complex(1, 0);
    public static readonly Complex ImaginaryOne = new Complex(0, 1);

    // Addition
    public static Complex operator +(Complex c1, Complex c2)
    {
        return new Complex(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
    }

    // Subtraction
    public static Complex operator -(Complex c1, Complex c2)
    {
        return new Complex(c1.Real - c2.Real, c1.Imaginary - c2.Imaginary);
    }

    // Multiplication
    public static Complex operator *(Complex c1, Complex c2)
    {
        return new Complex(
            c1.Real * c2.Real - c1.Imaginary * c2.Imaginary,
            c1.Real * c2.Imaginary + c1.Imaginary * c2.Real
        );
    }

    // Division
    public static Complex operator /(Complex c1, Complex c2)
    {
        double denominator = c2.Real * c2.Real + c2.Imaginary * c2.Imaginary;
        return new Complex(
            (c1.Real * c2.Real + c1.Imaginary * c2.Imaginary) / denominator,
            (c1.Imaginary * c2.Real - c1.Real * c2.Imaginary) / denominator
        );
    }

    // Negation
    public static Complex operator -(Complex c)
    {
        return new Complex(-c.Real, -c.Imaginary);
    }

    // Complex Conjugate
    public Complex Conjugate()
    {
        return new Complex(Real, -Imaginary);
    }

    // Exponentiation (Complex to the Power of Real)
    public Complex Pow(double exponent)
    {
        double magnitude = Math.Pow(Magnitude, exponent);
        double phase = Phase * exponent;
        return FromPolarCoordinates(magnitude, phase);
    }

    // Natural Exponential
    public Complex Exp()
    {
        double expReal = Math.Exp(Real);
        return new Complex(
            expReal * Math.Cos(Imaginary),
            expReal * Math.Sin(Imaginary)
        );
    }

    // Natural Logarithm
    public Complex Log()
    {
        return new Complex(Math.Log(Magnitude), Phase);
    }

    // Trigonometric Functions
    public static Complex Sin(Complex c)
    {
        return new Complex(
            Math.Sin(c.Real) * Math.Cosh(c.Imaginary),
            Math.Cos(c.Real) * Math.Sinh(c.Imaginary)
        );
    }

    public static Complex Cos(Complex c)
    {
        return new Complex(
            Math.Cos(c.Real) * Math.Cosh(c.Imaginary),
            -Math.Sin(c.Real) * Math.Sinh(c.Imaginary)
        );
    }

    public static Complex Tan(Complex c)
    {
        return Sin(c) / Cos(c);
    }

    // Polar Coordinates
    public static Complex FromPolarCoordinates(double magnitude, double phase)
    {
        return new Complex(
            magnitude * Math.Cos(phase),
            magnitude * Math.Sin(phase)
        );
    }

    public (double Magnitude, double Phase) ToPolarCoordinates()
    {
        return (Magnitude, Phase);
    }

    // Tensor Product (Kronecker Product for Quantum Systems)
    public static Complex[,] TensorProduct(Complex[,] a, Complex[,] b)
    {
        int rowsA = a.GetLength(0);
        int colsA = a.GetLength(1);
        int rowsB = b.GetLength(0);
        int colsB = b.GetLength(1);

        Complex[,] result = new Complex[rowsA * rowsB, colsA * colsB];

        for (int i = 0; i < rowsA; i++)
        {
            for (int j = 0; j < colsA; j++)
            {
                for (int k = 0; k < rowsB; k++)
                {
                    for (int l = 0; l < colsB; l++)
                    {
                        result[i * rowsB + k, j * colsB + l] = a[i, j] * b[k, l];
                    }
                }
            }
        }

        return result;
    }

    // Inner Product
    public static Complex InnerProduct(Complex[] vectorA, Complex[] vectorB)
    {
        if (vectorA.Length != vectorB.Length)
            throw new ArgumentException("Vectors must be the same length.");

        Complex result = Zero;
        for (int i = 0; i < vectorA.Length; i++)
        {
            result += vectorA[i].Conjugate() * vectorB[i];
        }
        return result;
    }

    // Hermitian Conjugate
    public static Complex[,] HermitianConjugate(Complex[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        Complex[,] result = new Complex[cols, rows];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[j, i] = matrix[i, j].Conjugate();
            }
        }
        return result;
    }

    // Overridden ToString
    public override string ToString()
    {
        if (Imaginary >= 0)
        {
            return $"{Real} + {Imaginary}i";
        }
        else
        {
            return $"{Real} - {-Imaginary}i";
        }
    }

    // Equals and HashCode for Precision Comparison
    public override bool Equals(object obj)
    {
        if (obj is Complex other)
        {
            return Math.Abs(Real - other.Real) < 1e-10 && Math.Abs(Imaginary - other.Imaginary) < 1e-10;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Real.GetHashCode() ^ Imaginary.GetHashCode();
    }
}
