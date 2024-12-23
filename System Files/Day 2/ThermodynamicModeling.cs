using System;
using UnityEngine;
using MathNet.Numerics;
using Accord.Math;

public class ThermodynamicModeling : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private double enthalpyChange = -200.0; // ?H: Enthalpy change (kJ/mol)
    [SerializeField] private double temperature = 298.15; // T: Temperature (Kelvin)
    [SerializeField] private double entropyChange = 0.1; // ?S: Entropy contribution (kJ/mol·K)
    [SerializeField] private double magicalEnergyFactor = 5.0; // ?: Magical energy factor
    [SerializeField] private double magicalEnergyEffect = 3.0; // ?: Magical energy input

    [Header("Results")]
    [SerializeField] private double freeEnergyChange; // ?G: Free energy

    void Update()
    {
        // Calculate the free energy change dynamically
        freeEnergyChange = CalculateFreeEnergy(
            enthalpyChange,
            temperature,
            entropyChange,
            magicalEnergyFactor,
            magicalEnergyEffect
        );

        // Log the resulting free energy change to the console
        Debug.Log($"Free Energy Change (?G): {freeEnergyChange:F4} kJ/mol");
    }

    private double CalculateFreeEnergy(
        double deltaH,
        double temp,
        double deltaS,
        double mu,
        double psi
    )
    {
        // Use MathNet.Numerics for calculations (if required for expansions later)
        return deltaH - (temp * deltaS) + (mu * psi);
    }
}
