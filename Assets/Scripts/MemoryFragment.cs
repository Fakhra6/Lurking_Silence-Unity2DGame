using UnityEngine;

public class MemoryFragment : MonoBehaviour
{
    public static int fragmentsCollected = 0;
    public static int totalFragments = 4;

    private bool collected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;
            fragmentsCollected++;
            Debug.Log("Fragments: " + fragmentsCollected);

            //Destroy(gameObject);

            if (fragmentsCollected >= totalFragments)
            {
                FinalDialogueTrigger.Instance.TriggerFinalDialogue();
            }
        }
    }
}
