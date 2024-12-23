using System;
using UnityEngine;
using MathNet.Numerics.Integration;

public class MagicalGeochemicalInteraction : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private int gridResolution = 100; // Number of points for numerical integration
    [SerializeField] private Vector3 domainStart = new Vector3(0, 0, 0); // Start of integration domain
    [SerializeField] private Vector3 domainEnd = new Vector3(10, 10, 10); // End of integration domain

    [Header("Results")]
    [SerializeField] private double geochemicalPower; // P_geo: Power available for geochemical transformations

    void Start()
    {
        // Calculate the power dynamically using numerical integration
        geochemicalPower = CalculateLeylineEnergy(domainStart, domainEnd, gridResolution);

        // Log the result
        Debug.Log($"Geochemical Power (P_geo): {geochemicalPower:F4}");
    }

    private double CalculateLeylineEnergy(Vector3 start, Vector3 end, int resolution)
    {
        // Define step size for each axis
        double dx = (end.x - start.x) / resolution;
        double dy = (end.y - start.y) / resolution;
        double dz = (end.z - start.z) / resolution;

        double energySum = 0.0;

        // Perform numerical integration over the 3D grid
        for (int i = 0; i < resolution; i++)
        {
            for (int j = 0; j < resolution; j++)
            {
                for (int k = 0; k < resolution; k++)
                {
                    // Calculate position in the grid
                    Vector3 position = new Vector3(
                        (float)(start.x + i * dx),
                        (float)(start.y + j * dy),
                        (float)(start.z + k * dz)
                    );

                    // Evaluate the integrand: ?(r) * ??(r)
                    double integrandValue = MagicalFieldStrength(position) * EnergyGradient(position).magnitude;

                    // Add contribution to the total energy sum
                    energySum += integrandValue * dx * dy * dz;
                }
            }
        }

        return energySum;
    }

    private double MagicalFieldStrength(Vector3 position)
    {
        // Example magical field strength ?(r): Gaussian distribution
        double sigma = 5.0; // Spread of the field
        return Math.Exp(-position.sqrMagnitude / (2 * sigma * sigma));
    }

    private Vector3 EnergyGradient(Vector3 position)
    {
        // Example gradient ??(r): Divergence of a 3D sine wave field
        float gradientX = (float)Math.Cos(position.x);
        float gradientY = (float)Math.Cos(position.y);
        float gradientZ = (float)Math.Cos(position.z);

        return new Vector3(gradientX, gradientY, gradientZ);
    }
}
