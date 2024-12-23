using UnityEngine;
using Accord.Math;
using MathNet.Spatial.Euclidean;
using MathNet.Numerics;
using System.Collections.Generic;

public class SpatialPartition : MonoBehaviour
{
    [System.Serializable]
    public class SpatialNode
    {
        public UnityEngine.Vector3 Center { get; private set; }
        public float Size { get; private set; }
        public List<GameObject> ContainedObjects { get; private set; }
        public SpatialNode[] Children { get; private set; }

        public SpatialNode(UnityEngine.Vector3 center, float size)
        {
            Center = center;
            Size = size;
            ContainedObjects = new List<GameObject>();
            Children = null; // No children initially
        }

        /// <summary>
        /// Splits this node into 8 child nodes (octree).
        /// </summary>
        public void Subdivide()
        {
            Children = new SpatialNode[8];
            float halfSize = Size / 2;

            for (int i = 0; i < 8; i++)
            {
                UnityEngine.Vector3 offset = new UnityEngine.Vector3(
                    (i & 1) == 0 ? -halfSize : halfSize,
                    (i & 2) == 0 ? -halfSize : halfSize,
                    (i & 4) == 0 ? -halfSize : halfSize
                );
                Children[i] = new SpatialNode(Center + offset, halfSize);
            }
        }

        /// <summary>
        /// Checks if a point is within this node.
        /// </summary>
        public bool ContainsPoint(UnityEngine.Vector3 point)
        {
            float halfSize = Size / 2;
            return (point.x >= Center.x - halfSize && point.x <= Center.x + halfSize) &&
                   (point.y >= Center.y - halfSize && point.y <= Center.y + halfSize) &&
                   (point.z >= Center.z - halfSize && point.z <= Center.z + halfSize);
        }
    }

    [Header("Spatial Partition Parameters")]
    public float initialSize = 10.0f;
    public int maxDepth = 5;
    public GameObject[] objectsToPartition;

    private SpatialNode rootNode;

    void Start()
    {
        rootNode = new SpatialNode(UnityEngine.Vector3.zero, initialSize);
        BuildSpatialPartition(rootNode, 0);
    }

    /// <summary>
    /// Recursively builds the spatial partition.
    /// </summary>
    /// <param name="node">Current node.</param>
    /// <param name="depth">Current depth of recursion.</param>
    private void BuildSpatialPartition(SpatialNode node, int depth)
    {
        if (depth >= maxDepth)
            return;

        node.Subdivide();

        foreach (var child in node.Children)
        {
            foreach (var obj in objectsToPartition)
            {
                if (child.ContainsPoint(obj.transform.position))
                {
                    child.ContainedObjects.Add(obj);
                }
            }

            // Recurse into the child nodes
            BuildSpatialPartition(child, depth + 1);
        }
    }

    /// <summary>
    /// Visualizes the spatial partition in the Scene view.
    /// </summary>
    void OnDrawGizmos()
    {
        if (rootNode != null)
        {
            DrawNode(rootNode);
        }
    }

    private void DrawNode(SpatialNode node)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(node.Center, UnityEngine.Vector3.one * node.Size);

        if (node.Children != null)
        {
            foreach (var child in node.Children)
            {
                DrawNode(child);
            }
        }
    }
}
