using UnityEngine;

[System.Serializable]
public class Nanovoxel
{
    public Vector3 position; // Center position of the voxel
    public Color color;      // Color representing the material
    public float density;    // Density or material property
    public bool isActive;    // Whether this voxel is active in the simulation

    public Nanovoxel(Vector3 position, Color color, float density, bool isActive)
    {
        this.position = position;
        this.color = color;
        this.density = density;
        this.isActive = isActive;
    }
}
