using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixEntanglement : MonoBehaviour
    {
        // Represents a word with its entangled connections
        private class WordNode
        {
            public string Word { get; private set; }
            public WordNode Proton { get; private set; }
            public WordNode Electron { get; private set; }

            public WordNode(string word)
            {
                Word = word;
            }

            public void Entangle(WordNode proton, WordNode electron)
            {
                Proton = proton;
                Electron = electron;
                Debug.Log($"Word '{Word}' entangled with Proton '{Proton.Word}' and Electron '{Electron.Word}'.");
            }
        }

        // Represents a server containing entangled words
        private class MatrixServer
        {
            public string ServerID { get; private set; }
            public List<WordNode> EntangledWords { get; private set; }

            public MatrixServer()
            {
                ServerID = System.Guid.NewGuid().ToString();
                EntangledWords = new List<WordNode>();
            }

            public void AddWordNode(WordNode wordNode)
            {
                EntangledWords.Add(wordNode);
                Debug.Log($"Word '{wordNode.Word}' added to Server '{ServerID}'.");
            }
        }

        // List of servers and domains
        private List<MatrixServer> servers = new List<MatrixServer>();
        private Dictionary<string, List<MatrixServer>> domains = new Dictionary<string, List<MatrixServer>>();

        // Timers for automation
        private float createServerTimer = 0f;
        private float addWordTimer = 0f;
        private float entangleWordsTimer = 0f;
        private float createDomainTimer = 0f;
        private float addServerToDomainTimer = 0f;

        // Automation intervals
        private const float createServerInterval = 5f; // Create a server every 5 seconds
        private const float addWordInterval = 3f; // Add a word every 3 seconds
        private const float entangleWordsInterval = 7f; // Entangle words every 7 seconds
        private const float createDomainInterval = 10f; // Create a domain every 10 seconds
        private const float addServerToDomainInterval = 12f; // Add a server to a domain every 12 seconds

        // Create a new server
        public void CreateServer()
        {
            MatrixServer server = new MatrixServer();
            servers.Add(server);
            Debug.Log($"Server Created - ID: {server.ServerID}");
        }

        // Add a word node to a server
        public void AddWordToServer(string word, string serverID)
        {
            MatrixServer server = servers.Find(s => s.ServerID == serverID);
            if (server != null)
            {
                WordNode wordNode = new WordNode(word);
                server.AddWordNode(wordNode);
            }
            else
            {
                Debug.LogWarning($"Server with ID '{serverID}' not found.");
            }
        }

        // Entangle a word node with two others
        public void EntangleWords(string word, string protonWord, string electronWord, string serverID)
        {
            MatrixServer server = servers.Find(s => s.ServerID == serverID);
            if (server != null)
            {
                WordNode wordNode = server.EntangledWords.Find(w => w.Word == word);
                WordNode protonNode = server.EntangledWords.Find(w => w.Word == protonWord);
                WordNode electronNode = server.EntangledWords.Find(w => w.Word == electronWord);

                if (wordNode != null && protonNode != null && electronNode != null)
                {
                    wordNode.Entangle(protonNode, electronNode);
                }
                else
                {
                    Debug.LogWarning("One or more words not found in the server.");
                }
            }
            else
            {
                Debug.LogWarning($"Server with ID '{serverID}' not found.");
            }
        }

        // Create a new domain
        public void CreateDomain(string domainName)
        {
            if (!domains.ContainsKey(domainName))
            {
                domains[domainName] = new List<MatrixServer>();
                Debug.Log($"Domain Created - Name: {domainName}");
            }
            else
            {
                Debug.LogWarning($"Domain '{domainName}' already exists.");
            }
        }

        // Add a server to a domain
        public void AddServerToDomain(string domainName, string serverID)
        {
            if (domains.ContainsKey(domainName))
            {
                MatrixServer server = servers.Find(s => s.ServerID == serverID);
                if (server != null)
                {
                    domains[domainName].Add(server);
                    Debug.Log($"Server '{serverID}' added to Domain '{domainName}'.");
                }
                else
                {
                    Debug.LogWarning($"Server with ID '{serverID}' not found.");
                }
            }
            else
            {
                Debug.LogWarning($"Domain '{domainName}' does not exist.");
            }
        }

        private void Start()
        {
            Debug.Log("Matrix Entanglement Initialized.");
        }

        private void Update()
        {
            // Increment timers
            createServerTimer += Time.deltaTime;
            addWordTimer += Time.deltaTime;
            entangleWordsTimer += Time.deltaTime;
            createDomainTimer += Time.deltaTime;
            addServerToDomainTimer += Time.deltaTime;

            // Automate server creation
            if (createServerTimer >= createServerInterval)
            {
                CreateServer();
                createServerTimer = 0f;
            }

            // Automate adding words to the last server
            if (addWordTimer >= addWordInterval && servers.Count > 0)
            {
                AddWordToServer($"Word_{Random.Range(1, 100)}", servers[servers.Count - 1].ServerID);
                addWordTimer = 0f;
            }

            // Automate entangling words
            if (entangleWordsTimer >= entangleWordsInterval && servers.Count > 0 && servers[servers.Count - 1].EntangledWords.Count >= 3)
            {
                var lastServer = servers[servers.Count - 1];
                EntangleWords(
                    lastServer.EntangledWords[0].Word,
                    lastServer.EntangledWords[1].Word,
                    lastServer.EntangledWords[2].Word,
                    lastServer.ServerID
                );
                entangleWordsTimer = 0f;
            }

            // Automate domain creation
            if (createDomainTimer >= createDomainInterval)
            {
                CreateDomain($"Domain_{domains.Count + 1}");
                createDomainTimer = 0f;
            }

            // Automate adding servers to domains
            if (addServerToDomainTimer >= addServerToDomainInterval && domains.Count > 0 && servers.Count > 0)
            {
                AddServerToDomain($"Domain_{domains.Count}", servers[servers.Count - 1].ServerID);
                addServerToDomainTimer = 0f;
            }
        }
    }
}
