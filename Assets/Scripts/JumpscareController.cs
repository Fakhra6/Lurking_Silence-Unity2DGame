using UnityEngine;
using System.Collections;

public class Jumpscare2D : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private SpriteRenderer ghostSprite;  // Assign in Inspector
    [SerializeField] private float slideDistance = 10f;
    [SerializeField] private float slideSpeed = 8f;

    [Header("Audio")]
    [SerializeField] private AudioClip scareSound;
    [SerializeField] private float soundVolume = 0.7f;

    [Header("Trigger")]
    [SerializeField] private Collider2D triggerZone;
    [SerializeField] private bool oneTimeOnly = true;

    private Vector2 _originalPos;
    private bool _hasScared = false;

    void Start()
    {
        if (!ghostSprite) Debug.LogError("Ghost Sprite not assigned!");
        
        if (!GetComponent<Collider2D>()) Debug.LogError("No Collider2D attached!");

        _originalPos = ghostSprite.transform.position;
        ghostSprite.enabled = false; // Hide initially
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (oneTimeOnly && _hasScared) return;
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ActivateJumpscare());
        }
    }

    IEnumerator ActivateJumpscare()
    {
        _hasScared = true;

        // 1. Brief pause for suspense
        yield return new WaitForSeconds(0.3f);

        // 2. Play sound
        AudioSource.PlayClipAtPoint(scareSound, Camera.main.transform.position, soundVolume);

        // 3. Make ghost visible
        ghostSprite.enabled = true;

        // 4. Slide ghost horizontally
        float elapsed = 0f;
        Vector2 startPos = _originalPos;
        Vector2 endPos = startPos + (Vector2.right * slideDistance);

        while (elapsed < 1f)
        {
            ghostSprite.transform.position = Vector2.Lerp(startPos, endPos, elapsed);
            elapsed += Time.deltaTime * slideSpeed;
            yield return null;
        }

        // 5. Hide ghost
        ghostSprite.enabled = false;
        ghostSprite.transform.position = _originalPos; // Reset position
    }

    // Call this to reset if needed
    public void ResetJumpscare()
    {
        _hasScared = false;
    }
}