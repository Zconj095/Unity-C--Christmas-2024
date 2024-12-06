using System;
using UnityEngine;
public class VectorRiftCalculator : MonoBehaviour
{
    public static float CalculateRift(MultidimensionalHypervector v1, MultidimensionalHypervector v2)
    {
        int dimensions = Math.Max(v1.Dimensions, v2.Dimensions);
        float totalRift = 0;

        for (int i = 0; i < dimensions; i++)
        {
            float v1Value = i < v1.Dimensions ? v1.Values[i] : 0;
            float v2Value = i < v2.Dimensions ? v2.Values[i] : 0;
            totalRift += Mathf.Abs(v1Value - v2Value); // Summing deviations
        }

        return totalRift;
    }
}
