using System;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;

public class EnergyFlowInCrystals : MonoBehaviour
{
    [Header("Crystal Parameters")]
    [SerializeField] private Vector3[] surfacePoints; // Points on the crystal surface
    [SerializeField] private Vector3[] normals; // Normal vectors at each surface point

    [Header("Magical Field Parameters")]
    [SerializeField] private Vector3 magicalFieldVector = new Vector3(1.0f, 1.0f, 0.0f); // E: Magical field vector
    [SerializeField] private float surfaceArea = 10.0f; // S: Surface area of the crystal (approximation)

    [Header("Results")]
    [SerializeField] private double totalMagicalFlux; // ?: Total magical energy flux

    void Start()
    {
        // Calculate the total magical energy flux
        totalMagicalFlux = CalculateMagicalEnergyFlux(surfacePoints, normals, magicalFieldVector, surfaceArea);

        // Log the result
        Debug.Log($"Total Magical Energy Flux (?): {totalMagicalFlux:F4}");
    }

    private double CalculateMagicalEnergyFlux(Vector3[] points, Vector3[] normals, Vector3 fieldVector, float area)
    {
        if (points.Length != normals.Length)
        {
            Debug.LogError("Surface points and normals arrays must have the same length!");
            return 0.0;
        }

        double flux = 0.0;
        float elementArea = area / points.Length; // Divide surface area equally among points

        // Compute the flux over the discretized surface
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 normal = normals[i].normalized;
            double dotProduct = Vector3.Dot(fieldVector, normal); // E·n
            flux += dotProduct * elementArea;
        }

        return flux;
    }
}
