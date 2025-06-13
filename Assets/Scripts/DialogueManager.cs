using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    public GameObject dialogueBox;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    private string[] lines;
    private int index;
    private Coroutine typingCoroutine;
    private bool isTyping;
    private System.Action onDialogueComplete;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        dialogueBox = GameObject.Find("DialogueBox");

        if (dialogueBox != null)
        {
            nameText = dialogueBox.transform.Find("NameText")?.GetComponent<TMP_Text>();
            dialogueText = dialogueBox.transform.Find("DialogueText")?.GetComponent<TMP_Text>();
            dialogueBox.SetActive(false);

            Button nextButton = dialogueBox.transform.Find("Button")?.GetComponent<Button>();
            if (nextButton != null)
            {
                nextButton.onClick.RemoveAllListeners();
                nextButton.onClick.AddListener(() => AdvanceDialogue());
            }
        }
    }

    private void Update()
    {
        if (dialogueBox != null && dialogueBox.activeSelf && Keyboard.current.eKey.wasPressedThisFrame)
        {
            AdvanceDialogue();
        }
    }

    public void StartDialogue(string[] newLines, string speaker, System.Action onComplete = null)
    {
        if (dialogueBox == null)
        {
            Debug.LogWarning("DialogueBox not found!");
            return;
        }

        lines = newLines;
        index = 0;
        onDialogueComplete = onComplete;

        nameText.text = speaker;
        dialogueBox.SetActive(true);

        StartTypingCurrentLine();
    }

    private void StartTypingCurrentLine()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeLine(lines[index]));
    }

    private void AdvanceDialogue()
    {
        if (isTyping)
        {
            // Fast-forward the current line
            StopCoroutine(typingCoroutine);
            dialogueText.text = lines[index];
            isTyping = false;
        }
        else
        {
            index++;
            if (index < lines.Length)
            {
                StartTypingCurrentLine();
            }
            else
            {
                dialogueBox.SetActive(false);
                onDialogueComplete?.Invoke();
            }
        }
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.02f);
        }

        isTyping = false;
    }
}
