using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixEmulationSystem : MonoBehaviour
    {
        // Represents a single emulator in the system
        private class MatrixEmulator
        {
            public string EmulatorID { get; private set; }
            public string TaskDescription { get; private set; }
            public string ControlledSystem { get; private set; }
            public bool IsRunning { get; private set; }

            public MatrixEmulator(string taskDescription, string controlledSystem)
            {
                EmulatorID = System.Guid.NewGuid().ToString();
                TaskDescription = taskDescription;
                ControlledSystem = controlledSystem;
                IsRunning = false;
            }

            public void Start()
            {
                IsRunning = true;
                Debug.Log($"Emulator '{EmulatorID}' started task: {TaskDescription} on system: {ControlledSystem}");
            }

            public void Stop()
            {
                IsRunning = false;
                Debug.Log($"Emulator '{EmulatorID}' stopped.");
            }
        }

        // List to store all active emulators
        private List<MatrixEmulator> emulators = new List<MatrixEmulator>();

        // Timers for automation
        private float addEmulatorTimer = 0f;
        private float startEmulatorTimer = 0f;
        private float stopEmulatorTimer = 0f;
        private float retrieveStatusTimer = 0f;

        // Automation intervals
        private const float addEmulatorInterval = 5f; // Add an emulator every 5 seconds
        private const float startEmulatorInterval = 7f; // Start emulators every 7 seconds
        private const float stopEmulatorInterval = 10f; // Stop emulators every 10 seconds
        private const float retrieveStatusInterval = 12f; // Retrieve statuses every 12 seconds

        // Add a new emulator
        public void AddEmulator(string taskDescription, string controlledSystem)
        {
            MatrixEmulator emulator = new MatrixEmulator(taskDescription, controlledSystem);
            emulators.Add(emulator);
            Debug.Log($"Emulator Added - ID: {emulator.EmulatorID}, Task: {taskDescription}, System: {controlledSystem}");
        }

        // Start a specific emulator
        public void StartEmulator(string emulatorID)
        {
            foreach (var emulator in emulators)
            {
                if (emulator.EmulatorID == emulatorID)
                {
                    if (!emulator.IsRunning)
                    {
                        emulator.Start();
                    }
                    else
                    {
                        Debug.LogWarning($"Emulator '{emulatorID}' is already running.");
                    }
                    return;
                }
            }
            Debug.LogWarning($"Emulator with ID '{emulatorID}' not found.");
        }

        // Stop a specific emulator
        public void StopEmulator(string emulatorID)
        {
            foreach (var emulator in emulators)
            {
                if (emulator.EmulatorID == emulatorID)
                {
                    if (emulator.IsRunning)
                    {
                        emulator.Stop();
                    }
                    else
                    {
                        Debug.LogWarning($"Emulator '{emulatorID}' is not running.");
                    }
                    return;
                }
            }
            Debug.LogWarning($"Emulator with ID '{emulatorID}' not found.");
        }

        // Retrieve status of all emulators
        public void RetrieveEmulatorStatus()
        {
            Debug.Log("Retrieving status of all emulators...");
            foreach (var emulator in emulators)
            {
                Debug.Log($"Emulator - ID: {emulator.EmulatorID}, Task: {emulator.TaskDescription}, System: {emulator.ControlledSystem}, Running: {emulator.IsRunning}");
            }
        }

        // Control all emulators
        public void ControlAllEmulators(bool start)
        {
            Debug.Log($"{(start ? "Starting" : "Stopping")} all emulators...");
            foreach (var emulator in emulators)
            {
                if (start && !emulator.IsRunning)
                {
                    emulator.Start();
                }
                else if (!start && emulator.IsRunning)
                {
                    emulator.Stop();
                }
            }
        }

        private void Start()
        {
            Debug.Log("Matrix Emulation System Initialized.");
        }

        private void Update()
        {
            // Increment timers
            addEmulatorTimer += Time.deltaTime;
            startEmulatorTimer += Time.deltaTime;
            stopEmulatorTimer += Time.deltaTime;
            retrieveStatusTimer += Time.deltaTime;

            // Automate adding emulators
            if (addEmulatorTimer >= addEmulatorInterval)
            {
                AddEmulator($"Task_{emulators.Count + 1}", $"System_{emulators.Count + 1}");
                addEmulatorTimer = 0f;
            }

            // Automate starting emulators
            if (startEmulatorTimer >= startEmulatorInterval && emulators.Count > 0)
            {
                foreach (var emulator in emulators)
                {
                    if (!emulator.IsRunning)
                    {
                        emulator.Start();
                        break; // Start one emulator at a time
                    }
                }
                startEmulatorTimer = 0f;
            }

            // Automate stopping emulators
            if (stopEmulatorTimer >= stopEmulatorInterval && emulators.Count > 0)
            {
                foreach (var emulator in emulators)
                {
                    if (emulator.IsRunning)
                    {
                        emulator.Stop();
                        break; // Stop one emulator at a time
                    }
                }
                stopEmulatorTimer = 0f;
            }

            // Automate retrieving statuses
            if (retrieveStatusTimer >= retrieveStatusInterval)
            {
                RetrieveEmulatorStatus();
                retrieveStatusTimer = 0f;
            }
        }
    }
}
