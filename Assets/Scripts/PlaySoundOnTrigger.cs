using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{
    public AudioClip soundEffect;
    public string triggerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(triggerTag) && soundEffect != null)
        {
            AudioSource.PlayClipAtPoint(soundEffect, transform.position);
        }
    }
}
