using System;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;

public class AtomicLatticeVisualization : MonoBehaviour
{
    [Header("Basis Vectors")]
    [SerializeField] private Vector3 a1 = new Vector3(1, 0, 0); // Basis vector 1
    [SerializeField] private Vector3 a2 = new Vector3(0, 1, 0); // Basis vector 2
    [SerializeField] private Vector3 a3 = new Vector3(0, 0, 1); // Basis vector 3

    [Header("Lattice Range")]
    [SerializeField] private int n1Range = 5; // Range of n1
    [SerializeField] private int n2Range = 5; // Range of n2
    [SerializeField] private int n3Range = 5; // Range of n3

    [Header("Visualization")]
    [SerializeField] private GameObject latticePointPrefab; // Prefab for lattice point visualization
    [SerializeField] private float pointScale = 0.1f; // Scale for lattice points

    void Start()
    {
        // Generate and visualize lattice points
        GenerateLattice();
    }

    private void GenerateLattice()
    {
        // Loop through the lattice ranges
        for (int n1 = -n1Range; n1 <= n1Range; n1++)
        {
            for (int n2 = -n2Range; n2 <= n2Range; n2++)
            {
                for (int n3 = -n3Range; n3 <= n3Range; n3++)
                {
                    // Calculate lattice point position
                    Vector3 latticePoint = n1 * a1 + n2 * a2 + n3 * a3;

                    // Visualize the lattice point
                    VisualizeLatticePoint(latticePoint);
                }
            }
        }
    }

    private void VisualizeLatticePoint(Vector3 position)
    {
        // Instantiate a prefab at the lattice point position
        if (latticePointPrefab != null)
        {
            GameObject point = Instantiate(latticePointPrefab, position, Quaternion.identity, transform);
            point.transform.localScale = Vector3.one * pointScale;
        }
        else
        {
            Debug.LogError("Lattice Point Prefab is not assigned!");
        }
    }
}
