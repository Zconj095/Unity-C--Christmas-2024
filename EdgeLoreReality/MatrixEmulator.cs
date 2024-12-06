using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixEmulator : MonoBehaviour
    {
        // Represents a replica of code or GUI in the emulator
        private class Replica
        {
            public string ReplicaID { get; private set; }
            public string OriginalData { get; private set; }
            public string SimulatedSpace { get; private set; }
            public bool IsActive { get; private set; }

            public Replica(string originalData, string simulatedSpace)
            {
                ReplicaID = System.Guid.NewGuid().ToString();
                OriginalData = originalData;
                SimulatedSpace = simulatedSpace;
                IsActive = false;
            }

            public void Activate()
            {
                IsActive = true;
                Debug.Log($"Replica '{ReplicaID}' activated in space: {SimulatedSpace}.");
            }

            public void Deactivate()
            {
                IsActive = false;
                Debug.Log($"Replica '{ReplicaID}' deactivated.");
            }
        }

        // List of replicas
        private List<Replica> replicas = new List<Replica>();

        // Flux capacitance generator management
        private float fluxCapacitancePower = 100f;

        // Timers for automation
        private float addReplicaTimer = 0f;
        private float activateReplicaTimer = 0f;
        private float deactivateReplicaTimer = 0f;
        private float adjustFluxTimer = 0f;
        private float simulateDataFlowTimer = 0f;

        // Automation intervals
        private const float addReplicaInterval = 5f; // Add a replica every 5 seconds
        private const float activateReplicaInterval = 7f; // Activate replicas every 7 seconds
        private const float deactivateReplicaInterval = 10f; // Deactivate replicas every 10 seconds
        private const float adjustFluxInterval = 8f; // Adjust flux capacitance every 8 seconds
        private const float simulateDataFlowInterval = 12f; // Simulate data flow every 12 seconds

        // Add a new replica
        public void AddReplica(string originalData, string simulatedSpace)
        {
            Replica replica = new Replica(originalData, simulatedSpace);
            replicas.Add(replica);
            Debug.Log($"Added Replica - ID: {replica.ReplicaID}, Original Data: {originalData}, Simulated Space: {simulatedSpace}");
        }

        // Activate a specific replica
        public void ActivateReplica(string replicaID)
        {
            foreach (var replica in replicas)
            {
                if (replica.ReplicaID == replicaID)
                {
                    if (!replica.IsActive)
                    {
                        replica.Activate();
                    }
                    else
                    {
                        Debug.LogWarning($"Replica '{replicaID}' is already active.");
                    }
                    return;
                }
            }
            Debug.LogWarning($"Replica with ID '{replicaID}' not found.");
        }

        // Deactivate a specific replica
        public void DeactivateReplica(string replicaID)
        {
            foreach (var replica in replicas)
            {
                if (replica.ReplicaID == replicaID)
                {
                    if (replica.IsActive)
                    {
                        replica.Deactivate();
                    }
                    else
                    {
                        Debug.LogWarning($"Replica '{replicaID}' is already inactive.");
                    }
                    return;
                }
            }
            Debug.LogWarning($"Replica with ID '{replicaID}' not found.");
        }

        // Manage flux capacitance power
        public void AdjustFluxCapacitance(float adjustment)
        {
            fluxCapacitancePower = Mathf.Clamp(fluxCapacitancePower + adjustment, 0f, 100f);
            Debug.Log($"Flux Capacitance Power adjusted to {fluxCapacitancePower}%.");
        }

        // Simulate data flow
        public void SimulateDataFlow()
        {
            if (fluxCapacitancePower <= 0)
            {
                Debug.LogWarning("Insufficient flux capacitance power for data simulation.");
                return;
            }

            Debug.Log("Simulating data flow between replicas and virtual grid...");
            foreach (var replica in replicas)
            {
                if (replica.IsActive)
                {
                    Debug.Log($"Simulated Data Flow - Replica ID: {replica.ReplicaID}, Space: {replica.SimulatedSpace}");
                }
            }
        }

        private void Start()
        {
            Debug.Log("Matrix Emulator Initialized.");
        }

        private void Update()
        {
            // Increment timers
            addReplicaTimer += Time.deltaTime;
            activateReplicaTimer += Time.deltaTime;
            deactivateReplicaTimer += Time.deltaTime;
            adjustFluxTimer += Time.deltaTime;
            simulateDataFlowTimer += Time.deltaTime;

            // Automate adding replicas
            if (addReplicaTimer >= addReplicaInterval)
            {
                AddReplica($"OriginalData_{replicas.Count + 1}", $"VirtualSpace_{replicas.Count + 1}");
                addReplicaTimer = 0f;
            }

            // Automate activating replicas
            if (activateReplicaTimer >= activateReplicaInterval && replicas.Count > 0)
            {
                foreach (var replica in replicas)
                {
                    if (!replica.IsActive)
                    {
                        replica.Activate();
                        break; // Activate one replica at a time
                    }
                }
                activateReplicaTimer = 0f;
            }

            // Automate deactivating replicas
            if (deactivateReplicaTimer >= deactivateReplicaInterval && replicas.Count > 0)
            {
                foreach (var replica in replicas)
                {
                    if (replica.IsActive)
                    {
                        replica.Deactivate();
                        break; // Deactivate one replica at a time
                    }
                }
                deactivateReplicaTimer = 0f;
            }

            // Automate adjusting flux capacitance power
            if (adjustFluxTimer >= adjustFluxInterval)
            {
                AdjustFluxCapacitance(Random.Range(-10f, 10f));
                adjustFluxTimer = 0f;
            }

            // Automate simulating data flow
            if (simulateDataFlowTimer >= simulateDataFlowInterval)
            {
                SimulateDataFlow();
                simulateDataFlowTimer = 0f;
            }
        }
    }
}
