using System;
using UnityEngine;
using MathNet.Numerics;
using Accord.Math;

public class GeochemicalCycleSimulation : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private double sampleRatio = 0.011237; // R_sample: Ratio of heavy to light isotopes in the sample
    [SerializeField] private double standardRatio = 0.011180; // R_standard: Standard isotope ratio

    [Header("Results")]
    [SerializeField] private double isotopicFractionation; // ?13C: Isotopic fractionation of carbon

    void Start()
    {
        // Calculate isotopic fractionation
        isotopicFractionation = CalculateIsotopicFractionation(sampleRatio, standardRatio);

        // Log the result to the console
        Debug.Log($"Isotopic Fractionation (?13C): {isotopicFractionation:F4}");
    }

    private double CalculateIsotopicFractionation(double R_sample, double R_standard)
    {
        // Formula: ?13C = ((R_sample / R_standard) - 1) * 1000
        return ((R_sample / R_standard) - 1.0) * 1000.0;
    }
}
