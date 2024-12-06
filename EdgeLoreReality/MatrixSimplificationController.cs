using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixSimplificationController : MonoBehaviour
    {
        // Represents a task processed by the controller
        private class SimulationTask
        {
            public string TaskID { get; private set; }
            public string Description { get; private set; }
            public float Percentage { get; private set; } // Simplified to nearest percentage
            public bool IsProcessed { get; private set; }

            public SimulationTask(string description, float rawValue)
            {
                TaskID = System.Guid.NewGuid().ToString();
                Description = description;
                Percentage = SimplifyToNearestPercentage(rawValue);
                IsProcessed = false;
            }

            private float SimplifyToNearestPercentage(float value)
            {
                return Mathf.Round(value); // Rounds to nearest percentage
            }

            public void MarkProcessed()
            {
                IsProcessed = true;
                Debug.Log($"Task '{TaskID}' processed: {Description} => {Percentage}%");
            }
        }

        // List of incoming tasks
        private Queue<SimulationTask> taskQueue = new Queue<SimulationTask>();

        // Processed tasks
        private List<SimulationTask> processedTasks = new List<SimulationTask>();

        // Timers for automation
        private float addTaskTimer = 0f;
        private float processTaskTimer = 0f;
        private float adjustOptionsTimer = 0f;

        // Automation intervals
        private const float addTaskInterval = 5f; // Add a task every 5 seconds
        private const float processTaskInterval = 3f; // Process a task every 3 seconds
        private const float adjustOptionsInterval = 10f; // Adjust options every 10 seconds

        // Rapid input handling for high-frequency commands
        public void AddTask(string description, float rawValue)
        {
            SimulationTask task = new SimulationTask(description, rawValue);
            taskQueue.Enqueue(task);
            Debug.Log($"Task Added - ID: {task.TaskID}, Description: {description}, Raw Value: {rawValue}, Simplified: {task.Percentage}%");
        }

        // Process the next task in the queue
        public void ProcessNextTask()
        {
            if (taskQueue.Count > 0)
            {
                SimulationTask task = taskQueue.Dequeue();
                task.MarkProcessed();
                processedTasks.Add(task);
            }
            else
            {
                Debug.LogWarning("No tasks in the queue to process.");
            }
        }

        // Adjust to simulation options
        public void AdjustSimulationOptions()
        {
            Debug.Log("Adjusting simulation options based on processed tasks...");
            foreach (var task in processedTasks)
            {
                Debug.Log($"Processed Task - ID: {task.TaskID}, Adjustment Percentage: {task.Percentage}%");
            }
        }

        // Display status of all tasks
        public void DisplayTaskStatus()
        {
            Debug.Log("Displaying task statuses...");
            foreach (var task in processedTasks)
            {
                Debug.Log($"Task - ID: {task.TaskID}, Description: {task.Description}, Percentage: {task.Percentage}%, Processed: {task.IsProcessed}");
            }

            if (taskQueue.Count > 0)
            {
                Debug.Log($"Pending Tasks: {taskQueue.Count}");
            }
            else
            {
                Debug.Log("No pending tasks.");
            }
        }

        private void Start()
        {
            Debug.Log("Matrix Simplification Controller Initialized.");
        }

        private void Update()
        {
            // Increment timers
            addTaskTimer += Time.deltaTime;
            processTaskTimer += Time.deltaTime;
            adjustOptionsTimer += Time.deltaTime;

            // Automate task addition
            if (addTaskTimer >= addTaskInterval)
            {
                AddTask("Auto-Generated Task", Random.Range(1.0f, 100.0f));
                addTaskTimer = 0f;
            }

            // Automate task processing
            if (processTaskTimer >= processTaskInterval)
            {
                ProcessNextTask();
                processTaskTimer = 0f;
            }

            // Automate adjustment of simulation options
            if (adjustOptionsTimer >= adjustOptionsInterval)
            {
                AdjustSimulationOptions();
                adjustOptionsTimer = 0f;
            }
        }
    }
}
