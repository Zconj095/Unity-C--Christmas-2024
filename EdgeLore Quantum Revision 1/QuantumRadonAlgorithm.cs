using UnityEngine;

public class QuantumRadonAlgorithm : MonoBehaviour
{
    public float[,] ApplyRadonTransform(float[,] densityMatrix, float angle)
    {
        // Rotate the density matrix (simplified)
        float cos = Mathf.Cos(angle);
        float sin = Mathf.Sin(angle);

        float[,] rotationMatrix = {
            { cos, -sin },
            { sin, cos }
        };

        float[,] transformedMatrix = MultiplyMatrices(densityMatrix, rotationMatrix);

        Debug.Log("Applied Quantum Radon Transform.");
        return transformedMatrix;
    }

    private float[,] MultiplyMatrices(float[,] mat1, float[,] mat2)
    {
        int rows = mat1.GetLength(0);
        int cols = mat2.GetLength(1);
        int sharedDim = mat1.GetLength(1);

        float[,] result = new float[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                for (int k = 0; k < sharedDim; k++)
                {
                    result[i, j] += mat1[i, k] * mat2[k, j];
                }
            }
        }

        return result;
    }
}
