using UnityEngine;

public class HadamardAlgorithm : MonoBehaviour
{
    public Vector2 ApplyHadamard(Vector2 initialState)
    {
        // Simplified Hadamard matrix
        float sqrt2Inv = 1.0f / Mathf.Sqrt(2.0f);
        Vector2 result = new Vector2(
            sqrt2Inv * (initialState.x + initialState.y),
            sqrt2Inv * (initialState.x - initialState.y)
        );

        Debug.Log($"Applied Hadamard transform. Resulting state: {result}");
        return result;
    }
}
