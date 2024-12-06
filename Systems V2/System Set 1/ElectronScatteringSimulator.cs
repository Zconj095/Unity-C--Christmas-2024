using System.Collections.Generic;
using UnityEngine;

public class ElectronScatteringSimulator : MonoBehaviour
{
    [Tooltip("Number of electrons to simulate.")]
    public int electronCount = 1000;

    [Tooltip("Maximum scattering angle (degrees).")]
    public float maxScatteringAngle = 90.0f;

    [Tooltip("Initial energy of electrons.")]
    public float initialEnergy = 100.0f;

    [Tooltip("Distance each electron travels per step.")]
    public float stepDistance = 0.1f;

    [Tooltip("Layer mask for detecting the sample.")]
    public LayerMask sampleLayer;

    public List<ScatteredElectron> electrons;
    public List<Vector3> finalPositions;

    void Start()
    {
        InitializeElectrons();
    }

    void InitializeElectrons()
    {
        electrons = new List<ScatteredElectron>();
        finalPositions = new List<Vector3>();

        for (int i = 0; i < electronCount; i++)
        {
            // Initialize electrons at the origin, pointing down (e.g., z-axis)
            electrons.Add(new ScatteredElectron(
                position: transform.position,
                direction: Vector3.forward,
                energy: initialEnergy,
                scatteringAngle: maxScatteringAngle
            ));
        }

        Debug.Log($"{electronCount} electrons initialized.");
    }

    void Update()
    {
        SimulateScattering();
    }

    void SimulateScattering()
    {
        foreach (var electron in electrons)
        {
            // Move electron in its current direction
            electron.Move(stepDistance);

            // Check if the electron hits the sample
            if (Physics.Raycast(electron.position, electron.direction, out RaycastHit hit, stepDistance, sampleLayer))
            {
                // Simulate scattering
                electron.Scatter(hit.normal);

                // Reduce energy due to interaction
                electron.energy *= 0.9f; // Example energy loss factor

                // Stop simulation if energy falls below a threshold
                if (electron.energy < 1.0f)
                {
                    finalPositions.Add(electron.position);
                    electrons.Remove(electron);
                    break;
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if (finalPositions == null) return;

        // Draw final positions of scattered electrons
        Gizmos.color = Color.red;
        foreach (var position in finalPositions)
        {
            Gizmos.DrawSphere(position, 0.02f);
        }
    }
}
