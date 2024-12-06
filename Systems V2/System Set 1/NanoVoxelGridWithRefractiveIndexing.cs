using UnityEngine;

public class NanovoxelGridWithRefractiveIndexing : MonoBehaviour
{
    public Vector3Int gridSize = new Vector3Int(10, 10, 10);
    public float voxelSize = 0.1f;
    public Color layerColor = Color.gray;
    public float minRefractiveIndex = 1.0f; // Minimum refractive index (e.g., air)
    public float maxRefractiveIndex = 2.5f; // Maximum refractive index (e.g., dense glass)

    private NanovoxelWithRefractiveIndexing[,,] voxels;

    void Start()
    {
        InitializeGrid();
    }

    void InitializeGrid()
    {
        voxels = new NanovoxelWithRefractiveIndexing[gridSize.x, gridSize.y, gridSize.z];

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int z = 0; z < gridSize.z; z++)
                {
                    Vector3 position = transform.position + new Vector3(x, y, z) * voxelSize;

                    // Assign random refractive index within range
                    float refractiveIndex = Random.Range(minRefractiveIndex, maxRefractiveIndex);

                    voxels[x, y, z] = new NanovoxelWithRefractiveIndexing(position, Color.clear, 0.0f, false, refractiveIndex);
                }
            }
        }

        Debug.Log($"Initialized nanovoxelwithrefractiveindexing grid with {gridSize.x * gridSize.y * gridSize.z} voxels.");
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

                // Optionally vary color based on refractive index
                voxel.color = Color.Lerp(Color.blue, Color.red, (voxel.refractiveIndex - minRefractiveIndex) / (maxRefractiveIndex - minRefractiveIndex));
            }
        }

        Debug.Log($"Layer {layerIndex} activated.");
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
