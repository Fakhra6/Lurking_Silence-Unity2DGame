using UnityEngine;

public class HiddenPathTrigger : MonoBehaviour
{
    [Header("Object Settings")]
    [SerializeField] private Transform movingObject;  // The object blocking the path
    [SerializeField] private Vector2 moveOffset = new Vector2(0, 3);  // Movement direction/distance
    [SerializeField] private float moveSpeed = 3f;

    [Header("Trigger Settings")]
    [SerializeField] private bool oneTimeUse = true;
    [SerializeField] private LayerMask triggerLayers;  // Set to player layer

    private Vector2 originalPosition;
    private Vector2 targetPosition;
    private bool isTriggered = false;
    private bool hasTriggered = false;

    void Start()
    {
        originalPosition = movingObject.position;
        targetPosition = originalPosition + moveOffset;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!oneTimeUse || !hasTriggered)
        {
            if (((1 << other.gameObject.layer) & triggerLayers) != 0)
            {
                isTriggered = true;
                hasTriggered = true;
            }
        }
    }

    void Update()
    {
        if (isTriggered)
        {
            // Smooth movement using Lerp
            movingObject.position = Vector2.Lerp(
                movingObject.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            
        }
    }

   
}