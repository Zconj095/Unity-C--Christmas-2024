using UnityEngine;

public class BSPNode
{
    public Bounds bounds; // The region represented by this node
    public BSPNode leftChild; // Left subregion
    public BSPNode rightChild; // Right subregion
    public bool isLeaf; // Whether this is a leaf node
    public Nanovoxel[] voxels; // Voxels contained in this node

    public BSPNode(Bounds bounds, Nanovoxel[] voxels)
    {
        this.bounds = bounds;
        this.voxels = voxels;
        isLeaf = true; // Start as a leaf node
    }

    public void Subdivide()
    {
        if (voxels.Length <= 1) return; // No need to subdivide further

        isLeaf = false;

        // Divide the longest axis of the bounds
        Vector3 size = bounds.size;
        Vector3 center = bounds.center;
        Bounds leftBounds, rightBounds;

        if (size.x >= size.y && size.x >= size.z)
        {
            float split = center.x;
            leftBounds = new Bounds(center - new Vector3(size.x / 4, 0, 0), new Vector3(size.x / 2, size.y, size.z));
            rightBounds = new Bounds(center + new Vector3(size.x / 4, 0, 0), new Vector3(size.x / 2, size.y, size.z));
        }
        else if (size.y >= size.x && size.y >= size.z)
        {
            float split = center.y;
            leftBounds = new Bounds(center - new Vector3(0, size.y / 4, 0), new Vector3(size.x, size.y / 2, size.z));
            rightBounds = new Bounds(center + new Vector3(0, size.y / 4, 0), new Vector3(size.x, size.y / 2, size.z));
        }
        else
        {
            float split = center.z;
            leftBounds = new Bounds(center - new Vector3(0, 0, size.z / 4), new Vector3(size.x, size.y, size.z / 2));
            rightBounds = new Bounds(center + new Vector3(0, 0, size.z / 4), new Vector3(size.x, size.y, size.z / 2));
        }

        // Partition voxels between the two children
        var leftVoxels = System.Array.FindAll(voxels, v => leftBounds.Contains(v.position));
        var rightVoxels = System.Array.FindAll(voxels, v => rightBounds.Contains(v.position));

        leftChild = new BSPNode(leftBounds, leftVoxels);
        rightChild = new BSPNode(rightBounds, rightVoxels);

        leftChild.Subdivide();
        rightChild.Subdivide();

        voxels = null; // Remove voxels from this node to save memory
    }
}
