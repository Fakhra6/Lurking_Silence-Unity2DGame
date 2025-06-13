using UnityEngine;

public class GhostChaseTrigger : MonoBehaviour
{
    [SerializeField] private GhostChaseSequence ghostChase;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ghostChase.StartChaseFromTrigger();
            gameObject.SetActive(false); // One-time use
        }
    }
}
