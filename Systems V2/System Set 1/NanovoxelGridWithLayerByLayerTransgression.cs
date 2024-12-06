using System.Collections.Generic;
using UnityEngine;

public class NanovoxelGridWithLayerByLayerTransgression : MonoBehaviour
{
    [Tooltip("Size of the grid (width, height, depth).")]
    public Vector3Int gridSize = new Vector3Int(10, 10, 10);

    [Tooltip("Size of each voxel.")]
    public float voxelSize = 0.1f;

    [Tooltip("Default color for new layers.")]
    public Color layerColor = Color.gray;

    private Nanovoxel[,,] voxels;

    void Start()
    {
        InitializeGrid();
    }

    void InitializeGrid()
    {
        voxels = new Nanovoxel[gridSize.x, gridSize.y, gridSize.z];

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int z = 0; z < gridSize.z; z++)
                {
                    Vector3 position = transform.position + new Vector3(x, y, z) * voxelSize;
                    voxels[x, y, z] = new Nanovoxel(position, Color.clear, 0.0f, false);
                }
            }
        }

        Debug.Log($"Initialized nanovoxel grid with {gridSize.x * gridSize.y * gridSize.z} voxels.");
    }

    public void ActivateLayer(int layerIndex, float density)
    {
        if (layerIndex < 0 || layerIndex >= gridSize.y)
        {
            Debug.LogWarning("Layer index out of bounds.");
            return;
        }

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int z = 0; z < gridSize.z; z++)
            {
                var voxel = voxels[x, layerIndex, z];
                voxel.isActive = true;
                voxel.color = layerColor;
                voxel.density = density;
            }
        }

        Debug.Log($"Layer {layerIndex} activated.");
    }

    public void DeactivateLayer(int layerIndex)
    {
        if (layerIndex < 0 || layerIndex >= gridSize.y)
        {
            Debug.LogWarning("Layer index out of bounds.");
            return;
        }

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int z = 0; z < gridSize.z; z++)
            {
                var voxel = voxels[x, layerIndex, z];
                voxel.isActive = false;
                voxel.color = Color.clear;
                voxel.density = 0.0f;
            }
        }

        Debug.Log($"Layer {layerIndex} deactivated.");
    }

    void OnDrawGizmos()
    {
        if (voxels == null) return;

        Gizmos.color = Color.white;

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int z = 0; z < gridSize.z; z++)
                {
                    var voxel = voxels[x, y, z];
                    if (voxel.isActive)
                    {
                        Gizmos.color = voxel.color;
                        Gizmos.DrawCube(voxel.position, Vector3.one * voxelSize);
                    }
                }
            }
        }
    }
}
