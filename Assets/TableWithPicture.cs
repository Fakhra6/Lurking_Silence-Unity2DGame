using UnityEngine;

public class TableWithPhoto : MonoBehaviour, IInteractable
{
    public GameObject familyPhotoSprite;  // Drag your hidden Sprite GameObject here
    public string[] dialogueLines = new string[] { "It's a picture of a family... That looks like me..." };
    private bool hasBeenViewed = false;

    public void TriggerDialogue()
    {
        if (!hasBeenViewed)
        {
            hasBeenViewed = true;

            Object.FindFirstObjectByType<DialogueManager>().StartDialogue(dialogueLines, "YOU", () => {
                familyPhotoSprite.SetActive(true); // Show photo after dialogue
            });
        }
        else
        {
            Object.FindFirstObjectByType<DialogueManager>().StartDialogue(new string[] { "Just the family photo again." }, "YOU", () => { });
        }
    }
}
