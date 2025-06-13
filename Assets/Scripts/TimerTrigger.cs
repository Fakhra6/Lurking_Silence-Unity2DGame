using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    public TimerManager timer;

    private void OnTriggerEnter2D(Collider2D other) // Use OnTriggerEnter if 3D
    {
        if (other.CompareTag("Player"))
        {
            timer.StartTimer();
            gameObject.SetActive(false); // Optional: disable trigger after use
        }
    }
}
