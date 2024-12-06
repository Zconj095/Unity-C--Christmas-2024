using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class SystemAdministrationAccuracyNetwork : MonoBehaviour
    {
        // Represents a data packet traveling through the network
        private class DataPacket
        {
            public string PacketID { get; private set; }
            public string Content { get; private set; }
            public string Source { get; private set; }
            public string Destination { get; private set; }
            public bool IsAuthorized { get; private set; }

            public DataPacket(string content, string source, string destination, bool isAuthorized)
            {
                PacketID = System.Guid.NewGuid().ToString();
                Content = content;
                Source = source;
                Destination = destination;
                IsAuthorized = isAuthorized;
            }
        }

        // Queue to hold incoming data packets
        private Queue<DataPacket> inputChannel = new Queue<DataPacket>();
        private Queue<DataPacket> outputChannel = new Queue<DataPacket>();

        // Process a data packet to calculate a safer route
        private void ProcessDataPacket(DataPacket packet)
        {
            Debug.Log($"Processing Data Packet - ID: {packet.PacketID}, Source: {packet.Source}, Destination: {packet.Destination}");

            if (packet.IsAuthorized)
            {
                string safeRoute = CalculateSafeRoute(packet.Source, packet.Destination);
                Debug.Log($"Data Packet routed safely to: {safeRoute}");
                TransferToHeadAdmin(packet);
            }
            else
            {
                Debug.LogWarning($"Unauthorized Data Packet detected. Packet ID: {packet.PacketID} has been blocked.");
            }
        }

        // Calculate a collision-free route for the packet
        private string CalculateSafeRoute(string source, string destination)
        {
            // Simplified logic for determining a safer route
            return $"{source} -> IntermediateNode -> {destination}";
        }

        // Transfer the packet to the HeadAdmin for cataloging
        private void TransferToHeadAdmin(DataPacket packet)
        {
            Debug.Log($"Transferring Data Packet to HeadAdmin - ID: {packet.PacketID}");
            CatalogData(packet);
        }

        // Catalog the data into the HeadAdmin Master Matrix Code Database
        private void CatalogData(DataPacket packet)
        {
            Debug.Log($"Cataloging Data Packet into Master Matrix Code Database - Content: {packet.Content}");
            EncodeData(packet);
        }

        // Encode the data for normal network systems
        private void EncodeData(DataPacket packet)
        {
            Debug.Log($"Encoding Data Packet for Network Systems - Packet ID: {packet.PacketID}");
            outputChannel.Enqueue(packet);
        }

        // Add a packet to the input channel
        public void AddDataPacket(string content, string source, string destination, bool isAuthorized)
        {
            DataPacket packet = new DataPacket(content, source, destination, isAuthorized);
            inputChannel.Enqueue(packet);
            Debug.Log($"Data Packet Added - ID: {packet.PacketID}, Content: {content}, Source: {source}, Destination: {destination}, Authorized: {isAuthorized}");
        }

        // Process the next packet in the input channel
        private void ProcessNextPacket()
        {
            if (inputChannel.Count > 0)
            {
                DataPacket packet = inputChannel.Dequeue();
                ProcessDataPacket(packet);
            }
            else
            {
                Debug.LogWarning("No data packets to process in the input channel.");
            }
        }

        // Display all packets in the output channel
        private void DisplayOutputChannel()
        {
            Debug.Log("Displaying all packets in the output channel...");
            foreach (var packet in outputChannel)
            {
                Debug.Log($"Packet ID: {packet.PacketID}, Content: {packet.Content}, Destination: {packet.Destination}");
            }
        }

        private void Start()
        {
            Debug.Log("System Administration Accuracy Network Initialized.");

            // Automate actions
            InvokeRepeating(nameof(AutoGeneratePackets), 2f, 10f); // Generate packets every 10 seconds
            InvokeRepeating(nameof(ProcessNextPacket), 5f, 8f);   // Process packets every 8 seconds
            InvokeRepeating(nameof(DisplayOutputChannel), 15f, 20f); // Display output every 20 seconds
        }

        // Automatically generate packets for testing
        private void AutoGeneratePackets()
        {
            string[] sources = { "NodeA", "NodeB", "NodeC" };
            string[] destinations = { "NodeD", "NodeE", "NodeF" };

            string content = "Automated Data Content";
            string source = sources[Random.Range(0, sources.Length)];
            string destination = destinations[Random.Range(0, destinations.Length)];
            bool isAuthorized = Random.Range(0, 2) == 1;

            AddDataPacket(content, source, destination, isAuthorized);
        }
    }
}
