using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class QuantumSceneOptimizer : MonoBehaviour
{
    private HypervectorandQubitQuantumManager hypervectorandQubitQuantumManager;

    void Start()
    {
        hypervectorandQubitQuantumManager = FindObjectOfType<HypervectorandQubitQuantumManager>();
        
        if (hypervectorandQubitQuantumManager != null)
        {
            float measurement = hypervectorandQubitQuantumManager.QuantumOperationQuBit.Measure();
            StartCoroutine(LoadSceneAsync("NextScene", measurement));
        }
        else
        {
            Debug.LogError("HypervectorandQubitQuantumManager or QuantumOperationQuBit not initialized!");
        }
    }

    private IEnumerator LoadSceneAsync(string sceneName, float optimizationFactor)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            Debug.Log($"Loading progress: {asyncLoad.progress * 100}%");
            if (asyncLoad.progress >= optimizationFactor)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
