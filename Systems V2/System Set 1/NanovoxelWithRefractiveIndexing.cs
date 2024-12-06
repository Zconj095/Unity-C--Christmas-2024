using UnityEngine;

[System.Serializable]
public class NanovoxelWithRefractiveIndexing
{
    public Vector3 position;     // Center position of the voxel
    public Color color;          // Visual representation
    public float density;        // Material density
    public bool isActive;        // Whether the voxel is part of the current layer
    public float refractiveIndex; // Refractive index of the voxel material

    public NanovoxelWithRefractiveIndexing(Vector3 position, Color color, float density, bool isActive, float refractiveIndex)
    {
        this.position = position;
        this.color = color;
        this.density = density;
        this.isActive = isActive;
        this.refractiveIndex = refractiveIndex;
    }
}
