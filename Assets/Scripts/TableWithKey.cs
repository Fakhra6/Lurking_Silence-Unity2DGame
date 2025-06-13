using UnityEngine;

public class TableWithKey : MonoBehaviour, IInteractable
{
    public string keyID = "BedroomKey";
    public string[] dialogueLines = new string[] { "I found a key inside the drawer." };
    private bool hasBeenSearched = false;

    public void TriggerDialogue()
    {
        Debug.Log("Interacted");
        if (!hasBeenSearched)
        {
            hasBeenSearched = true;

            Object.FindFirstObjectByType<DialogueManager>().StartDialogue(dialogueLines, "YOU", () => {
                InventoryManager.Instance.AddItem(keyID);
            });
        }
        else
        {
            Object.FindFirstObjectByType<DialogueManager>().StartDialogue(new string[] { "It's just an old table." }, "YOU", () => { });
        }
    }
}
