using UnityEngine;

public class BSPGridManager : MonoBehaviour
{
    [Tooltip("List of BSP Voxel Grids to manage.")]
    public BSPVoxelGrid[] grids;

    void OnDrawGizmos()
    {
        if (grids == null || grids.Length == 0) return;

        foreach (var grid in grids)
        {
            if (grid != null)
            {
                // Ensure grid Gizmos are drawn (no need to explicitly call OnDrawGizmos)
                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(grid.transform.position, grid.gridSize);
            }
        }
    }
}
