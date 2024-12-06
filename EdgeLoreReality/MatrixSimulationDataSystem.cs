using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixSimulationDataSystem : MonoBehaviour
    {
        // Represents a data packet
        private class DataPacket
        {
            public string PacketID { get; private set; }
            public string SourceSystem { get; private set; }
            public string DestinationSystem { get; private set; }
            public string Content { get; private set; }

            public DataPacket(string source, string destination, string content)
            {
                PacketID = System.Guid.NewGuid().ToString();
                SourceSystem = source;
                DestinationSystem = destination;
                Content = content;
            }
        }

        // Represents the data analysis chamber
        private class DataAnalysisChamber
        {
            private List<DataPacket> reservoir = new List<DataPacket>();

            public void StoreData(DataPacket packet)
            {
                reservoir.Add(packet);
                Debug.Log($"Data Packet Stored in Reservoir - ID: {packet.PacketID}, Content: {packet.Content}");
            }

            public DataPacket RetrieveData(string packetID)
            {
                DataPacket packet = reservoir.Find(p => p.PacketID == packetID);
                if (packet != null)
                {
                    Debug.Log($"Data Packet Retrieved from Reservoir - ID: {packet.PacketID}, Content: {packet.Content}");
                }
                else
                {
                    Debug.LogWarning($"Data Packet with ID '{packetID}' not found in reservoir.");
                }

                return packet;
            }

            public void ClearReservoir()
            {
                reservoir.Clear();
                Debug.Log("Reservoir cleared.");
            }

            public void DisplayAllData()
            {
                Debug.Log("Displaying all data in the reservoir...");
                foreach (var packet in reservoir)
                {
                    Debug.Log($"Packet - ID: {packet.PacketID}, Source: {packet.SourceSystem}, Destination: {packet.DestinationSystem}, Content: {packet.Content}");
                }
            }

            public int Count => reservoir.Count;
        }

        // Data analysis chamber instance
        private DataAnalysisChamber dataChamber = new DataAnalysisChamber();

        // Timer for automated data operations
        private float operationTimer = 0f;
        private const float operationInterval = 5f; // Interval for automated operations

        // Transfer data between systems
        public void TransferData(string source, string destination, string content)
        {
            DataPacket packet = new DataPacket(source, destination, content);
            Debug.Log($"Data Packet Created - ID: {packet.PacketID}, Source: {source}, Destination: {destination}, Content: {content}");

            // Send data to the analysis chamber
            SendToAnalysisChamber(packet);
        }

        // Send data to the analysis chamber
        private void SendToAnalysisChamber(DataPacket packet)
        {
            Debug.Log($"Sending Data Packet to Analysis Chamber - ID: {packet.PacketID}");
            dataChamber.StoreData(packet);
        }

        // Retrieve and display a random data packet
        private void RetrieveRandomPacket()
        {
            if (dataChamber.Count > 0)
            {
                var randomIndex = Random.Range(0, dataChamber.Count);
                var packets = dataChamber.GetType()
                    .GetField("reservoir", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.GetValue(dataChamber) as List<DataPacket>;

                if (packets != null)
                {
                    var packet = packets[randomIndex];
                    dataChamber.RetrieveData(packet.PacketID);
                }
            }
            else
            {
                Debug.LogWarning("No data packets available for retrieval.");
            }
        }

        // Display all data in the reservoir
        public void DisplayReservoirData()
        {
            dataChamber.DisplayAllData();
        }

        // Clear all data from the reservoir
        public void ClearReservoir()
        {
            dataChamber.ClearReservoir();
        }

        private void Start()
        {
            Debug.Log("Automated Matrix Simulation Data System Initialized.");
        }

        private void Update()
        {
            // Increment the timer
            operationTimer += Time.deltaTime;

            // Perform automated operations at regular intervals
            if (operationTimer >= operationInterval)
            {
                PerformAutomatedOperations();
                operationTimer = 0f; // Reset timer
            }
        }

        // Perform automated operations
        private void PerformAutomatedOperations()
        {
            Debug.Log("Performing automated data system operations...");

            // Example operations
            TransferData("SystemA", "SystemB", $"Automated data content at {Time.time}");
            if (dataChamber.Count > 0)
            {
                RetrieveRandomPacket();
            }
            else
            {
                Debug.Log("No data available to retrieve, skipping retrieval step.");
            }

            // Display the reservoir data for monitoring
            DisplayReservoirData();
        }
    }
}
