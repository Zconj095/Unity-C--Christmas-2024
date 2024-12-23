using System;
using UnityEngine;
using MathNet.Numerics;

public class DiffractionPatternAnalysis : MonoBehaviour
{
    [Header("Input Parameters")]
    [SerializeField] private int diffractionOrder = 1; // n: Order of diffraction
    [SerializeField] private double wavelength = 1.54; // ?: Wavelength of light (Angstroms)
    [SerializeField] private double spacingBetweenPlanes = 2.0; // d: Spacing between crystal planes (Angstroms)
    [SerializeField] private double angleOfIncidence = 30.0; // ?: Angle of incidence (degrees)

    [Header("Results")]
    [SerializeField] private bool isDiffractionConditionMet; // Whether Bragg's law is satisfied

    void Start()
    {
        // Check if the diffraction condition is satisfied
        isDiffractionConditionMet = CheckDiffractionCondition(
            diffractionOrder,
            wavelength,
            spacingBetweenPlanes,
            angleOfIncidence
        );

        // Log the result
        if (isDiffractionConditionMet)
        {
            Debug.Log("Bragg's Law is satisfied. Diffraction occurs.");
        }
        else
        {
            Debug.Log("Bragg's Law is NOT satisfied. No diffraction.");
        }
    }

    private bool CheckDiffractionCondition(int n, double lambda, double d, double theta)
    {
        // Convert angle to radians
        double thetaRadians = MathNet.Numerics.Trig.DegreeToRadian(theta);

        // Calculate the left-hand side of Bragg's law
        double lhs = n * lambda;

        // Calculate the right-hand side of Bragg's law
        double rhs = 2 * d * Math.Sin(thetaRadians);

        // Check if the diffraction condition is satisfied
        return Math.Abs(lhs - rhs) < 1e-6; // Allow a small tolerance for floating-point precision
    }
}
