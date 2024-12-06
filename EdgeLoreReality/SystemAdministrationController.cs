using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class SystemAdministrationController : MonoBehaviour
    {
        // Represents an administrative task
        private class AdminTask
        {
            public string TaskID { get; private set; }
            public string Description { get; private set; }
            public bool IsCompleted { get; private set; }

            public AdminTask(string description)
            {
                TaskID = System.Guid.NewGuid().ToString();
                Description = description;
                IsCompleted = false;
            }

            public void CompleteTask()
            {
                IsCompleted = true;
                Debug.Log($"Task '{TaskID}' completed: {Description}");
            }
        }

        // Represents an administrative option
        private class AdminOption
        {
            public string OptionID { get; private set; }
            public string Name { get; private set; }
            public string Modifier { get; private set; } // Example of how the option modifies a system

            public AdminOption(string name, string modifier)
            {
                OptionID = System.Guid.NewGuid().ToString();
                Name = name;
                Modifier = modifier;
            }

            public void ApplyOption()
            {
                Debug.Log($"Option '{Name}' applied with modifier: {Modifier}");
            }
        }

        // Administrative tasks and options
        private List<AdminTask> tasks = new List<AdminTask>();
        private List<AdminOption> options = new List<AdminOption>();

        // Add a new administrative task
        public void AddTask(string description)
        {
            AdminTask task = new AdminTask(description);
            tasks.Add(task);
            Debug.Log($"New Admin Task Added - ID: {task.TaskID}, Description: {description}");
        }

        // Complete a task
        public void CompleteTask(string taskID)
        {
            AdminTask task = tasks.Find(t => t.TaskID == taskID);
            if (task != null)
            {
                task.CompleteTask();
            }
            else
            {
                Debug.LogWarning($"Task with ID '{taskID}' not found.");
            }
        }

        // Add a new administrative option
        public void AddOption(string name, string modifier)
        {
            AdminOption option = new AdminOption(name, modifier);
            options.Add(option);
            Debug.Log($"New Admin Option Added - ID: {option.OptionID}, Name: {name}, Modifier: {modifier}");
        }

        // Apply an option
        public void ApplyOption(string optionID)
        {
            AdminOption option = options.Find(o => o.OptionID == optionID);
            if (option != null)
            {
                option.ApplyOption();
            }
            else
            {
                Debug.LogWarning($"Option with ID '{optionID}' not found.");
            }
        }

        // Display all tasks
        public void DisplayTasks()
        {
            Debug.Log("Displaying all administrative tasks...");
            foreach (var task in tasks)
            {
                Debug.Log($"Task - ID: {task.TaskID}, Description: {task.Description}, Completed: {task.IsCompleted}");
            }
        }

        // Display all options
        public void DisplayOptions()
        {
            Debug.Log("Displaying all administrative options...");
            foreach (var option in options)
            {
                Debug.Log($"Option - ID: {option.OptionID}, Name: {option.Name}, Modifier: {option.Modifier}");
            }
        }

        private void Start()
        {
            Debug.Log("System Administration Controller Initialized.");

            // Initial setup
            AddTask("Initialize System Diagnostics");
            AddOption("Activate Debug Mode", "+10% Performance Analysis");

            // Automate actions
            InvokeRepeating(nameof(AutoAddTask), 2f, 5f); // Add a new task every 5 seconds
            InvokeRepeating(nameof(AutoCompleteTask), 3f, 7f); // Complete a task every 7 seconds
            InvokeRepeating(nameof(AutoAddOption), 4f, 6f); // Add a new option every 6 seconds
            InvokeRepeating(nameof(AutoApplyOption), 5f, 8f); // Apply an option every 8 seconds
            InvokeRepeating(nameof(DisplayTasks), 10f, 10f); // Display tasks every 10 seconds
            InvokeRepeating(nameof(DisplayOptions), 12f, 10f); // Display options every 10 seconds
        }

        private void AutoAddTask()
        {
            string[] taskDescriptions = { "Reboot System Core", "Optimize Network Speed", "Check System Logs", "Update Security Protocols" };
            string description = taskDescriptions[Random.Range(0, taskDescriptions.Length)];
            AddTask(description);
        }

        private void AutoCompleteTask()
        {
            if (tasks.Count > 0)
            {
                CompleteTask(tasks[0].TaskID); // Complete the first task in the list
                tasks.RemoveAt(0); // Remove it from the task list
            }
            else
            {
                Debug.LogWarning("No tasks available to complete.");
            }
        }

        private void AutoAddOption()
        {
            string[] optionNames = { "Enable Encryption", "Activate Firewall", "Boost Performance", "Load Balancer Adjustment" };
            string[] modifiers = { "+20% Security", "+15% Stability", "+10% Speed", "+25% Efficiency" };
            int randomIndex = Random.Range(0, optionNames.Length);

            AddOption(optionNames[randomIndex], modifiers[randomIndex]);
        }

        private void AutoApplyOption()
        {
            if (options.Count > 0)
            {
                ApplyOption(options[0].OptionID); // Apply the first option in the list
                options.RemoveAt(0); // Remove it from the options list
            }
            else
            {
                Debug.LogWarning("No options available to apply.");
            }
        }
    }
}
