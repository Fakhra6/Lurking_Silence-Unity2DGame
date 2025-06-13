using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickerZone : MonoBehaviour
{
    public Light2D light2D;
    public float minIntensity = 0.6f;
    public float maxIntensity = 1.2f;
    public float flickerSpeed = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(Flicker());
        
    }
    System.Collections.IEnumerator Flicker()
    {
        while (true)
        {
            light2D.intensity = Random.Range(minIntensity, maxIntensity);
            yield return new WaitForSeconds(Random.Range(0.02f, flickerSpeed));
        }
    }
}
