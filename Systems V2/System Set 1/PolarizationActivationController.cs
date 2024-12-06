using UnityEngine;

public class PolarizationActivationController : MonoBehaviour
{
    public PolarizedVoxelGridManager voxelGridManager;
    public int layersToActivate = 3;

    void Start()
    {
        ActivatePolarizedVoxels();
    }

    void ActivatePolarizedVoxels()
    {
        for (int y = 0; y < layersToActivate; y++)
        {
            for (int x = 0; x < voxelGridManager.gridDimensions.x; x++)
            {
                for (int z = 0; z < voxelGridManager.gridDimensions.z; z++)
                {
                    var voxel = voxelGridManager.GetVoxel(x, y, z);
                    if (voxel != null)
                    {
                        voxel.isElectricallyActive = true;
                    }
                }
            }
        }

        Debug.Log($"Activated polarization for {layersToActivate} layers.");
    }
}
