using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixManagementSystem : MonoBehaviour
    {
        // Represents a command in the system
        private class MatrixCommand
        {
            public string CommandID { get; private set; }
            public string CommandText { get; private set; }
            public string Status { get; private set; }

            public MatrixCommand(string commandText)
            {
                CommandID = System.Guid.NewGuid().ToString();
                CommandText = commandText;
                Status = "Pending";
            }

            public void Execute()
            {
                Status = "Completed";
                Debug.Log($"Command '{CommandID}' executed: {CommandText}");
            }
        }

        // Task and job queue
        private Queue<MatrixCommand> commandQueue = new Queue<MatrixCommand>();
        private List<MatrixCommand> completedCommands = new List<MatrixCommand>();

        // Knowledge database for learning
        private Dictionary<string, int> knowledgeDatabase = new Dictionary<string, int>();

        // Timers for automation
        private float addCommandTimer = 0f;
        private float executeCommandTimer = 0f;
        private float retrieveStatusTimer = 0f;
        private float retrieveKnowledgeTimer = 0f;

        // Automation intervals
        private const float addCommandInterval = 5f; // Add a command every 5 seconds
        private const float executeCommandInterval = 3f; // Execute a command every 3 seconds
        private const float retrieveStatusInterval = 10f; // Retrieve command statuses every 10 seconds
        private const float retrieveKnowledgeInterval = 15f; // Retrieve knowledge database every 15 seconds

        // Add a command to the queue
        public void AddCommand(string commandText)
        {
            MatrixCommand command = new MatrixCommand(commandText);
            commandQueue.Enqueue(command);
            Debug.Log($"Command Added - ID: {command.CommandID}, Text: {commandText}");
        }

        // Execute the next command in the queue
        public void ExecuteNextCommand()
        {
            if (commandQueue.Count > 0)
            {
                MatrixCommand command = commandQueue.Dequeue();
                command.Execute();
                completedCommands.Add(command);

                LearnFromCommand(command.CommandText);
            }
            else
            {
                Debug.LogWarning("No commands in the queue to execute.");
            }
        }

        // Learn from completed commands
        private void LearnFromCommand(string commandText)
        {
            if (knowledgeDatabase.ContainsKey(commandText))
            {
                knowledgeDatabase[commandText]++;
                Debug.Log($"Knowledge increased for command: {commandText}, Count: {knowledgeDatabase[commandText]}");
            }
            else
            {
                knowledgeDatabase[commandText] = 1;
                Debug.Log($"New knowledge entry added for command: {commandText}");
            }
        }

        // Retrieve the status of all commands
        public void RetrieveCommandStatus()
        {
            Debug.Log("Retrieving command statuses...");
            foreach (var command in completedCommands)
            {
                Debug.Log($"Command - ID: {command.CommandID}, Text: {command.CommandText}, Status: {command.Status}");
            }
        }

        // Retrieve the knowledge database
        public void RetrieveKnowledgeDatabase()
        {
            Debug.Log("Retrieving knowledge database...");
            foreach (var knowledge in knowledgeDatabase)
            {
                Debug.Log($"Knowledge Entry - Command: {knowledge.Key}, Usage Count: {knowledge.Value}");
            }
        }

        private void Start()
        {
            Debug.Log("Matrix Management System Initialized.");
        }

        private void Update()
        {
            // Increment timers
            addCommandTimer += Time.deltaTime;
            executeCommandTimer += Time.deltaTime;
            retrieveStatusTimer += Time.deltaTime;
            retrieveKnowledgeTimer += Time.deltaTime;

            // Automate adding commands
            if (addCommandTimer >= addCommandInterval)
            {
                AddCommand($"Automated Command {Time.time}");
                addCommandTimer = 0f;
            }

            // Automate executing commands
            if (executeCommandTimer >= executeCommandInterval)
            {
                ExecuteNextCommand();
                executeCommandTimer = 0f;
            }

            // Automate retrieving command statuses
            if (retrieveStatusTimer >= retrieveStatusInterval)
            {
                RetrieveCommandStatus();
                retrieveStatusTimer = 0f;
            }

            // Automate retrieving the knowledge database
            if (retrieveKnowledgeTimer >= retrieveKnowledgeInterval)
            {
                RetrieveKnowledgeDatabase();
                retrieveKnowledgeTimer = 0f;
            }
        }
    }
}
