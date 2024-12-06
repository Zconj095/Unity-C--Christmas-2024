using UnityEngine;
using System.Collections.Generic;

namespace edgelorereality
{
    public class SystemAdministrationCreationSystem : MonoBehaviour
    {
        // Represents a created object in the system
        private class CreatorObject
        {
            public string ObjectID { get; private set; }
            public string Name { get; private set; }
            public Vector3 Position { get; private set; }
            public Vector3 Scale { get; private set; }
            public string Value { get; private set; } // Defines the existence of the object

            public CreatorObject(string name, Vector3 position, Vector3 scale, string value)
            {
                ObjectID = System.Guid.NewGuid().ToString();
                Name = name;
                Position = position;
                Scale = scale;
                Value = value;
            }

            public void UpdateValue(string newValue)
            {
                Value = newValue;
                Debug.Log($"Object '{Name}' updated with new value: {Value}");
            }

            public void UpdateTransform(Vector3 newPosition, Vector3 newScale)
            {
                Position = newPosition;
                Scale = newScale;
                Debug.Log($"Object '{Name}' updated to Position: {Position}, Scale: {Scale}");
            }

            public void Display()
            {
                Debug.Log($"Object ID: {ObjectID}, Name: {Name}, Position: {Position}, Scale: {Scale}, Value: {Value}");
            }
        }

        // List of created objects
        private List<CreatorObject> createdObjects = new List<CreatorObject>();

        // Create a new object
        public void CreateObject(string name, Vector3 position, Vector3 scale, string value)
        {
            CreatorObject newObject = new CreatorObject(name, position, scale, value);
            createdObjects.Add(newObject);
            Debug.Log($"Object Created - ID: {newObject.ObjectID}, Name: {name}, Value: {value}");
        }

        // Modify an existing object
        public void ModifyObject(string objectID, Vector3 newPosition, Vector3 newScale, string newValue)
        {
            CreatorObject obj = createdObjects.Find(o => o.ObjectID == objectID);
            if (obj != null)
            {
                obj.UpdateTransform(newPosition, newScale);
                obj.UpdateValue(newValue);
            }
            else
            {
                Debug.LogWarning($"Object with ID '{objectID}' not found.");
            }
        }

        // Display all objects
        public void DisplayObjects()
        {
            Debug.Log("Displaying all created objects...");
            foreach (var obj in createdObjects)
            {
                obj.Display();
            }
        }

        private void Start()
        {
            Debug.Log("System Administration Creation System Initialized.");

            // Initial setup
            CreateObject("Sphere", new Vector3(0, 0, 0), new Vector3(1, 1, 1), "Solid");
            CreateObject("Cube", new Vector3(2, 2, 2), new Vector3(1, 1, 1), "Hollow");

            // Automate actions
            InvokeRepeating(nameof(AutoCreateObject), 2f, 5f); // Create objects every 5 seconds
            InvokeRepeating(nameof(AutoModifyObject), 3f, 7f); // Modify objects every 7 seconds
            InvokeRepeating(nameof(DisplayObjects), 5f, 10f); // Display objects every 10 seconds
        }

        private void AutoCreateObject()
        {
            string[] objectNames = { "Pyramid", "Cylinder", "Prism", "Torus" };
            string[] values = { "Solid", "Hollow", "Liquid", "Gas" };

            string name = objectNames[Random.Range(0, objectNames.Length)];
            string value = values[Random.Range(0, values.Length)];
            Vector3 position = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            Vector3 scale = new Vector3(Random.Range(1f, 3f), Random.Range(1f, 3f), Random.Range(1f, 3f));

            CreateObject(name, position, scale, value);
        }

        private void AutoModifyObject()
        {
            if (createdObjects.Count > 0)
            {
                CreatorObject obj = createdObjects[Random.Range(0, createdObjects.Count)];
                Vector3 newPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
                Vector3 newScale = new Vector3(Random.Range(1f, 3f), Random.Range(1f, 3f), Random.Range(1f, 3f));
                string newValue = Random.Range(0, 2) == 0 ? "Plasma" : "Energy";

                ModifyObject(obj.ObjectID, newPosition, newScale, newValue);
            }
            else
            {
                Debug.LogWarning("No objects available to modify.");
            }
        }
    }
}
