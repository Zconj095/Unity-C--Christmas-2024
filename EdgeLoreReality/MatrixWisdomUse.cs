using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixWisdomUse : MonoBehaviour
    {
        // Represents a learned skill
        private class LearnedSkill
        {
            public string SkillID { get; private set; }
            public string SkillName { get; private set; }
            public float ExperienceGained { get; private set; }

            public LearnedSkill(string skillName, float experienceGained)
            {
                SkillID = System.Guid.NewGuid().ToString();
                SkillName = skillName;
                ExperienceGained = experienceGained;
            }

            public void AddExperience(float additionalExperience)
            {
                ExperienceGained += additionalExperience;
                Debug.Log($"Skill '{SkillName}' gained {additionalExperience} experience. Total: {ExperienceGained}");
            }
        }

        // Database for all learned skills
        private List<LearnedSkill> skillDatabase = new List<LearnedSkill>();

        // Add a new skill to the database
        public void AddSkill(string skillName, float initialExperience)
        {
            if (skillDatabase.Exists(skill => skill.SkillName == skillName))
            {
                Debug.LogWarning($"Skill '{skillName}' already exists.");
                return;
            }

            LearnedSkill skill = new LearnedSkill(skillName, initialExperience);
            skillDatabase.Add(skill);
            Debug.Log($"New Skill Added - ID: {skill.SkillID}, Name: {skill.SkillName}, Experience: {skill.ExperienceGained}");
        }

        // Update experience for an existing skill
        public void UpdateSkillExperience(string skillName, float additionalExperience)
        {
            LearnedSkill skill = skillDatabase.Find(s => s.SkillName == skillName);
            if (skill != null)
            {
                skill.AddExperience(additionalExperience);
            }
            else
            {
                Debug.LogWarning($"Skill '{skillName}' not found.");
            }
        }

        // Calculate new skill data for Matrix Cortex
        public float CalculateWisdom()
        {
            Debug.Log("Calculating wisdom from learned skills...");
            float totalExperience = 0f;

            foreach (var skill in skillDatabase)
            {
                Debug.Log($"Skill '{skill.SkillName}', Experience: {skill.ExperienceGained}");
                totalExperience += skill.ExperienceGained;
            }

            float wisdom = totalExperience / skillDatabase.Count; // Average experience as wisdom metric
            Debug.Log($"Total Experience: {totalExperience}, Calculated Wisdom: {wisdom}");
            return wisdom;
        }

        // Display all learned skills
        public void DisplaySkills()
        {
            Debug.Log("Displaying all learned skills...");
            foreach (var skill in skillDatabase)
            {
                Debug.Log($"Skill - ID: {skill.SkillID}, Name: {skill.SkillName}, Experience: {skill.ExperienceGained}");
            }
        }

        private void Start()
        {
            Debug.Log("Matrix Wisdom Use System Initialized.");

            // Example setup
            AddSkill("Programming", 50f);
            AddSkill("Problem Solving", 30f);
            AddSkill("Creativity", 40f);

            // Automate actions
            InvokeRepeating(nameof(AddRandomSkill), 5f, 20f); // Add a random skill every 20 seconds
            InvokeRepeating(nameof(UpdateRandomSkillExperience), 10f, 15f); // Update experience for a random skill every 15 seconds
            InvokeRepeating(nameof(CalculateAndLogWisdom), 15f, 30f); // Calculate wisdom every 30 seconds
            InvokeRepeating(nameof(DisplaySkills), 20f, 30f); // Display skills every 30 seconds
        }

        // Add a random skill
        private void AddRandomSkill()
        {
            string[] randomSkills = { "Leadership", "Design", "Communication", "Critical Thinking" };
            string skillName = randomSkills[Random.Range(0, randomSkills.Length)];
            float experience = Random.Range(10f, 50f);

            AddSkill(skillName, experience);
        }

        // Update experience for a random skill
        private void UpdateRandomSkillExperience()
        {
            if (skillDatabase.Count > 0)
            {
                LearnedSkill randomSkill = skillDatabase[Random.Range(0, skillDatabase.Count)];
                float additionalExperience = Random.Range(5f, 25f);
                UpdateSkillExperience(randomSkill.SkillName, additionalExperience);
            }
            else
            {
                Debug.LogWarning("No skills available to update.");
            }
        }

        // Calculate and log wisdom
        private void CalculateAndLogWisdom()
        {
            float wisdom = CalculateWisdom();
            Debug.Log($"Automated Wisdom Calculation: {wisdom}");
        }
    }
}
