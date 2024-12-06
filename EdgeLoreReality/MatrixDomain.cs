using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class MatrixDomain : MonoBehaviour
    {
        // Represents a domain in the Matrix Domain system
        private class Domain
        {
            public string DomainID { get; private set; }
            public string AssociatedWord { get; set; }
            public string AssociatedCode { get; set; }

            public bool IsSynchronized { get; private set; }

            public Domain(string word, string code)
            {
                DomainID = System.Guid.NewGuid().ToString();
                AssociatedWord = word;
                AssociatedCode = code;
                IsSynchronized = true; // Default to synchronized
            }

            public void ToggleSynchronization()
            {
                IsSynchronized = !IsSynchronized;
                Debug.Log($"Domain '{DomainID}' synchronization is now {(IsSynchronized ? "ON" : "OFF")}.");
            }
        }

        // List of domains
        private List<Domain> domains = new List<Domain>();

        // Timers for automation
        private float addDomainTimer = 0f;
        private float synchronizeDomainsTimer = 0f;
        private float retrieveDomainsTimer = 0f;
        private float adjustDomainTimer = 0f;

        // Automation intervals
        private const float addDomainInterval = 5f; // Add a new domain every 5 seconds
        private const float synchronizeDomainsInterval = 10f; // Synchronize domains every 10 seconds
        private const float retrieveDomainsInterval = 7f; // Retrieve domains every 7 seconds
        private const float adjustDomainInterval = 12f; // Adjust a domain every 12 seconds

        // Add a new domain
        public void AddDomain(string word, string code)
        {
            Domain newDomain = new Domain(word, code);
            domains.Add(newDomain);
            Debug.Log($"Added Domain - ID: {newDomain.DomainID}, Word: {word}, Code: {code}, Synchronized: {newDomain.IsSynchronized}");
        }

        // Synchronize all domains
        public void SynchronizeDomains()
        {
            Debug.Log("Synchronizing all domains...");
            foreach (var domain in domains)
            {
                if (!domain.IsSynchronized)
                {
                    domain.ToggleSynchronization();
                }
            }
        }

        // Retrieve all domains
        public void RetrieveDomains()
        {
            Debug.Log("Retrieving all domains...");
            foreach (var domain in domains)
            {
                Debug.Log($"Domain - ID: {domain.DomainID}, Word: {domain.AssociatedWord}, Code: {domain.AssociatedCode}, Synchronized: {domain.IsSynchronized}");
            }
        }

        // Adjust domain operations
        public void AdjustDomain(string domainID, string newWord, string newCode)
        {
            foreach (var domain in domains)
            {
                if (domain.DomainID == domainID)
                {
                    domain.AssociatedWord = newWord;
                    domain.AssociatedCode = newCode;
                    Debug.Log($"Adjusted Domain - ID: {domainID}, New Word: {newWord}, New Code: {newCode}");
                    return;
                }
            }

            Debug.LogWarning($"Domain with ID '{domainID}' not found.");
        }

        private void Start()
        {
            Debug.Log("Matrix Domain System Initialized.");
        }

        private void Update()
        {
            // Increment timers
            addDomainTimer += Time.deltaTime;
            synchronizeDomainsTimer += Time.deltaTime;
            retrieveDomainsTimer += Time.deltaTime;
            adjustDomainTimer += Time.deltaTime;

            // Automate adding domains
            if (addDomainTimer >= addDomainInterval)
            {
                AddDomain($"Word_{domains.Count + 1}", $"Code_{domains.Count + 1}");
                addDomainTimer = 0f;
            }

            // Automate retrieving domains
            if (retrieveDomainsTimer >= retrieveDomainsInterval)
            {
                RetrieveDomains();
                retrieveDomainsTimer = 0f;
            }

            // Automate synchronizing domains
            if (synchronizeDomainsTimer >= synchronizeDomainsInterval)
            {
                SynchronizeDomains();
                synchronizeDomainsTimer = 0f;
            }

            // Automate adjusting a domain
            if (adjustDomainTimer >= adjustDomainInterval && domains.Count > 0)
            {
                string domainID = domains[Random.Range(0, domains.Count)].DomainID;
                AdjustDomain(domainID, $"AdjustedWord_{Random.Range(1, 100)}", $"AdjustedCode_{Random.Range(1, 100)}");
                adjustDomainTimer = 0f;
            }
        }
    }
}
