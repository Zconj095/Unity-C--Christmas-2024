using UnityEngine;

[System.Serializable]
public class NanoVoxelWithPolarization
{
    public Vector3 gridPosition;      // Center position of the voxel
    public Color displayColor;       // Color for visual representation
    public float materialDensity;    // Material density
    public bool isElectricallyActive; // Whether the voxel is polarized
    public float opticalRefractiveIndex; // Refractive index of the material

    // Oscillating polarization properties
    public Vector3 electricDipoleMoment; // Current polarization vector
    public float frequencyOfOscillation; // Frequency of polarization oscillation

    public NanoVoxelWithPolarization(Vector3 gridPosition, Color displayColor, float materialDensity, bool isElectricallyActive, float opticalRefractiveIndex)
    {
        this.gridPosition = gridPosition;
        this.displayColor = displayColor;
        this.materialDensity = materialDensity;
        this.isElectricallyActive = isElectricallyActive;
        this.opticalRefractiveIndex = opticalRefractiveIndex;

        // Initialize polarization-specific attributes
        this.electricDipoleMoment = Vector3.zero;
        this.frequencyOfOscillation = 0.0f;
    }
}
