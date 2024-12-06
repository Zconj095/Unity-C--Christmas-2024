using UnityEngine;

public class BSPVoxelGrid : MonoBehaviour
{
    public Vector3Int gridSize = new Vector3Int(10, 10, 10);
    public float voxelSize = 1.0f;

    [Tooltip("Enable or disable Gizmos for this grid.")]
    public bool enableGizmos = true;

    private BSPNode root;

    void Start()
    {
        InitializeBSP();
    }

    void InitializeBSP()
    {
        Bounds gridBounds = new Bounds(transform.position + (Vector3)gridSize * voxelSize / 2, (Vector3)gridSize * voxelSize);

        Nanovoxel[] voxels = new Nanovoxel[gridSize.x * gridSize.y * gridSize.z];
        int index = 0;

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int z = 0; z < gridSize.z; z++)
                {
                    Vector3 position = transform.position + new Vector3(x, y, z) * voxelSize;
                    voxels[index++] = new Nanovoxel(position, Color.gray, 1.0f, Random.value > 0.5f);
                }
            }
        }

        root = new BSPNode(gridBounds, voxels);
        root.Subdivide();
        Debug.Log("BSP tree initialized.");
    }

    void OnDrawGizmos()
    {
        if (!enableGizmos || root == null) return;

        DrawBSPNode(root);
    }

    void DrawBSPNode(BSPNode node)
    {
        if (node.isLeaf)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(node.bounds.center, node.bounds.size);
        }
        else
        {
            if (node.leftChild != null) DrawBSPNode(node.leftChild);
            if (node.rightChild != null) DrawBSPNode(node.rightChild);
        }
    }
}
