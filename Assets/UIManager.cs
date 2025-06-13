using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public FloatingJoystick joystick;
    public Button runButton;
    public Button interactButton;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Hide mobile UI if not on a mobile platform
        if (!Application.isMobilePlatform)
        {
            gameObject.SetActive(false);
        }
    }
}
