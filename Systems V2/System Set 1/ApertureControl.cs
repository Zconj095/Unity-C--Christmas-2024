using UnityEngine;

public class ApertureControl : MonoBehaviour
{
    [Tooltip("Radius of the aperture opening.")]
    public float apertureRadius = 0.5f;

    [Tooltip("Intensity factor based on aperture size.")]
    public float intensityFactor = 1.0f;

    public float CalculateIntensity(float beamRadius)
    {
        if (beamRadius > apertureRadius)
        {
            // Block part of the beam if it exceeds the aperture radius
            float blockedIntensity = Mathf.Max(0, intensityFactor - (beamRadius - apertureRadius));
            return Mathf.Clamp(blockedIntensity, 0, intensityFactor);
        }

        // Full intensity if within the aperture radius
        return intensityFactor;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, apertureRadius);
    }
}
