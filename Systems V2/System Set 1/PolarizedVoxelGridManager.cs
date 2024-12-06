using UnityEngine;

public class PolarizedVoxelGridManager : MonoBehaviour
{
    [Header("Grid Parameters")]
    public Vector3Int gridDimensions = new Vector3Int(10, 10, 10);
    public float voxelSizeInWorldUnits = 0.1f;

    [Header("Material Properties")]
    public Color defaultGridColor = Color.gray;
    public float minRefractiveValue = 1.0f;
    public float maxRefractiveValue = 2.5f;

    [Header("Polarization Properties")]
    public float minOscillationFrequency = 0.1f; // Hz
    public float maxOscillationFrequency = 10.0f; // Hz
    public float polarizationMagnitude = 1.0f; // Magnitude of polarization oscillation

    private NanoVoxelWithPolarization[,,] polarizationGrid;

    void Start()
    {
        InitializePolarizedVoxelGrid();
    }

    void InitializePolarizedVoxelGrid()
    {
        polarizationGrid = new NanoVoxelWithPolarization[gridDimensions.x, gridDimensions.y, gridDimensions.z];

        for (int x = 0; x < gridDimensions.x; x++)
        {
            for (int y = 0; y < gridDimensions.y; y++)
            {
                for (int z = 0; z < gridDimensions.z; z++)
                {
                    Vector3 positionInGrid = transform.position + new Vector3(x, y, z) * voxelSizeInWorldUnits;

                    float randomRefractiveIndex = Random.Range(minRefractiveValue, maxRefractiveValue);
                    float randomFrequency = Random.Range(minOscillationFrequency, maxOscillationFrequency);

                    polarizationGrid[x, y, z] = new NanoVoxelWithPolarization(
                        gridPosition: positionInGrid,
                        displayColor: defaultGridColor,
                        materialDensity: 1.0f,
                        isElectricallyActive: false,
                        opticalRefractiveIndex: randomRefractiveIndex
                    )
                    {
                        frequencyOfOscillation = randomFrequency
                    };
                }
            }
        }

        Debug.Log($"Initialized polarized voxel grid with {gridDimensions.x * gridDimensions.y * gridDimensions.z} voxels.");
    }

    void Update()
    {
        UpdateVoxelPolarizationOverTime(Time.time);
        UpdateRefractiveIndicesOverTime(Time.time);
    }

    void UpdateVoxelPolarizationOverTime(float timeElapsed)
    {
        for (int x = 0; x < gridDimensions.x; x++)
        {
            for (int y = 0; y < gridDimensions.y; y++)
            {
                for (int z = 0; z < gridDimensions.z; z++)
                {
                    var voxel = polarizationGrid[x, y, z];

                    if (voxel.isElectricallyActive)
                    {
                        // Oscillate polarization using sine wave
                        float oscillation = Mathf.Sin(2 * Mathf.PI * voxel.frequencyOfOscillation * timeElapsed);
                        voxel.electricDipoleMoment = new Vector3(oscillation * polarizationMagnitude, 0, 0);

                        // Influence neighbors
                        PropagatePolarizationToNeighbors(x, y, z, voxel.electricDipoleMoment);
                    }
                }
            }
        }
    }

    void UpdateRefractiveIndicesOverTime(float timeElapsed)
    {
        for (int x = 0; x < gridDimensions.x; x++)
        {
            for (int y = 0; y < gridDimensions.y; y++)
            {
                for (int z = 0; z < gridDimensions.z; z++)
                {
                    var voxel = polarizationGrid[x, y, z];
                    if (voxel != null)
                    {
                        // Dynamically adjust refractive index
                        voxel.opticalRefractiveIndex = Mathf.Lerp(minRefractiveValue, maxRefractiveValue, Mathf.Sin(timeElapsed) * 0.5f + 0.5f);
                    }
                }
            }
        }
    }

    void PropagatePolarizationToNeighbors(int x, int y, int z, Vector3 polarization)
    {
        // Apply polarization influence to neighboring voxels
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dz = -1; dz <= 1; dz++)
                {
                    if (dx == 0 && dy == 0 && dz == 0) continue; // Skip self

                    int nx = x + dx;
                    int ny = y + dy;
                    int nz = z + dz;

                    var neighbor = GetVoxel(nx, ny, nz);
                    if (neighbor != null && neighbor.isElectricallyActive)
                    {
                        neighbor.electricDipoleMoment += polarization * 0.1f; // Apply influence
                    }
                }
            }
        }
    }

    public NanoVoxelWithPolarization GetVoxel(int x, int y, int z)
    {
        if (x < 0 || x >= gridDimensions.x || y < 0 || y >= gridDimensions.y || z < 0 || z >= gridDimensions.z)
        {
            return null;
        }

        return polarizationGrid[x, y, z];
    }
}
