using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class SystemAdministrationAnalysisSystem : MonoBehaviour
    {
        // Represents a data packet for analysis
        private class DataPacket
        {
            public string PacketID { get; private set; }
            public string Content { get; private set; }
            public string Direction { get; private set; } // "Input" or "Output"
            public bool IsAnalyzed { get; private set; }

            public DataPacket(string content, string direction)
            {
                PacketID = System.Guid.NewGuid().ToString();
                Content = content;
                Direction = direction;
                IsAnalyzed = false;
            }

            public void MarkAnalyzed()
            {
                IsAnalyzed = true;
                Debug.Log($"Packet '{PacketID}' analyzed. Direction: {Direction}, Content: {Content}");
            }
        }

        // Represents an adaptability and adjustment record
        private class AdaptabilityRecord
        {
            public string RecordID { get; private set; }
            public string Content { get; private set; }
            public string AdjustmentDetails { get; private set; }

            public AdaptabilityRecord(string content, string adjustmentDetails)
            {
                RecordID = System.Guid.NewGuid().ToString();
                Content = content;
                AdjustmentDetails = adjustmentDetails;
            }
        }

        // Input and output data channels
        private Queue<DataPacket> inputChannel = new Queue<DataPacket>();
        private Queue<DataPacket> outputChannel = new Queue<DataPacket>();

        // Adaptability records
        private List<AdaptabilityRecord> adaptabilityRecords = new List<AdaptabilityRecord>();

        // Analyze a data packet
        private void AnalyzeDataPacket(DataPacket packet)
        {
            Debug.Log($"Analyzing Packet - ID: {packet.PacketID}, Direction: {packet.Direction}");
            packet.MarkAnalyzed();

            // Simulate checking for adjustments
            if (packet.Content.Contains("Error") || packet.Content.Contains("Patch"))
            {
                CreateAdaptabilityRecord(packet.Content, "Error detected; patch required.");
            }

            // Catalog the data
            CatalogData(packet);
        }

        // Create an adaptability record for noted adjustments
        private void CreateAdaptabilityRecord(string content, string adjustmentDetails)
        {
            AdaptabilityRecord record = new AdaptabilityRecord(content, adjustmentDetails);
            adaptabilityRecords.Add(record);
            Debug.Log($"Adaptability Record Created - ID: {record.RecordID}, Content: {content}, Adjustment: {adjustmentDetails}");
        }

        // Catalog data into the Master Database
        private void CatalogData(DataPacket packet)
        {
            Debug.Log($"Cataloging Packet into Master Database - ID: {packet.PacketID}, Content: {packet.Content}");
            outputChannel.Enqueue(packet); // Simulate moving analyzed packets to the output channel
        }

        // Add a packet to the input channel
        public void AddDataPacket(string content, string direction)
        {
            DataPacket packet = new DataPacket(content, direction);
            if (direction == "Input")
            {
                inputChannel.Enqueue(packet);
            }
            else if (direction == "Output")
            {
                outputChannel.Enqueue(packet);
            }
            else
            {
                Debug.LogWarning("Invalid packet direction specified.");
            }

            Debug.Log($"Data Packet Added - ID: {packet.PacketID}, Content: {content}, Direction: {direction}");
        }

        // Process the next packet in the input channel
        private void ProcessNextPacket()
        {
            if (inputChannel.Count > 0)
            {
                DataPacket packet = inputChannel.Dequeue();
                AnalyzeDataPacket(packet);
            }
            else
            {
                Debug.LogWarning("No data packets to process in the input channel.");
            }
        }

        // Display adaptability records
        private void DisplayAdaptabilityRecords()
        {
            Debug.Log("Displaying all adaptability records...");
            foreach (var record in adaptabilityRecords)
            {
                Debug.Log($"Record ID: {record.RecordID}, Content: {record.Content}, Adjustment: {record.AdjustmentDetails}");
            }
        }

        private void Start()
        {
            Debug.Log("System Administration Analysis System Initialized.");

            // Automate actions
            InvokeRepeating(nameof(AutoGenerateInputPacket), 2f, 5f); // Generate input packets every 5 seconds
            InvokeRepeating(nameof(AutoGenerateOutputPacket), 3f, 7f); // Generate output packets every 7 seconds
            InvokeRepeating(nameof(ProcessNextPacket), 4f, 6f); // Process packets every 6 seconds
            InvokeRepeating(nameof(DisplayAdaptabilityRecords), 10f, 15f); // Display records every 15 seconds
        }

        // Automatically generate an input packet
        private void AutoGenerateInputPacket()
        {
            AddDataPacket("Routine Input Data", "Input");
        }

        // Automatically generate an output packet
        private void AutoGenerateOutputPacket()
        {
            AddDataPacket("Routine Output Data", "Output");
        }
    }
}
