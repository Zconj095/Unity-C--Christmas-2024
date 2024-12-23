using System;
using UnityEngine;
using MathNet.Numerics;
using Accord.Math;

public class EnergyTransferModel : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private double enthalpyChange = -100.0; // ?H (kJ/mol)
    [SerializeField] private double temperature = 298.15; // T (Kelvin)
    [SerializeField] private double entropyChange = 0.1; // ?S (kJ/mol·K)
    [SerializeField] private double magicalEnergyFactor = 5.0; // ?
    [SerializeField] private double magicalEnergyEffect = 2.0; // ?

    [Header("Results")]
    [SerializeField] private double freeEnergyChange; // ?G (kJ/mol)

    void Update()
    {
        // Update the free energy change dynamically
        freeEnergyChange = CalculateFreeEnergy(enthalpyChange, temperature, entropyChange, magicalEnergyFactor, magicalEnergyEffect);

        // Log the free energy change to the console
        Debug.Log($"?G: {freeEnergyChange:F4} kJ/mol");
    }

    private double CalculateFreeEnergy(double deltaH, double temp, double deltaS, double mu, double psi)
    {
        // Gibbs Free Energy with Magical Influence
        return deltaH - (temp * deltaS) - (mu * psi);
    }
}
