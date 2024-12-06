using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class SystemAdministrationNetwork : MonoBehaviour
    {
        // Represents a network in the system
        private class Network
        {
            public string NetworkID { get; private set; }
            public string Name { get; private set; }
            public string Type { get; private set; } // e.g., Public, Private, Server, etc.
            public bool IsActive { get; private set; }

            public Network(string name, string type, bool isActive)
            {
                NetworkID = System.Guid.NewGuid().ToString();
                Name = name;
                Type = type;
                IsActive = isActive;
            }

            public void Activate()
            {
                IsActive = true;
                Debug.Log($"Network '{Name}' activated.");
            }

            public void Deactivate()
            {
                IsActive = false;
                Debug.Log($"Network '{Name}' deactivated.");
            }

            public void Display()
            {
                Debug.Log($"Network ID: {NetworkID}, Name: {Name}, Type: {Type}, Active: {IsActive}");
            }
        }

        // List of networks
        private List<Network> networks = new List<Network>();

        // Infinity Matrix power status
        private bool infinityMatrixActive = true;

        // Add a new network
        private void AddNetwork(string name, string type, bool isActive = false)
        {
            Network newNetwork = new Network(name, type, isActive);
            networks.Add(newNetwork);
            Debug.Log($"New Network Added - ID: {newNetwork.NetworkID}, Name: {name}, Type: {type}, Active: {isActive}");
        }

        // Activate or deactivate a network
        private void ToggleNetwork(string networkID, bool activate)
        {
            if (!infinityMatrixActive && activate)
            {
                Debug.LogWarning("Cannot activate networks while the Infinity Matrix is inactive.");
                return;
            }

            Network network = networks.Find(n => n.NetworkID == networkID);
            if (network != null)
            {
                if (activate)
                {
                    network.Activate();
                }
                else
                {
                    network.Deactivate();
                }
            }
            else
            {
                Debug.LogWarning($"Network with ID '{networkID}' not found.");
            }
        }

        // Display all networks
        private void DisplayNetworks()
        {
            Debug.Log("Displaying all networks...");
            foreach (var network in networks)
            {
                network.Display();
            }
        }

        // Toggle the Infinity Matrix power
        private void ToggleInfinityMatrix(bool activate)
        {
            infinityMatrixActive = activate;
            Debug.Log($"Infinity Matrix power toggled. Active: {infinityMatrixActive}");
        }

        // Automated actions
        private void AutoAddNetwork()
        {
            string name = $"Network-{networks.Count + 1}";
            string type = Random.value > 0.5f ? "Public" : "Private";
            bool isActive = Random.value > 0.5f;
            AddNetwork(name, type, isActive);
        }

        private void AutoToggleNetwork()
        {
            if (networks.Count > 0)
            {
                int randomIndex = Random.Range(0, networks.Count);
                Network network = networks[randomIndex];
                ToggleNetwork(network.NetworkID, !network.IsActive);
            }
        }

        private void AutoDisplayNetworks()
        {
            DisplayNetworks();
        }

        private void AutoToggleInfinityMatrix()
        {
            ToggleInfinityMatrix(!infinityMatrixActive);
        }

        private void Start()
        {
            Debug.Log("Automated System Administration Network Initialized.");

            // Initial setup
            AddNetwork("Public Network", "Public", true);
            AddNetwork("Private Network", "Private", false);
            AddNetwork("Server Network", "Server", false);

            // Schedule automated actions
            InvokeRepeating(nameof(AutoAddNetwork), 3f, 15f); // Add a new network every 15 seconds
            InvokeRepeating(nameof(AutoToggleNetwork), 5f, 10f); // Toggle a network every 10 seconds
            InvokeRepeating(nameof(AutoDisplayNetworks), 10f, 20f); // Display networks every 20 seconds
            InvokeRepeating(nameof(AutoToggleInfinityMatrix), 30f, 60f); // Toggle Infinity Matrix power every 60 seconds
        }
    }
}
