using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixAtomizerCode : MonoBehaviour
    {
        // Represents an atom with serial input/output and frequency
        private class AtomCommand
        {
            public string SerialCode { get; private set; }
            public float Frequency { get; private set; }
            public string DigitalPixelData { get; private set; }

            public AtomCommand(string serialCode, float frequency, string digitalPixelData)
            {
                SerialCode = serialCode;
                Frequency = frequency;
                DigitalPixelData = digitalPixelData;
            }
        }

        // Dictionary to manage atom commands
        private Dictionary<string, AtomCommand> atomCommands = new Dictionary<string, AtomCommand>();

        // Timers for automation
        private float createCommandTimer = 0f;
        private float retrieveCommandTimer = 0f;
        private float manipulateFieldTimer = 0f;

        // Automation intervals
        private const float createCommandInterval = 5f; // Create a new command every 5 seconds
        private const float retrieveCommandInterval = 7f; // Retrieve the last command every 7 seconds
        private const float manipulateFieldInterval = 10f; // Manipulate field frequency every 10 seconds

        // Generate a unique serial code
        private string GenerateSerialCode()
        {
            return System.Guid.NewGuid().ToString();
        }

        // Calculate frequency based on serial code
        private float CalculateFrequency(string serialCode)
        {
            int hash = serialCode.GetHashCode();
            return Mathf.Abs((hash % 100) + 10) / 10.0f; // Generate frequency in range [1, 10] Hz
        }

        // Generate pixel data for atom manipulation
        private string GeneratePixelData()
        {
            int pixelValue = Random.Range(0, 256); // Simulate an 8-bit grayscale pixel value
            return $"PixelValue: {pixelValue}";
        }

        // Create an Atomizer Code command
        public void CreateAtomizerCommand()
        {
            string serialCode = GenerateSerialCode();
            float frequency = CalculateFrequency(serialCode);
            string pixelData = GeneratePixelData();

            AtomCommand command = new AtomCommand(serialCode, frequency, pixelData);
            atomCommands[serialCode] = command;

            Debug.Log($"Atomizer Command Created - Serial Code: {serialCode}, Frequency: {frequency} Hz, Pixel Data: {pixelData}");
        }

        // Retrieve command by serial code
        public void GetAtomizerCommand(string serialCode)
        {
            if (atomCommands.TryGetValue(serialCode, out AtomCommand command))
            {
                Debug.Log($"Retrieved Atomizer Command - Serial Code: {command.SerialCode}, Frequency: {command.Frequency} Hz, Pixel Data: {command.DigitalPixelData}");
            }
            else
            {
                Debug.LogWarning($"No Atomizer Command found with Serial Code: {serialCode}");
            }
        }

        // Retrieve the last created atomizer command
        private void RetrieveLastCommand()
        {
            if (atomCommands.Count > 0)
            {
                string lastKey = "";
                foreach (var key in atomCommands.Keys) lastKey = key; // Get last key
                GetAtomizerCommand(lastKey);
            }
            else
            {
                Debug.LogWarning("No atomizer commands available.");
            }
        }

        // Simulate field frequency manipulation
        public void ManipulateFieldFrequency(string serialCode)
        {
            if (atomCommands.TryGetValue(serialCode, out AtomCommand command))
            {
                Debug.Log($"Manipulating Field Frequency for Serial Code: {serialCode} at Frequency: {command.Frequency} Hz");
                Debug.Log($"Using Digital Pixel Data: {command.DigitalPixelData}");
            }
            else
            {
                Debug.LogWarning($"No Atomizer Command found with Serial Code: {serialCode}");
            }
        }

        private void ManipulateLastCommandField()
        {
            if (atomCommands.Count > 0)
            {
                string lastKey = "";
                foreach (var key in atomCommands.Keys) lastKey = key; // Get last key
                ManipulateFieldFrequency(lastKey);
            }
            else
            {
                Debug.LogWarning("No atomizer commands available.");
            }
        }

        private void Start()
        {
            Debug.Log("Matrix Atomizer Code Initialized.");
        }

        private void Update()
        {
            // Increment timers
            createCommandTimer += Time.deltaTime;
            retrieveCommandTimer += Time.deltaTime;
            manipulateFieldTimer += Time.deltaTime;

            // Automate creating commands
            if (createCommandTimer >= createCommandInterval)
            {
                CreateAtomizerCommand();
                createCommandTimer = 0f;
            }

            // Automate retrieving the last command
            if (retrieveCommandTimer >= retrieveCommandInterval)
            {
                RetrieveLastCommand();
                retrieveCommandTimer = 0f;
            }

            // Automate manipulating field frequency
            if (manipulateFieldTimer >= manipulateFieldInterval)
            {
                ManipulateLastCommandField();
                manipulateFieldTimer = 0f;
            }
        }
    }
}
