using UnityEngine;
using System;
public class HypervectorGate : MonoBehaviour
{
    public static MultidimensionalHypervector AND(MultidimensionalHypervector v1, MultidimensionalHypervector v2)
    {
        int dimensions = Math.Min(v1.Dimensions, v2.Dimensions);
        MultidimensionalHypervector result = new MultidimensionalHypervector(dimensions);

        for (int i = 0; i < dimensions; i++)
        {
            result.Values[i] = Mathf.Min(v1.Values[i], v2.Values[i]); // AND logic
        }

        return result;
    }

    public static MultidimensionalHypervector OR(MultidimensionalHypervector v1, MultidimensionalHypervector v2)
    {
        int dimensions = Math.Min(v1.Dimensions, v2.Dimensions);
        MultidimensionalHypervector result = new MultidimensionalHypervector(dimensions);

        for (int i = 0; i < dimensions; i++)
        {
            result.Values[i] = Mathf.Max(v1.Values[i], v2.Values[i]); // OR logic
        }

        return result;
    }
}
