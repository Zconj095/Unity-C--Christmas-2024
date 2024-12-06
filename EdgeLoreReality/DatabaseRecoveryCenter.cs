using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace edgelorereality
{
    public class DatabaseRecoveryCenter : MonoBehaviour
    {
        // Constants
        private const float AdaptationFrequency = 7.6f; // Hz for frequency chamber access
        private const float CommandFrequency = 10f; // Base command access frequency

        // Storage for galactic data
        private Dictionary<string, string> galacticStorage = new Dictionary<string, string>();
        private List<string> compressedGhostFiles = new List<string>();
        private List<string> virusScannedFiles = new List<string>();

        // Firewalls and barriers
        private bool isBarrierActive = false;

        // Timers for automation
        private float storeDataTimer = 0f;
        private float retrieveDataTimer = 0f;
        private float scanVirusTimer = 0f;
        private float barrierToggleTimer = 0f;
        private float frequencyAccessTimer = 0f;

        // Automation intervals
        private const float storeDataInterval = 5f; // Every 5 seconds
        private const float retrieveDataInterval = 7f; // Every 7 seconds
        private const float scanVirusInterval = 10f; // Every 10 seconds
        private const float barrierToggleInterval = 12f; // Every 12 seconds
        private const float frequencyAccessInterval = 15f; // Every 15 seconds

        // Methods to handle data storage
        public void StoreData(string filename, string data)
        {
            if (isBarrierActive)
            {
                Debug.LogWarning("Barrier is active. Cannot store data.");
                return;
            }

            // Compress and add as a ghost file
            string compressedData = CompressData(data);
            compressedGhostFiles.Add(filename);
            galacticStorage[filename] = compressedData;

            Debug.Log($"Stored data: {filename}, Compressed.");
        }

        public string RetrieveData(string filename)
        {
            if (galacticStorage.ContainsKey(filename))
            {
                string decompressedData = DecompressData(galacticStorage[filename]);
                Debug.Log($"Retrieved and decompressed data: {filename}");
                return decompressedData;
            }
            else
            {
                Debug.LogWarning($"File '{filename}' not found in storage.");
                return null;
            }
        }

        // Compression and decompression
        private string CompressData(string data)
        {
            return new string(data.Reverse().ToArray()); // Simulate compression
        }

        private string DecompressData(string compressedData)
        {
            return new string(compressedData.Reverse().ToArray()); // Simulate decompression
        }

        // Virus scanning
        public bool ScanForViruses(string data)
        {
            bool hasVirus = data.Contains("virus"); // Simple simulation
            if (hasVirus)
            {
                Debug.LogWarning("Virus detected in data!");
                return false;
            }

            virusScannedFiles.Add(data);
            Debug.Log("Data is clean.");
            return true;
        }

        // Barriers
        public void ActivateBarrier()
        {
            isBarrierActive = true;
            Debug.Log("Barrier activated. Data input restricted.");
        }

        public void DeactivateBarrier()
        {
            isBarrierActive = false;
            Debug.Log("Barrier deactivated. Data input unrestricted.");
        }

        // Command center access
        public void AccessFrequencyChamber(float frequency)
        {
            if (Mathf.Approximately(frequency, AdaptationFrequency))
            {
                Debug.Log("Frequency Chamber accessed. Accepting commands.");
            }
            else
            {
                Debug.LogWarning("Invalid frequency for chamber access.");
            }
        }

        private void Start()
        {
            Debug.Log("Database Recovery Center Initialized.");
        }

        private void Update()
        {
            // Timers for automation
            storeDataTimer += Time.deltaTime;
            retrieveDataTimer += Time.deltaTime;
            scanVirusTimer += Time.deltaTime;
            barrierToggleTimer += Time.deltaTime;
            frequencyAccessTimer += Time.deltaTime;

            // Store data periodically
            if (storeDataTimer >= storeDataInterval)
            {
                StoreData("example.txt", "This is some example data.");
                storeDataTimer = 0f;
            }

            // Retrieve data periodically
            if (retrieveDataTimer >= retrieveDataInterval)
            {
                RetrieveData("example.txt");
                retrieveDataTimer = 0f;
            }

            // Scan for viruses periodically
            if (scanVirusTimer >= scanVirusInterval)
            {
                ScanForViruses("This data is clean.");
                scanVirusTimer = 0f;
            }

            // Toggle barriers periodically
            if (barrierToggleTimer >= barrierToggleInterval)
            {
                if (isBarrierActive)
                {
                    DeactivateBarrier();
                }
                else
                {
                    ActivateBarrier();
                }
                barrierToggleTimer = 0f;
            }

            // Access frequency chamber periodically
            if (frequencyAccessTimer >= frequencyAccessInterval)
            {
                AccessFrequencyChamber(AdaptationFrequency);
                frequencyAccessTimer = 0f;
            }
        }
    }
}
