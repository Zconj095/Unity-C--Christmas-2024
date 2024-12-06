using UnityEngine;

public class LayerTransgressionController : MonoBehaviour
{
    [Tooltip("Reference to the voxel grid.")]
    public NanovoxelGridWithLayerByLayerTransgression voxelGrid;

    [Tooltip("Time interval between layer activations.")]
    public float activationInterval = 1.0f;

    [Tooltip("Density for activated layers.")]
    public float layerDensity = 1.0f;

    private int currentLayer = 0;
    private float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= activationInterval)
        {
            ActivateNextLayer();
            timer = 0;
        }
    }

    void ActivateNextLayer()
    {
        if (currentLayer >= voxelGrid.gridSize.y)
        {
            Debug.Log("All layers activated.");
            enabled = false;
            return;
        }

        voxelGrid.ActivateLayer(currentLayer, layerDensity);
        currentLayer++;
    }
}
