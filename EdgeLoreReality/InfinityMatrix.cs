using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace edgelorereality
{
    public class InfinityMatrix : MonoBehaviour
    {
        // Represents a word in the Infinity Matrix
        private class MatrixWord
        {
            public string Word { get; private set; }
            public string Definition { get; private set; }
            public float PowerGenerated { get; private set; }

            public MatrixWord(string word, string definition)
            {
                Word = word;
                Definition = definition;
                PowerGenerated = CalculatePower(word);
            }

            private float CalculatePower(string word)
            {
                return word.Length * 10f; // Power based on word length
            }
        }

        // Word storage
        private Dictionary<string, MatrixWord> wordBank = new Dictionary<string, MatrixWord>();
        private List<MatrixWord> activeWords = new List<MatrixWord>();
        private float totalPower = 0f;

        // Folder path for words
        private string wordsFolderPath;

        // Timers for automation
        private float addWordTimer = 0f;
        private float removeWordTimer = 0f;

        // Automation intervals
        private const float addWordInterval = 5f; // Every 5 seconds
        private const float removeWordInterval = 10f; // Every 10 seconds

        private void Start()
        {
            wordsFolderPath = Path.Combine(Application.dataPath, "words");
            LoadWordsFromFolder();
        }

        private void LoadWordsFromFolder()
        {
            if (!Directory.Exists(wordsFolderPath))
            {
                Debug.LogError($"Words folder not found at: {wordsFolderPath}");
                return;
            }

            string[] files = Directory.GetFiles(wordsFolderPath);
            foreach (var file in files)
            {
                string extension = Path.GetExtension(file).ToLower();
                string word = Path.GetFileNameWithoutExtension(file);
                string definition = null;

                if (extension == ".md")
                {
                    definition = File.ReadAllText(file).Trim();
                }
                else if (extension == ".py")
                {
                    string content = File.ReadAllText(file);
                    string[] lines = content.Split('=');
                    if (lines.Length == 2 && lines[0].Trim() == word)
                        definition = lines[1].Trim().Trim('"');
                }
                else if (extension == ".bat")
                {
                    string content = File.ReadAllText(file);
                    if (content.StartsWith("echo", System.StringComparison.OrdinalIgnoreCase))
                        definition = content.Substring(4).Trim();
                }

                if (!string.IsNullOrEmpty(definition))
                {
                    wordBank[word] = new MatrixWord(word, definition);
                    Debug.Log($"Loaded word: {word}, Definition: {definition}");
                }
            }
        }

        public void AddWordToMatrix(string word)
        {
            if (wordBank.ContainsKey(word))
            {
                MatrixWord matrixWord = wordBank[word];
                activeWords.Add(matrixWord);
                totalPower += matrixWord.PowerGenerated;
                Debug.Log($"Added word to matrix: {word}. Total Power: {totalPower}");
            }
            else
            {
                Debug.LogWarning($"Word '{word}' not found in the word bank.");
            }
        }

        public void RemoveWordFromMatrix(string word)
        {
            MatrixWord wordToRemove = activeWords.Find(w => w.Word == word);
            if (wordToRemove != null)
            {
                activeWords.Remove(wordToRemove);
                totalPower -= wordToRemove.PowerGenerated;
                Debug.Log($"Removed word from matrix: {word}. Total Power: {totalPower}");
            }
            else
            {
                Debug.LogWarning($"Word '{word}' is not active in the matrix.");
            }
        }

        private void Update()
        {
            // Timed automation for adding and removing words
            addWordTimer += Time.deltaTime;
            removeWordTimer += Time.deltaTime;

            if (addWordTimer >= addWordInterval)
            {
                // Example word to add
                if (wordBank.Count > 0)
                {
                    string wordToAdd = new List<string>(wordBank.Keys)[Random.Range(0, wordBank.Count)];
                    AddWordToMatrix(wordToAdd);
                }
                addWordTimer = 0f;
            }

            if (removeWordTimer >= removeWordInterval && activeWords.Count > 0)
            {
                // Remove a random active word
                string wordToRemove = activeWords[Random.Range(0, activeWords.Count)].Word;
                RemoveWordFromMatrix(wordToRemove);
                removeWordTimer = 0f;
            }
        }
    }
}
