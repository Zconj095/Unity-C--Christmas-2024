using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixSimulationCustomInterface : MonoBehaviour
    {
        // Enum to represent available options
        public enum SimulationOption
        {
            Reconnection,
            CreateSensors,
            EmotionalEditor,
            SimulationCloning,
            FrequencyControl,
            DestructionModifier,
            CreationModifier,
            CreationSystem,
            DestructionSystem,
            EditingManipulator,
            NetworkConnector,
            ServerAdjustment,
            DomainModifying
        }

        // Custom settings for each simulation option
        private class OptionSettings
        {
            public SimulationOption Option { get; private set; }
            public Dictionary<string, string> Settings { get; private set; }

            public OptionSettings(SimulationOption option)
            {
                Option = option;
                Settings = new Dictionary<string, string>();
            }

            public void UpdateSetting(string key, string value)
            {
                Settings[key] = value;
                Debug.Log($"Option '{Option}' updated setting '{key}' to '{value}'.");
            }

            public void DisplaySettings()
            {
                Debug.Log($"Settings for Option: {Option}");
                foreach (var setting in Settings)
                {
                    Debug.Log($"  {setting.Key}: {setting.Value}");
                }
            }
        }

        // Dictionary to hold all options and their settings
        private Dictionary<SimulationOption, OptionSettings> simulationOptions = new Dictionary<SimulationOption, OptionSettings>();

        // Timer for automated setting updates
        private float settingsUpdateTimer = 0f;
        private const float settingsUpdateInterval = 5f; // Update settings every 5 seconds

        // Initialize all options with default settings
        private void InitializeOptions()
        {
            foreach (SimulationOption option in System.Enum.GetValues(typeof(SimulationOption)))
            {
                simulationOptions[option] = new OptionSettings(option);
                Debug.Log($"Initialized Option: {option}");
            }
        }

        // Update a specific setting for an option
        private void UpdateOptionSetting(SimulationOption option, string key, string value)
        {
            if (simulationOptions.ContainsKey(option))
            {
                simulationOptions[option].UpdateSetting(key, value);
            }
            else
            {
                Debug.LogWarning($"Option '{option}' not found.");
            }
        }

        // Display all options and their settings
        private void DisplayAllOptions()
        {
            Debug.Log("Displaying all simulation options and their settings...");
            foreach (var option in simulationOptions.Values)
            {
                option.DisplaySettings();
            }
        }

        private void Start()
        {
            Debug.Log("Matrix Simulation Custom Interface Initialized.");
            InitializeOptions();
        }

        private void Update()
        {
            // Increment timer
            settingsUpdateTimer += Time.deltaTime;

            // Automate settings updates every interval
            if (settingsUpdateTimer >= settingsUpdateInterval)
            {
                AutomateOptionUpdates();
                settingsUpdateTimer = 0f; // Reset timer
            }
        }

        // Automated updates for selected options
        private void AutomateOptionUpdates()
        {
            Debug.Log("Automating simulation option updates...");

            // Example automated updates
            UpdateOptionSetting(SimulationOption.FrequencyControl, "Max Frequency", $"{Random.Range(50, 200)} Hz");
            UpdateOptionSetting(SimulationOption.CreationModifier, "Creation Rate", $"{Random.Range(1, 100)} units/s");
            UpdateOptionSetting(SimulationOption.DestructionSystem, "Destruction Threshold", $"{Random.Range(0, 500)}");

            // Display updated settings
            DisplayAllOptions();
        }
    }
}
