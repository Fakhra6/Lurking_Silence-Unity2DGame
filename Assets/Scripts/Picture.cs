using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;
//using UnityEditor.Rendering;

public class Picture : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private SpriteRenderer ghostSprite;  // Assign in Inspector
    

    

    [Header("Trigger")]
    [SerializeField] private Collider2D triggerZone;
    [SerializeField] private bool oneTimeOnly = true;

    private Vector2 _originalPos;
    private bool _hasScared = false;
    public Light2D light2D;
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


        // 3. Make ghost visible
        ghostSprite.enabled = true;
        light2D.intensity = 1.1f;

        yield return new WaitForSeconds(3.0f);


        ghostSprite.enabled = false;
        light2D.intensity = 0.06f;

    }

    
}