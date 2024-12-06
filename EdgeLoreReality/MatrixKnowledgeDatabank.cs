using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixKnowledgeDatabank : MonoBehaviour
    {
        // Represents a knowledge entry in the databank
        private class KnowledgeEntry
        {
            public string EntryID { get; private set; }
            public string Category { get; private set; }
            public string Data { get; private set; }
            public List<string> LinkedSections { get; private set; }

            public KnowledgeEntry(string category, string data)
            {
                EntryID = System.Guid.NewGuid().ToString();
                Category = category;
                Data = data;
                LinkedSections = new List<string>();
            }

            public void LinkSection(string section)
            {
                if (!LinkedSections.Contains(section))
                {
                    LinkedSections.Add(section);
                    Debug.Log($"Entry '{EntryID}' linked to section '{section}'.");
                }
            }
        }

        // Stores all knowledge entries
        private Dictionary<string, KnowledgeEntry> databank = new Dictionary<string, KnowledgeEntry>();

        // Timers for automation
        private float addEntryTimer = 0f;
        private float linkSectionTimer = 0f;
        private float retrieveAllEntriesTimer = 0f;
        private float retrieveSpecificEntryTimer = 0f;

        // Automation intervals
        private const float addEntryInterval = 5f; // Add an entry every 5 seconds
        private const float linkSectionInterval = 7f; // Link an entry every 7 seconds
        private const float retrieveAllEntriesInterval = 10f; // Retrieve all entries every 10 seconds
        private const float retrieveSpecificEntryInterval = 12f; // Retrieve a specific entry every 12 seconds

        // Add a new knowledge entry
        public void AddKnowledgeEntry(string category, string data)
        {
            KnowledgeEntry entry = new KnowledgeEntry(category, data);
            databank[entry.EntryID] = entry;
            Debug.Log($"Knowledge Entry Added - ID: {entry.EntryID}, Category: {category}, Data: {data}");
        }

        // Link an entry to a section
        public void LinkEntryToSection(string entryID, string section)
        {
            if (databank.TryGetValue(entryID, out KnowledgeEntry entry))
            {
                entry.LinkSection(section);
            }
            else
            {
                Debug.LogWarning($"Entry with ID '{entryID}' not found.");
            }
        }

        // Retrieve a specific entry by ID
        public void RetrieveEntry(string entryID)
        {
            if (databank.TryGetValue(entryID, out KnowledgeEntry entry))
            {
                Debug.Log($"Entry - ID: {entry.EntryID}, Category: {entry.Category}, Data: {entry.Data}");
                Debug.Log($"Linked Sections: {string.Join(", ", entry.LinkedSections)}");
            }
            else
            {
                Debug.LogWarning($"Entry with ID '{entryID}' not found.");
            }
        }

        // Retrieve all entries
        public void RetrieveAllEntries()
        {
            Debug.Log("Retrieving all knowledge entries...");
            foreach (var entry in databank.Values)
            {
                Debug.Log($"Entry - ID: {entry.EntryID}, Category: {entry.Category}, Data: {entry.Data}");
                Debug.Log($"Linked Sections: {string.Join(", ", entry.LinkedSections)}");
            }
        }

        private void Start()
        {
            Debug.Log("Matrix Knowledge Databank Initialized.");
        }

        private void Update()
        {
            // Increment timers
            addEntryTimer += Time.deltaTime;
            linkSectionTimer += Time.deltaTime;
            retrieveAllEntriesTimer += Time.deltaTime;
            retrieveSpecificEntryTimer += Time.deltaTime;

            // Automate adding knowledge entries
            if (addEntryTimer >= addEntryInterval)
            {
                AddKnowledgeEntry($"Category_{databank.Count + 1}", $"Data_{Random.Range(1, 100)}");
                addEntryTimer = 0f;
            }

            // Automate linking entries to sections
            if (linkSectionTimer >= linkSectionInterval && databank.Count > 0)
            {
                foreach (var entry in databank.Values)
                {
                    LinkEntryToSection(entry.EntryID, "AutomatedSection");
                    break; // Link only one entry per interval
                }
                linkSectionTimer = 0f;
            }

            // Automate retrieving all entries
            if (retrieveAllEntriesTimer >= retrieveAllEntriesInterval)
            {
                RetrieveAllEntries();
                retrieveAllEntriesTimer = 0f;
            }

            // Automate retrieving a specific entry
            if (retrieveSpecificEntryTimer >= retrieveSpecificEntryInterval && databank.Count > 0)
            {
                foreach (var entry in databank.Values)
                {
                    RetrieveEntry(entry.EntryID);
                    break; // Retrieve only one entry per interval
                }
                retrieveSpecificEntryTimer = 0f;
            }
        }
    }
}
