using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable currentInteractable;
    private PlayerControls controls;

    [Header("Mobile Support")]
    public bool isMobile = false;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Interact.performed += ctx =>
        {
            if (!isMobile)
                TryInteract();
        };
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void Start()
    {
        if (Application.isMobilePlatform)
            isMobile = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
            currentInteractable = interactable;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (currentInteractable != null && other.gameObject == ((MonoBehaviour)currentInteractable).gameObject)
            currentInteractable = null;
    }

    public void TryInteract()  // Made public for mobile button
    {
        currentInteractable?.TriggerDialogue();
    }
}
