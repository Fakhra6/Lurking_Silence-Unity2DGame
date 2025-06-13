using UnityEngine;

public class TriggerDialogueZone : MonoBehaviour
{
    public string[] lines;
    public string speakerName = "???";
    public bool destroyAfter = false;
   
    

    private bool triggered = false;
    void Start()
    {
        //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().canMove = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (!triggered && other.CompareTag("Player"))
        {
            
            triggered = true;
            Object.FindFirstObjectByType<DialogueManager>().StartDialogue(lines, speakerName, () => {
                if (destroyAfter) Destroy(gameObject);
            });
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().canMove = true;
    }
}
