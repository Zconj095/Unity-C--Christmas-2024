using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixCortex : MonoBehaviour
    {
        // Represents a skill in the brain
        private class BrainSkill
        {
            public string SkillName { get; private set; }
            public int Level { get; private set; } // Skill level determines proficiency
            public string Category { get; private set; }

            public BrainSkill(string skillName, string category, int initialLevel = 1)
            {
                SkillName = skillName;
                Level = initialLevel;
                Category = category;
            }

            public void ImproveSkill(int increment)
            {
                Level += increment;
                Debug.Log($"Skill '{SkillName}' improved by {increment}. New Level: {Level}");
            }
        }

        // Represents a Matrix Cortex database
        private class MatrixDatabase
        {
            public string DatabaseID { get; private set; }
            public List<BrainSkill> Skills { get; private set; }
            public float MemoryCapacity { get; private set; } // GB
            public float MemoryUsage { get; private set; } // GB

            public MatrixDatabase(float initialCapacity = 200f)
            {
                DatabaseID = System.Guid.NewGuid().ToString();
                Skills = new List<BrainSkill>();
                MemoryCapacity = initialCapacity;
                MemoryUsage = 0f;
            }

            public void AddSkill(BrainSkill skill)
            {
                Skills.Add(skill);
                MemoryUsage += 0.1f; // Simulated memory usage per skill
                Debug.Log($"Skill '{skill.SkillName}' added to Database '{DatabaseID}'. Memory Usage: {MemoryUsage}/{MemoryCapacity} GB.");
            }

            public void RecycleMemory()
            {
                MemoryUsage *= 0.75f; // Recycle 25% of memory usage
                Debug.Log($"Database '{DatabaseID}' recycled memory. New Usage: {MemoryUsage}/{MemoryCapacity} GB.");
            }
        }

        // Temporal Lobe acting as the Master Database
        private MatrixDatabase masterDatabase;

        // Timers for automation
        private float addSkillTimer = 0f;
        private float improveSkillTimer = 0f;
        private float recycleMemoryTimer = 0f;
        private float displaySkillsTimer = 0f;

        // Automation intervals
        private const float addSkillInterval = 5f; // Add a skill every 5 seconds
        private const float improveSkillInterval = 7f; // Improve a skill every 7 seconds
        private const float recycleMemoryInterval = 10f; // Recycle memory every 10 seconds
        private const float displaySkillsInterval = 15f; // Display skills every 15 seconds

        // Initialize the Matrix Cortex
        private void InitializeCortex()
        {
            masterDatabase = new MatrixDatabase(6000f); // Set large initial capacity for the master database
            Debug.Log("Matrix Cortex initialized with Master Database.");
        }

        // Add a skill to the Matrix Cortex
        public void AddSkillToCortex(string skillName, string category)
        {
            BrainSkill skill = new BrainSkill(skillName, category);
            if (masterDatabase.MemoryUsage + 0.1f > masterDatabase.MemoryCapacity)
            {
                Debug.LogWarning($"Cannot add skill '{skillName}'. Memory limit reached. Recycle memory first.");
            }
            else
            {
                masterDatabase.AddSkill(skill);
            }
        }

        // Improve an existing skill in the cortex
        public void ImproveSkill(string skillName, int increment)
        {
            BrainSkill skill = masterDatabase.Skills.Find(s => s.SkillName == skillName);
            if (skill != null)
            {
                skill.ImproveSkill(increment);
            }
            else
            {
                Debug.LogWarning($"Skill '{skillName}' not found in the Matrix Cortex.");
            }
        }

        // Recycle memory in the Matrix Cortex
        public void RecycleCortexMemory()
        {
            masterDatabase.RecycleMemory();
        }

        // Display all skills in the Matrix Cortex
        public void DisplaySkills()
        {
            Debug.Log("Displaying all skills in the Matrix Cortex:");
            foreach (var skill in masterDatabase.Skills)
            {
                Debug.Log($"Skill: {skill.SkillName}, Level: {skill.Level}, Category: {skill.Category}");
            }
        }

        private void Start()
        {
            InitializeCortex();

            // Initial setup
            AddSkillToCortex("Programming", "Cognitive");
            AddSkillToCortex("Athletics", "Physical");
            AddSkillToCortex("Problem Solving", "Cognitive");
        }

        private void Update()
        {
            // Increment timers
            addSkillTimer += Time.deltaTime;
            improveSkillTimer += Time.deltaTime;
            recycleMemoryTimer += Time.deltaTime;
            displaySkillsTimer += Time.deltaTime;

            // Automate adding skills
            if (addSkillTimer >= addSkillInterval)
            {
                string[] categories = { "Cognitive", "Physical", "Creative" };
                string randomCategory = categories[Random.Range(0, categories.Length)];
                AddSkillToCortex($"Skill_{masterDatabase.Skills.Count + 1}", randomCategory);
                addSkillTimer = 0f;
            }

            // Automate improving skills
            if (improveSkillTimer >= improveSkillInterval && masterDatabase.Skills.Count > 0)
            {
                var skillToImprove = masterDatabase.Skills[Random.Range(0, masterDatabase.Skills.Count)];
                ImproveSkill(skillToImprove.SkillName, Random.Range(1, 5));
                improveSkillTimer = 0f;
            }

            // Automate recycling memory
            if (recycleMemoryTimer >= recycleMemoryInterval)
            {
                RecycleCortexMemory();
                recycleMemoryTimer = 0f;
            }

            // Automate displaying skills
            if (displaySkillsTimer >= displaySkillsInterval)
            {
                DisplaySkills();
                displaySkillsTimer = 0f;
            }
        }
    }
}
