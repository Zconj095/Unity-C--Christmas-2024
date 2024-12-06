using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixCodingSystem : MonoBehaviour
    {
        // Represents a piece of code being processed
        private class MatrixCode
        {
            public string CodeID { get; private set; }
            public string Content { get; private set; }
            public bool HasErrors { get; private set; }

            public MatrixCode(string content)
            {
                CodeID = System.Guid.NewGuid().ToString();
                Content = content;
                HasErrors = DetectErrors(content);
            }

            private bool DetectErrors(string content)
            {
                // Example: Simulate error detection
                return content.Contains("BUG");
            }
        }

        // Simulates a coding compiler task manager
        private List<MatrixCode> compiledCode = new List<MatrixCode>();
        private List<MatrixCode> debuggingQueue = new List<MatrixCode>();

        // Timers for automation
        private float createCodeTimer = 0f;
        private float debugCodeTimer = 0f;
        private float analyzeCodeTimer = 0f;

        // Automation intervals
        private const float createCodeInterval = 5f; // Create code every 5 seconds
        private const float debugCodeInterval = 7f; // Debug code every 7 seconds
        private const float analyzeCodeInterval = 10f; // Analyze compiled code every 10 seconds

        // Create a new code block
        public void CreateCode(string content)
        {
            MatrixCode code = new MatrixCode(content);
            Debug.Log($"New code created - ID: {code.CodeID}, Has Errors: {code.HasErrors}");

            if (code.HasErrors)
            {
                debuggingQueue.Add(code);
                Debug.LogWarning($"Code sent to debugging queue - ID: {code.CodeID}");
            }
            else
            {
                SendToCompiler(code);
            }
        }

        // Debug a piece of code
        public void DebugCode()
        {
            if (debuggingQueue.Count > 0)
            {
                MatrixCode code = debuggingQueue[0];
                debuggingQueue.RemoveAt(0);

                code = new MatrixCode(code.Content.Replace("BUG", "")); // Simulated debugging
                Debug.Log($"Debugged code - ID: {code.CodeID}, Has Errors: {code.HasErrors}");

                if (!code.HasErrors)
                {
                    SendToCompiler(code);
                }
                else
                {
                    debuggingQueue.Add(code); // Return to debugging queue if errors persist
                }
            }
            else
            {
                Debug.LogWarning("No code in the debugging queue.");
            }
        }

        // Send code to the compiler
        private void SendToCompiler(MatrixCode code)
        {
            compiledCode.Add(code);
            Debug.Log($"Code sent to compiler - ID: {code.CodeID}");
        }

        // Analyze all compiled code
        public void AnalyzeCompiledCode()
        {
            Debug.Log("Analyzing all compiled code...");
            foreach (var code in compiledCode)
            {
                Debug.Log($"Compiled Code - ID: {code.CodeID}, Content: {code.Content}");
            }
        }

        private void Start()
        {
            Debug.Log("Matrix Coding System Initialized.");
        }

        private void Update()
        {
            // Increment timers
            createCodeTimer += Time.deltaTime;
            debugCodeTimer += Time.deltaTime;
            analyzeCodeTimer += Time.deltaTime;

            // Automate creating code blocks
            if (createCodeTimer >= createCodeInterval)
            {
                // Alternate between clean and buggy code
                string content = Random.value > 0.5f ? "Example code without BUG" : "Example code with BUG";
                CreateCode(content);
                createCodeTimer = 0f;
            }

            // Automate debugging code
            if (debugCodeTimer >= debugCodeInterval)
            {
                DebugCode();
                debugCodeTimer = 0f;
            }

            // Automate analyzing compiled code
            if (analyzeCodeTimer >= analyzeCodeInterval)
            {
                AnalyzeCompiledCode();
                analyzeCodeTimer = 0f;
            }
        }
    }
}
