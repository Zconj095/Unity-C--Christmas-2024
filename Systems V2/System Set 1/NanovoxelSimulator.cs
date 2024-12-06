using UnityEngine;

public class NanovoxelSimulator : MonoBehaviour
{
    public NanovoxelGrid voxelGrid;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3Int gridPosition = GetRandomGridPosition();
            voxelGrid.ActivateVoxel(gridPosition, Color.green, Random.Range(0.5f, 1.5f));
        }
    }

    private Vector3Int GetRandomGridPosition()
    {
        return new Vector3Int(
            Random.Range(0, voxelGrid.gridSize.x),
            Random.Range(0, voxelGrid.gridSize.y),
            Random.Range(0, voxelGrid.gridSize.z)
        );
    }
}
