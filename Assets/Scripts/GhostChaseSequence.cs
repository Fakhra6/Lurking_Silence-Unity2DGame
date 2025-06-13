using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostChaseSequence : MonoBehaviour
{
    [Header("Ghost Settings")]
    [SerializeField] private Transform ghost;
    [SerializeField] private float chaseSpeed = 4f;
    [SerializeField] private float acceleration = 3f;

    [Header("Player Settings")]
    [SerializeField] private string playerTag = "Player";

    [Header("Effects")]
    [SerializeField] private AudioClip chaseMusic;
    [SerializeField] private AudioClip jumpscareSound;
    [SerializeField] private ParticleSystem appearParticles;

    private Transform player;
    private float currentSpeed = 0f;
    private bool isChasing = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag)?.transform;

        // Initially hide ghost
        ghost.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isChasing || player == null) return;

        // Smooth acceleration
        currentSpeed = Mathf.Lerp(currentSpeed, chaseSpeed, acceleration * Time.deltaTime);

        // Move horizontally only
        Vector3 targetPosition = new Vector3(player.position.x, ghost.position.y, ghost.position.z);
        ghost.position = Vector3.MoveTowards(ghost.position, targetPosition, currentSpeed * Time.deltaTime);

        UpdateGhostVisuals();
    }

    public void StartChaseFromTrigger()
    {
        if (isChasing) return;

        ghost.gameObject.SetActive(true);
        isChasing = true;
        currentSpeed = 0f;

        if (appearParticles) appearParticles.Play();
        if (chaseMusic) AudioSource.PlayClipAtPoint(chaseMusic, ghost.position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isChasing) return;
        if (other.CompareTag(playerTag))
        {
            HandleCapture();
        }
    }

    void HandleCapture()
    {
        if (jumpscareSound) AudioSource.PlayClipAtPoint(jumpscareSound, player.position, 2f);
        Time.timeScale = 0.3f;
        Invoke(nameof(ReloadScene), 1.2f);
    }

    void ReloadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateGhostVisuals()
    {
        if (ghost.position.x < player.position.x)
            ghost.localScale = new Vector3(0.0369815f, 0.04367653f, 1);
        else
            ghost.localScale = new Vector3(-0.0369815f, 0.04367653f, 1);
    }
}
