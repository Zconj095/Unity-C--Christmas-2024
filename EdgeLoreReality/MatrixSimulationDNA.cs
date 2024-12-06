using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixSimulationDNA : MonoBehaviour
    {
        // Represents genetic data for a species
        private class GeneticData
        {
            public string Species { get; private set; }
            public Dictionary<string, string> Genes { get; private set; }

            public GeneticData(string species)
            {
                Species = species;
                Genes = new Dictionary<string, string>();
            }

            public void AddGene(string geneKey, string geneValue)
            {
                Genes[geneKey] = geneValue;
                Debug.Log($"Gene Added - Species: {Species}, Gene: {geneKey} = {geneValue}");
            }

            public void EditGene(string geneKey, string newGeneValue)
            {
                if (Genes.ContainsKey(geneKey))
                {
                    Genes[geneKey] = newGeneValue;
                    Debug.Log($"Gene Edited - Species: {Species}, Gene: {geneKey} = {newGeneValue}");
                }
                else
                {
                    Debug.LogWarning($"Gene '{geneKey}' not found in species '{Species}'.");
                }
            }

            public void DisplayGenes()
            {
                Debug.Log($"Genes for Species: {Species}");
                foreach (var gene in Genes)
                {
                    Debug.Log($"  {gene.Key} = {gene.Value}");
                }
            }
        }

        // List of genetic data for all species
        private List<GeneticData> geneticDatabase = new List<GeneticData>();

        // Automated task timer
        private float taskTimer = 0f;
        private const float taskInterval = 5f; // Interval for automated tasks

        // Add a new species to the database
        public void AddSpecies(string species)
        {
            if (geneticDatabase.Exists(g => g.Species == species))
            {
                Debug.LogWarning($"Species '{species}' already exists in the database.");
                return;
            }

            GeneticData geneticData = new GeneticData(species);
            geneticDatabase.Add(geneticData);
            Debug.Log($"Species Added - {species}");
        }

        // Add a gene to a species
        public void AddGeneToSpecies(string species, string geneKey, string geneValue)
        {
            GeneticData speciesData = geneticDatabase.Find(g => g.Species == species);
            if (speciesData != null)
            {
                speciesData.AddGene(geneKey, geneValue);
            }
            else
            {
                Debug.LogWarning($"Species '{species}' not found.");
            }
        }

        // Clone genetic data from one species to another
        public void CloneGeneticData(string sourceSpecies, string targetSpecies)
        {
            GeneticData sourceData = geneticDatabase.Find(g => g.Species == sourceSpecies);
            if (sourceData == null)
            {
                Debug.LogWarning($"Source species '{sourceSpecies}' not found.");
                return;
            }

            AddSpecies(targetSpecies);
            GeneticData targetData = geneticDatabase.Find(g => g.Species == targetSpecies);
            foreach (var gene in sourceData.Genes)
            {
                targetData.AddGene(gene.Key, gene.Value);
            }

            Debug.Log($"Genetic Data Cloned - Source: {sourceSpecies}, Target: {targetSpecies}");
        }

        // Display genetic data for all species
        public void DisplayAllGeneticData()
        {
            Debug.Log("Displaying genetic data for all species...");
            foreach (var geneticData in geneticDatabase)
            {
                geneticData.DisplayGenes();
            }
        }

        private void Start()
        {
            Debug.Log("Automated Matrix Simulation DNA System Initialized.");
        }

        private void Update()
        {
            // Increment the task timer
            taskTimer += Time.deltaTime;

            // Perform automated tasks at regular intervals
            if (taskTimer >= taskInterval)
            {
                PerformAutomatedTasks();
                taskTimer = 0f; // Reset the timer
            }
        }

        // Perform automated DNA management tasks
        private void PerformAutomatedTasks()
        {
            Debug.Log("Performing automated DNA tasks...");

            // Add species dynamically
            string speciesName = "AutomatedSpecies" + geneticDatabase.Count;
            AddSpecies(speciesName);

            // Add genetic traits to the most recently added species
            if (geneticDatabase.Count > 0)
            {
                AddGeneToSpecies(speciesName, "Trait" + Random.Range(1, 10), "Value" + Random.Range(1, 100));
            }

            // Clone the first species to a new one if possible
            if (geneticDatabase.Count >= 2)
            {
                CloneGeneticData(geneticDatabase[0].Species, "Cloned" + Random.Range(1, 100));
            }

            // Display current genetic database
            DisplayAllGeneticData();
        }
    }
}
