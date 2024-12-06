using System.Collections;
using UnityEngine;

public class LazyUpdateManager : MonoBehaviour
{
    private bool updatePending = false;

    public void ScheduleUpdate(System.Action updateAction)
    {
        if (!updatePending)
        {
            updatePending = true;
            StartCoroutine(DelayedUpdate(updateAction));
        }
    }

    private IEnumerator DelayedUpdate(System.Action updateAction)
    {
        yield return new WaitForSeconds(0.1f); // Delay for batching
        updateAction?.Invoke();
        updatePending = false;
    }
}
