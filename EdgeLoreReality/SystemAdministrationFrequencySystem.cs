using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class SystemAdministrationFrequencySystem : MonoBehaviour
    {
        // Represents a frequency channel
        private class FrequencyChannel
        {
            public string ChannelID { get; private set; }
            public string Name { get; private set; }
            public float Frequency { get; private set; } // Measured in Hz
            public string Purpose { get; private set; }

            public FrequencyChannel(string name, float frequency, string purpose)
            {
                ChannelID = System.Guid.NewGuid().ToString();
                Name = name;
                Frequency = frequency;
                Purpose = purpose;
            }

            public void UpdateFrequency(float newFrequency)
            {
                Frequency = newFrequency;
                Debug.Log($"Frequency Channel '{Name}' updated to {Frequency} Hz.");
            }

            public void Display()
            {
                Debug.Log($"Channel ID: {ChannelID}, Name: {Name}, Frequency: {Frequency} Hz, Purpose: {Purpose}");
            }
        }

        // List of frequency channels
        private List<FrequencyChannel> frequencyChannels = new List<FrequencyChannel>();

        // Add a new frequency channel
        private void AddFrequencyChannel(string name, float frequency, string purpose)
        {
            FrequencyChannel newChannel = new FrequencyChannel(name, frequency, purpose);
            frequencyChannels.Add(newChannel);
            Debug.Log($"Frequency Channel Added - ID: {newChannel.ChannelID}, Name: {name}, Frequency: {frequency} Hz, Purpose: {purpose}");
        }

        // Edit an existing frequency channel
        private void EditFrequencyChannel(string channelID, float newFrequency)
        {
            FrequencyChannel channel = frequencyChannels.Find(c => c.ChannelID == channelID);
            if (channel != null)
            {
                channel.UpdateFrequency(newFrequency);
            }
            else
            {
                Debug.LogWarning($"Frequency Channel with ID '{channelID}' not found.");
            }
        }

        // Display all frequency channels
        private void DisplayFrequencyChannels()
        {
            Debug.Log("Displaying all frequency channels...");
            foreach (var channel in frequencyChannels)
            {
                channel.Display();
            }
        }

        // Automated actions
        private void AutoAddFrequencyChannel()
        {
            string[] purposes = { "Matrix Operations", "System Synchronization", "Signal Management", "Data Transmission" };
            string name = $"Channel {Random.Range(100, 999)}";
            float frequency = Random.Range(20f, 100f);
            string purpose = purposes[Random.Range(0, purposes.Length)];

            AddFrequencyChannel(name, frequency, purpose);
        }

        private void AutoEditFrequencyChannel()
        {
            if (frequencyChannels.Count > 0)
            {
                int index = Random.Range(0, frequencyChannels.Count);
                float newFrequency = Random.Range(20f, 100f);
                EditFrequencyChannel(frequencyChannels[index].ChannelID, newFrequency);
            }
            else
            {
                Debug.LogWarning("No frequency channels available to edit.");
            }
        }

        private void AutoDisplayFrequencyChannels()
        {
            DisplayFrequencyChannels();
        }

        private void Start()
        {
            Debug.Log("System Administration Frequency System Initialized.");

            // Initial setup
            AddFrequencyChannel("Channel Alpha", 27f, "Matrix Cortex Operations");
            AddFrequencyChannel("Channel Beta", 42f, "Infinity Matrix Synchronization");

            // Schedule automated actions
            InvokeRepeating(nameof(AutoAddFrequencyChannel), 2f, 10f); // Add a new channel every 10 seconds
            InvokeRepeating(nameof(AutoEditFrequencyChannel), 5f, 15f); // Edit a random channel every 15 seconds
            InvokeRepeating(nameof(AutoDisplayFrequencyChannels), 10f, 20f); // Display channels every 20 seconds
        }
    }
}
