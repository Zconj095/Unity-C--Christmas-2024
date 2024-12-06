using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixSerialCode : MonoBehaviour
    {
        // Represents an entry in the serial code database
        private class SerialCodeEntry
        {
            public string Word { get; private set; }
            public string SerialCode { get; private set; }

            public SerialCodeEntry(string word, string serialCode)
            {
                Word = word;
                SerialCode = serialCode;
            }
        }

        // Universal serial language database
        private Dictionary<string, SerialCodeEntry> serialCodeDatabase = new Dictionary<string, SerialCodeEntry>();

        // Timers for automation
        private float addEntryTimer = 0f;
        private float searchWordTimer = 0f;
        private float searchCodeTimer = 0f;
        private float retrieveEntriesTimer = 0f;

        // Automation intervals
        private const float addEntryInterval = 5f; // Add an entry every 5 seconds
        private const float searchWordInterval = 7f; // Search a word every 7 seconds
        private const float searchCodeInterval = 10f; // Search a code every 10 seconds
        private const float retrieveEntriesInterval = 15f; // Retrieve all entries every 15 seconds

        // Add a word and its serial code to the database
        public void AddWordWithSerialCode(string word, string serialCode)
        {
            if (serialCodeDatabase.ContainsKey(word.ToLower()))
            {
                Debug.LogWarning($"Word '{word}' already exists in the database.");
                return;
            }

            SerialCodeEntry entry = new SerialCodeEntry(word, serialCode);
            serialCodeDatabase[word.ToLower()] = entry;

            Debug.Log($"Added Word: '{word}', Serial Code: '{serialCode}'");
        }

        // Search for a word in the database
        public string SearchWord(string word)
        {
            if (serialCodeDatabase.TryGetValue(word.ToLower(), out SerialCodeEntry entry))
            {
                Debug.Log($"Word Found: '{word}', Serial Code: '{entry.SerialCode}'");
                return entry.SerialCode;
            }

            Debug.LogWarning($"Word '{word}' not found in the database.");
            return null;
        }

        // Search for a serial code in the database
        public string SearchSerialCode(string serialCode)
        {
            foreach (var entry in serialCodeDatabase.Values)
            {
                if (entry.SerialCode == serialCode)
                {
                    Debug.Log($"Serial Code Found: '{serialCode}', Word: '{entry.Word}'");
                    return entry.Word;
                }
            }

            Debug.LogWarning($"Serial Code '{serialCode}' not found in the database.");
            return null;
        }

        // Retrieve all entries in the database
        public void RetrieveAllEntries()
        {
            Debug.Log("Retrieving all words and serial codes...");
            foreach (var entry in serialCodeDatabase.Values)
            {
                Debug.Log($"Word: '{entry.Word}', Serial Code: '{entry.SerialCode}'");
            }
        }

        private void Start()
        {
            Debug.Log("Matrix Serial Code System Initialized.");
        }

        private void Update()
        {
            // Increment timers
            addEntryTimer += Time.deltaTime;
            searchWordTimer += Time.deltaTime;
            searchCodeTimer += Time.deltaTime;
            retrieveEntriesTimer += Time.deltaTime;

            // Automate adding entries
            if (addEntryTimer >= addEntryInterval)
            {
                string randomWord = $"Word_{Random.Range(1, 100)}";
                string randomCode = $"s{Random.Range(1000, 9999)}.x{Random.Range(100, 999)}";
                AddWordWithSerialCode(randomWord, randomCode);
                addEntryTimer = 0f;
            }

            // Automate searching for a word
            if (searchWordTimer >= searchWordInterval && serialCodeDatabase.Count > 0)
            {
                foreach (var entry in serialCodeDatabase.Values) // Use the first available word for testing
                {
                    SearchWord(entry.Word);
                    break;
                }
                searchWordTimer = 0f;
            }

            // Automate searching for a serial code
            if (searchCodeTimer >= searchCodeInterval && serialCodeDatabase.Count > 0)
            {
                foreach (var entry in serialCodeDatabase.Values) // Use the first available code for testing
                {
                    SearchSerialCode(entry.SerialCode);
                    break;
                }
                searchCodeTimer = 0f;
            }

            // Automate retrieving all entries
            if (retrieveEntriesTimer >= retrieveEntriesInterval)
            {
                RetrieveAllEntries();
                retrieveEntriesTimer = 0f;
            }
        }
    }
}
