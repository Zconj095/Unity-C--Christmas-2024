using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixSimulationEditor : MonoBehaviour
    {
        // Represents a setting in the editor
        private class EditorSetting
        {
            public string Name { get; private set; }
            public float Value { get; private set; }

            public EditorSetting(string name, float initialValue)
            {
                Name = name;
                Value = initialValue;
            }

            public void UpdateValue(float newValue)
            {
                Value = Mathf.Clamp(newValue, 0f, 100f); // Sensitivity clamped between 0 and 100
                Debug.Log($"Setting '{Name}' updated to: {Value}");
            }
        }

        // Represents a command made in the editor
        private class EditorCommand
        {
            public string CommandID { get; private set; }
            public string CommandText { get; private set; }

            public EditorCommand(string commandText)
            {
                CommandID = System.Guid.NewGuid().ToString();
                CommandText = commandText;
            }
        }

        // Settings and commands
        private Dictionary<string, EditorSetting> settings = new Dictionary<string, EditorSetting>();
        private List<EditorCommand> commands = new List<EditorCommand>();

        // Automated task timer
        private float taskTimer = 0f;
        private const float taskInterval = 3f; // Interval for automated tasks

        // Add a new setting to the editor
        public void AddSetting(string name, float initialValue)
        {
            if (settings.ContainsKey(name))
            {
                Debug.LogWarning($"Setting '{name}' already exists.");
                return;
            }

            settings[name] = new EditorSetting(name, initialValue);
            Debug.Log($"Setting Added: {name} = {initialValue}");
        }

        // Update a setting's value
        public void UpdateSetting(string name, float newValue)
        {
            if (settings.ContainsKey(name))
            {
                settings[name].UpdateValue(newValue);
            }
            else
            {
                Debug.LogWarning($"Setting '{name}' not found.");
            }
        }

        // Execute a command in the editor
        public void ExecuteCommand(string commandText)
        {
            EditorCommand command = new EditorCommand(commandText);
            commands.Add(command);

            Debug.Log($"Command Executed - ID: {command.CommandID}, Text: {command.CommandText}");

            // Simulate transferring power from the Infinity Matrix
            TransferPowerToEditor(commandText);
        }

        // Simulate power transfer from the Infinity Matrix
        private void TransferPowerToEditor(string commandText)
        {
            float powerConsumption = CalculatePower(commandText);
            Debug.Log($"Power Transferred from Infinity Matrix: {powerConsumption} units for command '{commandText}'");
        }

        // Calculate power consumption for a command
        private float CalculatePower(string commandText)
        {
            float basePower = commandText.Length * 2f; // Power based on command length
            float sensitivity = settings.ContainsKey("Sensitivity") ? settings["Sensitivity"].Value : 50f; // Default sensitivity
            return basePower * (sensitivity / 100f);
        }

        // Revert a command
        public void RevertCommand(string commandID)
        {
            EditorCommand command = commands.Find(c => c.CommandID == commandID);
            if (command != null)
            {
                commands.Remove(command);
                Debug.Log($"Command Reverted - ID: {commandID}, Text: {command.CommandText}");
            }
            else
            {
                Debug.LogWarning($"Command with ID '{commandID}' not found.");
            }
        }

        // Display all settings
        public void DisplaySettings()
        {
            Debug.Log("Displaying all editor settings...");
            foreach (var setting in settings.Values)
            {
                Debug.Log($"Setting: {setting.Name} = {setting.Value}");
            }
        }

        // Display all commands
        public void DisplayCommands()
        {
            Debug.Log("Displaying all executed commands...");
            foreach (var command in commands)
            {
                Debug.Log($"Command - ID: {command.CommandID}, Text: {command.CommandText}");
            }
        }

        private void Start()
        {
            Debug.Log("Automated Matrix Simulation Editor Initialized.");

            // Initialize default settings
            AddSetting("Sensitivity", 50f); // Default sensitivity
        }

        private void Update()
        {
            // Increment the task timer
            taskTimer += Time.deltaTime;

            // Perform automated tasks at regular intervals
            if (taskTimer >= taskInterval)
            {
                PerformAutomatedTasks();
                taskTimer = 0f; // Reset the timer
            }
        }

        // Perform automated editor tasks
        private void PerformAutomatedTasks()
        {
            Debug.Log("Performing automated editor tasks...");

            // Execute automated commands dynamically
            ExecuteCommand($"AutoCommand_{commands.Count}");

            // Update sensitivity randomly
            if (Random.value > 0.5f)
            {
                UpdateSetting("Sensitivity", Random.Range(10f, 90f));
            }

            // Revert the oldest command if the list exceeds a certain size
            if (commands.Count > 10)
            {
                RevertCommand(commands[0].CommandID);
            }

            // Display the current state
            DisplaySettings();
            DisplayCommands();
        }
    }
}
