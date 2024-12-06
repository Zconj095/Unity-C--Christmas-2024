using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixServer : MonoBehaviour
    {
        // Represents a word in the spatial-temporal grid
        private class Word
        {
            public string Text { get; private set; }

            public Word(string text)
            {
                Text = text;
            }
        }

        // Represents an operating system connected to the server
        private class OperatingSystem
        {
            public string OSID { get; private set; }
            public List<Word> Words { get; private set; }

            public OperatingSystem()
            {
                OSID = System.Guid.NewGuid().ToString();
                Words = new List<Word>();
            }

            public void AddWord(string wordText)
            {
                Word word = new Word(wordText);
                Words.Add(word);
                Debug.Log($"Word '{wordText}' added to OS '{OSID}'.");
            }

            public Word PickWord()
            {
                if (Words.Count > 0)
                {
                    int randomIndex = Random.Range(0, Words.Count);
                    Debug.Log($"Word picked from OS '{OSID}': {Words[randomIndex].Text}");
                    return Words[randomIndex];
                }

                Debug.LogWarning($"OS '{OSID}' has no words to pick from.");
                return null;
            }
        }

        // List of operating systems connected to the server
        private List<OperatingSystem> operatingSystems = new List<OperatingSystem>();

        // Timers for automation
        private float addOSTimer = 0f;
        private float addWordTimer = 0f;
        private float retrieveStatusTimer = 0f;

        // Automation intervals
        private const float addOSInterval = 10f; // Add a new OS every 10 seconds
        private const float addWordInterval = 5f; // Add a word to an OS every 5 seconds
        private const float retrieveStatusInterval = 15f; // Retrieve OS status every 15 seconds

        // Add an operating system to the server
        public void AddOperatingSystem()
        {
            if (operatingSystems.Count >= 3)
            {
                Debug.LogWarning("Maximum of 3 operating systems can be connected to the server.");
                return;
            }

            OperatingSystem os = new OperatingSystem();
            operatingSystems.Add(os);
            Debug.Log($"Operating System added to the server - OSID: {os.OSID}");
        }

        // Add a word to an operating system
        public void AddWordToOS(int osIndex, string wordText)
        {
            if (osIndex >= 0 && osIndex < operatingSystems.Count)
            {
                operatingSystems[osIndex].AddWord(wordText);
            }
            else
            {
                Debug.LogWarning($"Operating system index '{osIndex}' is out of range.");
            }
        }

        // Pick a word from a specific operating system
        public void PickWordFromOS(int osIndex)
        {
            if (osIndex >= 0 && osIndex < operatingSystems.Count)
            {
                operatingSystems[osIndex].PickWord();
            }
            else
            {
                Debug.LogWarning($"Operating system index '{osIndex}' is out of range.");
            }
        }

        // Retrieve the status of all operating systems
        public void RetrieveOSStatus()
        {
            Debug.Log("Retrieving operating system status...");
            for (int i = 0; i < operatingSystems.Count; i++)
            {
                Debug.Log($"OS {i + 1} - ID: {operatingSystems[i].OSID}, Words Count: {operatingSystems[i].Words.Count}");
            }
        }

        private void Start()
        {
            Debug.Log("Matrix Server Initialized.");
        }

        private void Update()
        {
            // Increment timers
            addOSTimer += Time.deltaTime;
            addWordTimer += Time.deltaTime;
            retrieveStatusTimer += Time.deltaTime;

            // Automate adding operating systems
            if (addOSTimer >= addOSInterval)
            {
                AddOperatingSystem();
                addOSTimer = 0f;
            }

            // Automate adding words to the first OS
            if (addWordTimer >= addWordInterval && operatingSystems.Count > 0)
            {
                AddWordToOS(0, $"Word_{Random.Range(1, 100)}");
                addWordTimer = 0f;
            }

            // Automate retrieving OS status
            if (retrieveStatusTimer >= retrieveStatusInterval)
            {
                RetrieveOSStatus();
                retrieveStatusTimer = 0f;
            }
        }
    }
}
