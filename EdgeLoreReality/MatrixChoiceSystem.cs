using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixChoiceSystem : MonoBehaviour
    {
        // Represents a choice or option within the system
        private class MatrixChoice
        {
            public string ChoiceID { get; private set; }
            public string Description { get; private set; }
            public bool IsValid { get; private set; }

            public MatrixChoice(string choiceID, string description, bool isValid)
            {
                ChoiceID = choiceID;
                Description = description;
                IsValid = isValid;
            }
        }

        // Stores all available choices
        private Dictionary<string, MatrixChoice> choiceDatabase = new Dictionary<string, MatrixChoice>();

        // Timers for automation
        private float addChoiceTimer = 0f;
        private float retrieveChoiceTimer = 0f;
        private float validateChoiceTimer = 0f;
        private float handleChoiceTimer = 0f;

        // Automation intervals
        private const float addChoiceInterval = 5f; // Add a new choice every 5 seconds
        private const float retrieveChoiceInterval = 7f; // Retrieve the latest choice every 7 seconds
        private const float validateChoiceInterval = 10f; // Validate the latest choice every 10 seconds
        private const float handleChoiceInterval = 12f; // Handle the latest choice every 12 seconds

        // Add a new choice
        public void AddChoice(string description, bool isValid)
        {
            string choiceID = System.Guid.NewGuid().ToString();
            MatrixChoice choice = new MatrixChoice(choiceID, description, isValid);
            choiceDatabase[choiceID] = choice;

            Debug.Log($"Added Choice - ID: {choiceID}, Description: {description}, Valid: {isValid}");
        }

        // Retrieve a choice by ID
        public void RetrieveChoice(string choiceID)
        {
            if (choiceDatabase.TryGetValue(choiceID, out MatrixChoice choice))
            {
                Debug.Log($"Retrieved Choice - ID: {choice.ChoiceID}, Description: {choice.Description}, Valid: {choice.IsValid}");
            }
            else
            {
                Debug.LogWarning($"Choice with ID '{choiceID}' not found.");
            }
        }

        // Validate choice stability
        public bool ValidateChoiceStability(string choiceID)
        {
            if (choiceDatabase.TryGetValue(choiceID, out MatrixChoice choice))
            {
                if (choice.IsValid)
                {
                    Debug.Log($"Choice '{choiceID}' is stable.");
                    return true;
                }
                else
                {
                    Debug.LogWarning($"Choice '{choiceID}' is unstable.");
                }
            }
            else
            {
                Debug.LogWarning($"Choice '{choiceID}' does not exist.");
            }

            return false;
        }

        // Handle tasks based on choices
        public void HandleChoice(string choiceID)
        {
            if (ValidateChoiceStability(choiceID))
            {
                Debug.Log($"Handling task for choice ID: {choiceID}");
                // Add further handling logic here
            }
            else
            {
                Debug.LogWarning("Task cannot be handled due to choice instability.");
            }
        }

        private void Start()
        {
            Debug.Log("Matrix Choice System Initialized.");
        }

        private void Update()
        {
            // Increment timers
            addChoiceTimer += Time.deltaTime;
            retrieveChoiceTimer += Time.deltaTime;
            validateChoiceTimer += Time.deltaTime;
            handleChoiceTimer += Time.deltaTime;

            // Automate adding a new choice
            if (addChoiceTimer >= addChoiceInterval)
            {
                AddChoice("Automated Choice: Enable Matrix Optimization", Random.value > 0.5f);
                addChoiceTimer = 0f;
            }

            // Automate retrieving the latest choice
            if (retrieveChoiceTimer >= retrieveChoiceInterval && choiceDatabase.Count > 0)
            {
                string lastKey = GetLastKey();
                RetrieveChoice(lastKey);
                retrieveChoiceTimer = 0f;
            }

            // Automate validating the latest choice
            if (validateChoiceTimer >= validateChoiceInterval && choiceDatabase.Count > 0)
            {
                string lastKey = GetLastKey();
                ValidateChoiceStability(lastKey);
                validateChoiceTimer = 0f;
            }

            // Automate handling the latest choice
            if (handleChoiceTimer >= handleChoiceInterval && choiceDatabase.Count > 0)
            {
                string lastKey = GetLastKey();
                HandleChoice(lastKey);
                handleChoiceTimer = 0f;
            }
        }

        // Get the last added choice ID
        private string GetLastKey()
        {
            string lastKey = "";
            foreach (var key in choiceDatabase.Keys) lastKey = key;
            return lastKey;
        }
    }
}
