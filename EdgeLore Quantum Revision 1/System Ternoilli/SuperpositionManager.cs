using UnityEngine;

public class SuperpositionManager : MonoBehaviour
{
    [Header("Superposition Settings")]
    [Tooltip("Populate this array with valid Vector2 states in the Inspector.")]
    public Vector2[] States;

    /// <summary>
    /// Collapses the quantum state to one of the superposed states randomly.
    /// </summary>
    /// <returns>The collapsed Vector2 state.</returns>
    public Vector2 CollapseToState()
    {
        if (States == null)
        {
            Debug.LogError("States array is null in SuperpositionManager.");
            return Vector2.zero; // Return a default value or handle appropriately
        }

        if (States.Length == 0)
        {
            Debug.LogError("States array is empty in SuperpositionManager.");
            return Vector2.zero; // Return a default value or handle appropriately
        }

        int randomIndex = Random.Range(0, States.Length);
        Vector2 collapsedState = States[randomIndex];
        Debug.Log($"Collapsed to State: {collapsedState}");
        return collapsedState;
    }

    /// <summary>
    /// Initializes the States array with default values if it's empty or null.
    /// Call this method before using CollapseToState if States are not set via Inspector.
    /// </summary>
    public void InitializeStates(int numberOfStates)
    {
        if (numberOfStates <= 0)
        {
            Debug.LogError("Number of states must be greater than zero.");
            return;
        }

        States = new Vector2[numberOfStates];
        for (int i = 0; i < numberOfStates; i++)
        {
            States[i] = new Vector2(i, Mathf.Sin(i));
        }

        Debug.Log($"Initialized States with {numberOfStates} elements.");
    }
}
