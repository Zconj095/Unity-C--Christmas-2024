using System;
using MathNet.Numerics.LinearAlgebra; // For Matrix operations
using MathNet.Numerics.LinearAlgebra.Double; // For creating double matrices
using UnityEngine;

public class GeneticInformationProcessor
{
    private int matchScore; // Bonus for matching bases
    private int gapPenalty; // Penalty for gaps in sequence

    public GeneticInformationProcessor(int matchScore, int gapPenalty)
    {
        this.matchScore = matchScore;
        this.gapPenalty = gapPenalty;
    }

    /// <summary>
    /// Aligns two biological sequences using dynamic programming.
    /// </summary>
    /// <param name="sequenceA">The first sequence (string).</param>
    /// <param name="sequenceB">The second sequence (string).</param>
    /// <returns>Alignment score matrix.</returns>
    public Matrix<double> AlignSequences(string sequenceA, string sequenceB)
    {
        int m = sequenceA.Length;
        int n = sequenceB.Length;

        // Create a score matrix
        var scoreMatrix = DenseMatrix.Build.Dense(m + 1, n + 1);

        // Initialize the matrix with gap penalties
        for (int i = 0; i <= m; i++)
        {
            scoreMatrix[i, 0] = i * -gapPenalty;
        }
        for (int j = 0; j <= n; j++)
        {
            scoreMatrix[0, j] = j * -gapPenalty;
        }

        // Fill the matrix using the recurrence relation
        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                double match = scoreMatrix[i - 1, j - 1] +
                              (sequenceA[i - 1] == sequenceB[j - 1] ? matchScore : -gapPenalty);

                double delete = scoreMatrix[i - 1, j] - gapPenalty;
                double insert = scoreMatrix[i, j - 1] - gapPenalty;

                scoreMatrix[i, j] = Math.Max(match, Math.Max(delete, insert));
            }
        }

        return scoreMatrix;
    }

    /// <summary>
    /// Prints the alignment score matrix to the Unity Console.
    /// </summary>
    /// <param name="scoreMatrix">Matrix containing alignment scores.</param>
    public void PrintMatrix(Matrix<double> scoreMatrix)
    {
        Debug.Log("Alignment Score Matrix:");
        for (int i = 0; i < scoreMatrix.RowCount; i++)
        {
            string row = "";
            for (int j = 0; j < scoreMatrix.ColumnCount; j++)
            {
                row += $"{scoreMatrix[i, j]}\t";
            }
            Debug.Log(row);
        }
    }
}

// Unity Example
public class GeneticInformationProcessorExample : MonoBehaviour
{
    private void Start()
    {
        // Example sequences
        string sequenceA = "AGCT";
        string sequenceB = "GCTA";

        // Initialize the processor with scoring parameters
        int matchScore = 2; // Example: +2 for matches
        int gapPenalty = 1; // Example: -1 for gaps
        var processor = new GeneticInformationProcessor(matchScore, gapPenalty);

        // Perform sequence alignment
        var scoreMatrix = processor.AlignSequences(sequenceA, sequenceB);

        // Print the alignment matrix
        processor.PrintMatrix(scoreMatrix);
    }
}
