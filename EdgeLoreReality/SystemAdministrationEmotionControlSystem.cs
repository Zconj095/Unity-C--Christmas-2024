using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class SystemAdministrationEmotionControlSystem : MonoBehaviour
    {
        // Represents an emotional sensory input
        private class EmotionalInput
        {
            public string InputID { get; private set; }
            public string Description { get; private set; }
            public float EmotionalWillpowerRating { get; private set; } // Scale of 0 to 100
            public bool IsSafe { get; private set; }

            public EmotionalInput(string description, float willpowerRating)
            {
                InputID = System.Guid.NewGuid().ToString();
                Description = description;
                EmotionalWillpowerRating = willpowerRating;
                IsSafe = willpowerRating >= 50; // Example threshold for safety
            }

            public void ModifyForSafety()
            {
                if (!IsSafe)
                {
                    EmotionalWillpowerRating = 50; // Adjust to safe threshold
                    IsSafe = true;
                    Debug.Log($"Input '{Description}' modified to a safe Emotional Willpower Rating: {EmotionalWillpowerRating}");
                }
            }

            public void Display()
            {
                Debug.Log($"Input ID: {InputID}, Description: {Description}, Willpower Rating: {EmotionalWillpowerRating}, IsSafe: {IsSafe}");
            }
        }

        // List of emotional inputs
        private List<EmotionalInput> emotionalInputs = new List<EmotionalInput>();

        // Add a new emotional input
        private void AddEmotionalInput(string description, float willpowerRating)
        {
            EmotionalInput input = new EmotionalInput(description, willpowerRating);
            emotionalInputs.Add(input);
            Debug.Log($"New Emotional Input Added - ID: {input.InputID}, Description: {description}, Willpower Rating: {willpowerRating}, Safe: {input.IsSafe}");
        }

        // Analyze all inputs for safety
        private void AnalyzeInputs()
        {
            Debug.Log("Analyzing all emotional inputs for safety...");
            foreach (var input in emotionalInputs)
            {
                if (!input.IsSafe)
                {
                    Debug.LogWarning($"Unsafe Emotional Input Detected - ID: {input.InputID}, Description: {input.Description}");
                    input.ModifyForSafety();
                }
            }
        }

        // Send safe inputs to the Master Database Network
        private void SendToMasterDatabase()
        {
            Debug.Log("Sending safe emotional inputs to the Master Database Network...");
            foreach (var input in emotionalInputs)
            {
                if (input.IsSafe)
                {
                    Debug.Log($"Input '{input.Description}' with Willpower Rating: {input.EmotionalWillpowerRating} sent to the database.");
                }
            }
        }

        // Display all emotional inputs
        private void DisplayInputs()
        {
            Debug.Log("Displaying all emotional inputs...");
            foreach (var input in emotionalInputs)
            {
                input.Display();
            }
        }

        private void Start()
        {
            Debug.Log("System Administration Emotion Control System Initialized.");

            // Initial setup
            AddEmotionalInput("Calmness Sensory Input", 70);
            AddEmotionalInput("Anxiety Spike", 30);
            AddEmotionalInput("Euphoria Pulse", 85);

            // Schedule automated actions
            InvokeRepeating(nameof(AutoAddEmotionalInput), 2f, 5f); // Add a new input every 5 seconds
            InvokeRepeating(nameof(AnalyzeInputs), 3f, 10f); // Analyze inputs for safety every 10 seconds
            InvokeRepeating(nameof(SendToMasterDatabase), 5f, 15f); // Send safe inputs every 15 seconds
            InvokeRepeating(nameof(DisplayInputs), 7f, 20f); // Display inputs every 20 seconds
        }

        private void AutoAddEmotionalInput()
        {
            string[] descriptions = { "Joy Surge", "Stress Pulse", "Focus State", "Panic Wave", "Contentment Stream" };
            string description = descriptions[Random.Range(0, descriptions.Length)];
            float willpowerRating = Random.Range(20f, 100f);

            AddEmotionalInput(description, willpowerRating);
        }
    }
}
