using UnityEngine;
using Accord.Math;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using System.Collections.Generic;

public class SpatialMemorySystemWithLibraries : MonoBehaviour
{
    [System.Serializable]
    public class MemoryNode
    {
        public Point3D Center { get; private set; }
        public double Radius { get; private set; }
        private Dictionary<GameObject, Point3D> objectMemory;

        public MemoryNode(Point3D center, double radius)
        {
            Center = center;
            Radius = radius;
            objectMemory = new Dictionary<GameObject, Point3D>();
        }

        /// <summary>
        /// Adds an object to the memory with its current position.
        /// </summary>
        public void AddOrUpdateObject(GameObject obj)
        {
            var position = ConvertToPoint3D(obj.transform.position);
            if (objectMemory.ContainsKey(obj))
            {
                objectMemory[obj] = position;
            }
            else
            {
                objectMemory.Add(obj, position);
            }
        }

        /// <summary>
        /// Removes an object from the memory.
        /// </summary>
        public void RemoveObject(GameObject obj)
        {
            if (objectMemory.ContainsKey(obj))
            {
                objectMemory.Remove(obj);
            }
        }

        /// <summary>
        /// Queries objects within a specific distance using Accord and MathNet.
        /// </summary>
        public List<GameObject> QueryObjects(Point3D queryCenter, double queryRadius)
        {
            List<GameObject> result = new List<GameObject>();

            foreach (var kvp in objectMemory)
            {
                double distance = queryCenter.DistanceTo(kvp.Value); // MathNet's precise distance calculation
                if (distance <= queryRadius)
                {
                    result.Add(kvp.Key);
                }
            }

            return result;
        }

        /// <summary>
        /// Checks if a point lies within the node's spherical boundary.
        /// </summary>
        public bool IsWithinBounds(Point3D point)
        {
            double distance = Center.DistanceTo(point);
            return distance <= Radius;
        }

        /// <summary>
        /// Converts a Unity Vector3 to a MathNet Point3D.
        /// </summary>
        private Point3D ConvertToPoint3D(UnityEngine.Vector3 vector)
        {
            return new Point3D(vector.x, vector.y, vector.z);
        }
    }

    [Header("Spatial Memory Parameters")]
    public float memoryRadius = 15.0f;
    public GameObject[] objectsToTrack;

    private MemoryNode spatialMemoryNode;

    void Start()
    {
        // Initialize the spatial memory node
        spatialMemoryNode = new MemoryNode(new Point3D(0, 0, 0), memoryRadius);

        // Populate memory with tracked objects
        foreach (var obj in objectsToTrack)
        {
            spatialMemoryNode.AddOrUpdateObject(obj);
        }
    }

    void Update()
    {
        // Update memory dynamically for moving objects
        foreach (var obj in objectsToTrack)
        {
            spatialMemoryNode.AddOrUpdateObject(obj);
        }

        // Example query: Find objects near a specific point
        var queryCenter = new Point3D(2, 0, -3);
        double queryRange = 7.0;
        List<GameObject> foundObjects = spatialMemoryNode.QueryObjects(queryCenter, queryRange);

        Debug.Log($"Found {foundObjects.Count} objects near {queryCenter} within {queryRange} units.");
    }

    void OnDrawGizmos()
    {
        if (spatialMemoryNode != null)
        {
            DrawMemoryNode(spatialMemoryNode);
        }
    }

    private void DrawMemoryNode(MemoryNode node)
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(new UnityEngine.Vector3((float)node.Center.X, (float)node.Center.Y, (float)node.Center.Z), (float)node.Radius);

        foreach (var obj in objectsToTrack)
        {
            if (obj != null)
            {
                Gizmos.color = Color.yellow;
                var pos = obj.transform.position;
                Gizmos.DrawSphere(pos, 0.2f);
            }
        }
    }
}
