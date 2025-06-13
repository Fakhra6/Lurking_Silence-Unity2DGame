using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDialogueTrigger : MonoBehaviour
{
    public static FinalDialogueTrigger Instance;

    public string[] finalLines = new string[]
    {
        "I remember now...",
        "All the pieces... my sister, my family, the fire, the truth.",
        "The guilf of not being able to save them.. ",
        "The guilt that my little sister died right in front of me.. ",
        "The fear and guilt... It was all fear and my own guilt.",
        "It's time to move on..."
    };

    private void Awake()
    {
        Instance = this;
    }

    public void TriggerFinalDialogue()
    {
        TimerManager timer = FindFirstObjectByType<TimerManager>();
        if (timer != null)
        {
            timer.StopTimer(); // or timer.StartTimer();
        }
        DialogueManager.Instance.StartDialogue(finalLines, "You", OnFinalDialogueEnd);

    }

    void OnFinalDialogueEnd()
    {
        Debug.Log("Game Over! Showing credits...");
        SceneManager.LoadScene("Credits");
        // Load end scene, fade out, or show credits
        // Example:
        // SceneManager.LoadScene("CreditsScene");
    }
}
