using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    public string[] lines;
    public string speakerName = "???";
    public bool destroyAfter = false;

    public void TriggerDialogue()
    {

        Object.FindFirstObjectByType<DialogueManager>().StartDialogue(lines, speakerName, () => {
            if (destroyAfter) Destroy(gameObject);
        });
    }
}
