using UnityEngine;

public class SuperpositionAnimator : MonoBehaviour
{
    [SerializeField] private GameObject qubitSphere;
    private float animationDuration = 2.0f;

    public void AnimateSuperposition(int qubitIndex, int numStates)
    {
        Debug.Log($"Animating superposition for Qubit {qubitIndex} into {numStates} states...");

        for (int i = 0; i < numStates; i++)
        {
            GameObject superposedState = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            superposedState.transform.position = qubitSphere.transform.position;
            superposedState.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            superposedState.GetComponent<Renderer>().material = new Material(Shader.Find("Standard"))
            {
                color = new Color(1, 1, 0, 0.5f) // Yellow, semi-transparent
            };

            Vector3 targetPosition = qubitSphere.transform.position + Random.insideUnitSphere * 0.5f;
            StartCoroutine(MoveToPosition(superposedState, targetPosition, animationDuration));
        }
    }

    private System.Collections.IEnumerator MoveToPosition(GameObject obj, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = obj.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = targetPosition;
        Destroy(obj, 1.0f); // Remove the object after animation
    }
}
