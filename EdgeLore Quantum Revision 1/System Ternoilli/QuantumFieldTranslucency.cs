using UnityEngine;

public class QuantumFieldTranslucency : MonoBehaviour
{
    private float[] translucencyData;
    private float lastFluxOutput = float.MinValue; // Track the last flux output used for calculation

    /// <summary>
    /// Calculates translucency data based on the provided flux output and expected size.
    /// </summary>
    /// <param name="fluxOutput">The quantum flux output value.</param>
    /// <param name="expectedSize">The expected size of the translucency data array.</param>
    public void CalculateTranslucency(float fluxOutput, int expectedSize)
    {
        if (fluxOutput != lastFluxOutput)
        {
            if (expectedSize <= 0)
            {
                Debug.LogError("Expected size for translucency data must be greater than zero.");
                return;
            }

            translucencyData = new float[expectedSize];

            for (int i = 0; i < expectedSize; i++)
            {
                translucencyData[i] = Mathf.Abs(Mathf.Sin(fluxOutput + i * 0.1f));
            }

            lastFluxOutput = fluxOutput; // Update the last flux output after recalculating
            Debug.Log($"Translucency data calculated with fluxOutput: {fluxOutput} and size: {expectedSize}");
        }
        else
        {
            Debug.Log("Flux output unchanged. Using cached translucency data.");
        }
    }

    /// <summary>
    /// Retrieves the calculated translucency data.
    /// </summary>
    /// <returns>An array of translucency data. Returns an empty array if data is not initialized.</returns>
    public float[] GetTranslucencyData()
    {
        if (translucencyData == null || translucencyData.Length == 0)
        {
            Debug.LogWarning("Translucency data is not initialized. Returning empty array.");
            return new float[0]; // Safely handle the case where data is not available
        }
        return translucencyData;
    }
}
