using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixKnowledgeUnits : MonoBehaviour
    {
        // Represents a single unit of matrix knowledge
        private class KnowledgeUnit
        {
            public string UnitID { get; private set; }
            public string Data { get; private set; }
            public bool IsProcessed { get; private set; }

            public KnowledgeUnit(string data)
            {
                UnitID = System.Guid.NewGuid().ToString();
                Data = data;
                IsProcessed = false;
            }

            public void MarkProcessed()
            {
                IsProcessed = true;
                Debug.Log($"Knowledge Unit '{UnitID}' has been processed.");
            }
        }

        // List of all knowledge units
        private List<KnowledgeUnit> knowledgeUnits = new List<KnowledgeUnit>();

        // Synchronization and energy management
        private float totalEnergy = 0f;

        // Timers for automation
        private float addUnitTimer = 0f;
        private float processUnitsTimer = 0f;
        private float retrieveUnitsTimer = 0f;

        // Automation intervals
        private const float addUnitInterval = 5f; // Add a unit every 5 seconds
        private const float processUnitsInterval = 7f; // Process units every 7 seconds
        private const float retrieveUnitsInterval = 10f; // Retrieve units every 10 seconds

        // Add a new knowledge unit
        public void AddKnowledgeUnit(string data)
        {
            KnowledgeUnit unit = new KnowledgeUnit(data);
            knowledgeUnits.Add(unit);
            Debug.Log($"Knowledge Unit Added - ID: {unit.UnitID}, Data: {data}");
        }

        // Process knowledge units into energy
        public void ProcessKnowledgeUnits()
        {
            Debug.Log("Processing knowledge units into energy...");
            foreach (var unit in knowledgeUnits)
            {
                if (!unit.IsProcessed)
                {
                    float unitEnergy = CalculateUnitEnergy(unit.Data);
                    totalEnergy += unitEnergy;
                    unit.MarkProcessed();
                    Debug.Log($"Unit '{unit.UnitID}' converted to {unitEnergy} energy units.");
                }
            }

            Debug.Log($"Total Energy: {totalEnergy} units.");
        }

        // Calculate energy based on the mathematical framework
        private float CalculateUnitEnergy(string data)
        {
            int wordCount = data.Split(' ').Length;
            float energy = Mathf.PI * 22.831f * Mathf.Pow(2, wordCount);
            energy /= 42.185f * Mathf.Sin(32.81f * Mathf.Deg2Rad);
            energy *= 0.6681f; // 66% split factor
            energy *= 0.971346f; // 2.8654% flux capacitance adjustment
            return energy;
        }

        // Retrieve all knowledge units
        public void RetrieveKnowledgeUnits()
        {
            Debug.Log("Retrieving all knowledge units...");
            foreach (var unit in knowledgeUnits)
            {
                Debug.Log($"Unit - ID: {unit.UnitID}, Data: {unit.Data}, Processed: {unit.IsProcessed}");
            }
        }

        // Get the total energy
        public float GetTotalEnergy()
        {
            return totalEnergy;
        }

        private void Start()
        {
            Debug.Log("Matrix Knowledge Units System Initialized.");
        }

        private void Update()
        {
            // Increment timers
            addUnitTimer += Time.deltaTime;
            processUnitsTimer += Time.deltaTime;
            retrieveUnitsTimer += Time.deltaTime;

            // Automate adding knowledge units
            if (addUnitTimer >= addUnitInterval)
            {
                AddKnowledgeUnit($"Generated data at {Time.time}");
                addUnitTimer = 0f;
            }

            // Automate processing knowledge units
            if (processUnitsTimer >= processUnitsInterval)
            {
                ProcessKnowledgeUnits();
                processUnitsTimer = 0f;
            }

            // Automate retrieving all knowledge units
            if (retrieveUnitsTimer >= retrieveUnitsInterval)
            {
                RetrieveKnowledgeUnits();
                retrieveUnitsTimer = 0f;
            }
        }
    }
}
