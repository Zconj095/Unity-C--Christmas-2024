using UnityEngine;
using UnityEngine.UI;

public class FeedbackManager : MonoBehaviour
{
    public Text FeedbackText;

    public void ShowMessage(string message)
    {
        FeedbackText.text = message;
        Invoke("ClearMessage", 3.0f);
    }

    private void ClearMessage()
    {
        FeedbackText.text = "";
    }
}
