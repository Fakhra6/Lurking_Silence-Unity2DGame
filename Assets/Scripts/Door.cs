using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    [Header("Audio")]
    public AudioClip doorOpenSound;

    public string spawnIDForNextScene; // ID of spawn point in next scene

    [Tooltip("The scene to load when this door is opened")]
    public string sceneToLoad;

    [Tooltip("Key required to open this door, leave empty if no key required")]
    public string requiredKeyID = "";

    private bool isUnlocked = false;

    public void TriggerDialogue()
    {
        if (requiredKeyID == "" || InventoryManager.Instance.HasItem(requiredKeyID))
        {
            if (!isUnlocked)
            {
                isUnlocked = true;
                // You can show some dialogue or UI feedback here
                Debug.Log("Door unlocked! Loading next scene...");
                if (doorOpenSound)
                {
                    AudioSource.PlayClipAtPoint(doorOpenSound, transform.position);
                }
            }

            //float delay = doorOpenSound != null ? doorOpenSound.length : 0f;
            float delay = 0.5f;
            Invoke(nameof(LoadNextScene), delay);
        }

          
        else
        {
            // Show locked door message
            Object.FindFirstObjectByType<DialogueManager>().StartDialogue(
                new string[] { "The door is locked. I need a key." },
                "YOU",
                () => { }
            );
        }
    }

    private void LoadNextScene()
    {
        //SceneManager.LoadScene(sceneToLoad);
        PlayerSpawnManager.Instance.SetSpawnPoint(spawnIDForNextScene);
        SceneManager.LoadScene(sceneToLoad);
    }
}
