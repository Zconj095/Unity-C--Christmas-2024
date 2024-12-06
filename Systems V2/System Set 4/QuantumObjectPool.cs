using System.Collections.Generic;
using UnityEngine;

public class QuantumObjectPool : MonoBehaviour
{
    public GameObject prefab;
    private Queue<GameObject> pool;
    private List<float[]> hypervectors;

    void Start()
    {
        pool = new Queue<GameObject>();

        // Access the public Hypervectors property
        var manager = FindObjectOfType<HypervectorandQubitQuantumManager>();
        if (manager != null)
        {
            hypervectors = manager.Hypervectors;

            for (int i = 0; i < hypervectors.Count; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                pool.Enqueue(obj);
            }
        }
        else
        {
            Debug.LogError("HypervectorandQubitQuantumManager not found!");
        }
    }

    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        return null;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
