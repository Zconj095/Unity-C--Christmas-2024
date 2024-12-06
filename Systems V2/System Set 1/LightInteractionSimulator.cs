using UnityEngine;
using System.Collections.Generic;

public class LightInteractionSimulator : MonoBehaviour
{
    [Tooltip("Reference to the voxel grid.")]
    public PolarizedVoxelGridManager voxelGridManager;

    [Tooltip("Position of the light source in world space.")]
    public Vector3 lightSource = new Vector3(0, 5, 0);

    [Tooltip("Intensity of the light source.")]
    public float lightIntensity = 1.0f;

    [Tooltip("Dynamic light source movement speed.")]
    public float lightMovementSpeed = 1.0f;

    private Vector3 initialLightSource;

    void Start()
    {
        initialLightSource = lightSource;
    }

    void Update()
    {
        // Move the light source dynamically in a circular motion for visualization
        lightSource = initialLightSource + new Vector3(
            Mathf.Sin(Time.time * lightMovementSpeed) * 2.0f,
            0,
            Mathf.Cos(Time.time * lightMovementSpeed) * 2.0f
        );
    }

    void OnDrawGizmos()
    {
        if (voxelGridManager == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(lightSource, 0.1f);

        foreach (var voxel in GetActiveVoxels())
        {
            SimulateLightInteraction(voxel);
        }
    }

    void SimulateLightInteraction(NanoVoxelWithPolarization voxel)
    {
        Vector3 lightDirection = (voxel.gridPosition - lightSource).normalized;
        float distance = Vector3.Distance(lightSource, voxel.gridPosition);

        // Snell's Law: Calculate refraction angle
        float incidentAngle = Vector3.Angle(-lightDirection, Vector3.up);
        float refractedAngle = Mathf.Asin(Mathf.Sin(incidentAngle * Mathf.Deg2Rad) / voxel.opticalRefractiveIndex) * Mathf.Rad2Deg;

        // Refraction visualization (adjust direction slightly)
        Vector3 refractedDirection = Quaternion.AngleAxis(refractedAngle, Vector3.Cross(Vector3.up, lightDirection)) * Vector3.up;

        // Light attenuation based on density and distance
        float attenuation = Mathf.Exp(-distance * voxel.materialDensity);

        // Map attenuation to color intensity
        Gizmos.color = new Color(
            voxel.displayColor.r * attenuation,
            voxel.displayColor.g * attenuation,
            voxel.displayColor.b * attenuation
        );

        // Draw refracted light path
        Gizmos.DrawLine(lightSource, voxel.gridPosition + refractedDirection);
    }

    private NanoVoxelWithPolarization[] GetActiveVoxels()
    {
        List<NanoVoxelWithPolarization> activeVoxels = new List<NanoVoxelWithPolarization>();

        for (int x = 0; x < voxelGridManager.gridDimensions.x; x++)
        {
            for (int y = 0; y < voxelGridManager.gridDimensions.y; y++)
            {
                for (int z = 0; z < voxelGridManager.gridDimensions.z; z++)
                {
                    var voxel = voxelGridManager.GetVoxel(x, y, z);
                    if (voxel != null && voxel.isElectricallyActive)
                    {
                        activeVoxels.Add(voxel);
                    }
                }
            }
        }

        return activeVoxels.ToArray();
    }
}
