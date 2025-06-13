using UnityEngine;

public class InputInitializer : MonoBehaviour
{
    public GameObject mobileUIPrefab;

    void Awake()
    {
        if (Application.isMobilePlatform && UIManager.Instance == null)
        {
            Instantiate(mobileUIPrefab);
        }
    }
}
