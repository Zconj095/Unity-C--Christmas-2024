using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixInterface : MonoBehaviour
    {
        // Represents a joint in the Matrix Interface
        private class MatrixJoint
        {
            public string JointID { get; private set; }
            public string Path { get; private set; }
            public bool IsEntangled { get; private set; }

            public MatrixJoint(string path)
            {
                JointID = System.Guid.NewGuid().ToString();
                Path = path;
                IsEntangled = false;
            }

            public void Entangle()
            {
                IsEntangled = true;
                Debug.Log($"Joint '{JointID}' on Path '{Path}' is now entangled.");
            }
        }

        // Represents a stitched network of joints
        private class StitchedNetwork
        {
            public string NetworkID { get; private set; }
            public List<MatrixJoint> Joints { get; private set; }

            public StitchedNetwork()
            {
                NetworkID = System.Guid.NewGuid().ToString();
                Joints = new List<MatrixJoint>();
            }

            public void AddJoint(MatrixJoint joint)
            {
                Joints.Add(joint);
                Debug.Log($"Joint '{joint.JointID}' added to Network '{NetworkID}'.");
            }

            public void BindAllJoints()
            {
                Debug.Log($"Binding all joints in Network '{NetworkID}'...");
                foreach (var joint in Joints)
                {
                    joint.Entangle();
                }
            }
        }

        // List of networks
        private List<StitchedNetwork> networks = new List<StitchedNetwork>();

        // Timers for automation
        private float createNetworkTimer = 0f;
        private float addJointTimer = 0f;
        private float bindNetworkTimer = 0f;
        private float retrieveNetworksTimer = 0f;

        // Automation intervals
        private const float createNetworkInterval = 5f; // Create a network every 5 seconds
        private const float addJointInterval = 3f; // Add a joint every 3 seconds
        private const float bindNetworkInterval = 7f; // Bind a network every 7 seconds
        private const float retrieveNetworksInterval = 10f; // Retrieve networks every 10 seconds

        // Create a new stitched network
        public void CreateNetwork()
        {
            StitchedNetwork network = new StitchedNetwork();
            networks.Add(network);
            Debug.Log($"Network Created - ID: {network.NetworkID}");
        }

        // Add a joint to a network
        public void AddJointToNetwork(string path, string networkID)
        {
            StitchedNetwork network = networks.Find(n => n.NetworkID == networkID);
            if (network != null)
            {
                MatrixJoint joint = new MatrixJoint(path);
                network.AddJoint(joint);
            }
            else
            {
                Debug.LogWarning($"Network with ID '{networkID}' not found.");
            }
        }

        // Bind all joints in a network
        public void BindNetwork(string networkID)
        {
            StitchedNetwork network = networks.Find(n => n.NetworkID == networkID);
            if (network != null)
            {
                network.BindAllJoints();
            }
            else
            {
                Debug.LogWarning($"Network with ID '{networkID}' not found.");
            }
        }

        // Retrieve all networks and their details
        public void RetrieveNetworks()
        {
            Debug.Log("Retrieving all networks...");
            foreach (var network in networks)
            {
                Debug.Log($"Network - ID: {network.NetworkID}, Total Joints: {network.Joints.Count}");
                foreach (var joint in network.Joints)
                {
                    Debug.Log($"  Joint - ID: {joint.JointID}, Path: {joint.Path}, Entangled: {joint.IsEntangled}");
                }
            }
        }

        private void Start()
        {
            Debug.Log("Matrix Interface Initialized.");
        }

        private void Update()
        {
            // Increment timers
            createNetworkTimer += Time.deltaTime;
            addJointTimer += Time.deltaTime;
            bindNetworkTimer += Time.deltaTime;
            retrieveNetworksTimer += Time.deltaTime;

            // Automate network creation
            if (createNetworkTimer >= createNetworkInterval)
            {
                CreateNetwork();
                createNetworkTimer = 0f;
            }

            // Automate adding joints
            if (addJointTimer >= addJointInterval && networks.Count > 0)
            {
                AddJointToNetwork($"Path_{Random.Range(1, 100)}", networks[networks.Count - 1].NetworkID);
                addJointTimer = 0f;
            }

            // Automate binding networks
            if (bindNetworkTimer >= bindNetworkInterval && networks.Count > 0)
            {
                BindNetwork(networks[networks.Count - 1].NetworkID);
                bindNetworkTimer = 0f;
            }

            // Automate retrieving network details
            if (retrieveNetworksTimer >= retrieveNetworksInterval)
            {
                RetrieveNetworks();
                retrieveNetworksTimer = 0f;
            }
        }
    }
}
