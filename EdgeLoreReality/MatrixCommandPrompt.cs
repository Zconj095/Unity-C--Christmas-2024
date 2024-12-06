using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixCommandPrompt : MonoBehaviour
    {
        // Represents a command in the Matrix Command Prompt
        private class MatrixCommand
        {
            public string CommandID { get; private set; }
            public string CommandText { get; private set; }
            public float RequiredEnergy { get; private set; }

            public MatrixCommand(string commandText, float requiredEnergy)
            {
                CommandID = System.Guid.NewGuid().ToString();
                CommandText = commandText;
                RequiredEnergy = requiredEnergy;
            }
        }

        // Word system energy source
        private float availableEnergy = 100f; // Example starting energy
        private List<MatrixCommand> commandQueue = new List<MatrixCommand>();

        // Timers for automation
        private float addCommandTimer = 0f;
        private float executeCommandTimer = 0f;
        private float replenishEnergyTimer = 0f;

        // Automation intervals
        private const float addCommandInterval = 5f; // Add a new command every 5 seconds
        private const float executeCommandInterval = 7f; // Execute a command every 7 seconds
        private const float replenishEnergyInterval = 10f; // Replenish energy every 10 seconds

        // Add a command to the system
        public void AddCommand(string commandText, float requiredEnergy)
        {
            if (requiredEnergy > availableEnergy)
            {
                Debug.LogWarning("Not enough energy to execute the command.");
                return;
            }

            MatrixCommand command = new MatrixCommand(commandText, requiredEnergy);
            commandQueue.Add(command);

            Debug.Log($"Command added - ID: {command.CommandID}, Text: {commandText}, Required Energy: {requiredEnergy}");
        }

        // Execute the next command in the queue
        public void ExecuteCommand()
        {
            if (commandQueue.Count > 0)
            {
                MatrixCommand command = commandQueue[0];
                commandQueue.RemoveAt(0);

                availableEnergy -= command.RequiredEnergy;
                Debug.Log($"Executed Command - ID: {command.CommandID}, Text: {command.CommandText}, Remaining Energy: {availableEnergy}");
            }
            else
            {
                Debug.LogWarning("No commands in the queue to execute.");
            }
        }

        // Replenish energy from the Word System
        public void ReplenishEnergy(float energyAmount)
        {
            availableEnergy += energyAmount;
            Debug.Log($"Energy replenished by {energyAmount}. Available Energy: {availableEnergy}");
        }

        private void Start()
        {
            Debug.Log("Matrix Command Prompt Initialized.");
        }

        private void Update()
        {
            // Increment timers
            addCommandTimer += Time.deltaTime;
            executeCommandTimer += Time.deltaTime;
            replenishEnergyTimer += Time.deltaTime;

            // Automate adding commands
            if (addCommandTimer >= addCommandInterval)
            {
                // Random energy requirement for commands
                float requiredEnergy = Random.Range(10f, 50f);
                AddCommand($"Automated Command - Energy {requiredEnergy}", requiredEnergy);
                addCommandTimer = 0f;
            }

            // Automate executing commands
            if (executeCommandTimer >= executeCommandInterval)
            {
                ExecuteCommand();
                executeCommandTimer = 0f;
            }

            // Automate replenishing energy
            if (replenishEnergyTimer >= replenishEnergyInterval)
            {
                // Random energy replenishment
                float energyAmount = Random.Range(20f, 40f);
                ReplenishEnergy(energyAmount);
                replenishEnergyTimer = 0f;
            }
        }
    }
}
