//using System.Reflection;
using UnityEngine;
//using static UnityEditor.ShaderData;

public class OpeningDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;
    private static bool hasPlayedOpening = false;
    void Start()
    {
        

        
            if (dialogueManager == null)
        {
            Debug.LogError("DialogueManager reference not assigned!");
            return;
        }

        if (!hasPlayedOpening)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().canMove = false;
            hasPlayedOpening = true;

            string[] openingLines = new string[]
            {
            "Where... am I?",
            "This place... it's so familiar... but why can't I remember?",
            "I can’t remember how I got inside this old mansion.",
            "There’s a chill in the air... like someone’s watching me.",
            "I feel... lost. Something important is missing from my past.",
            "Maybe if I explore this place... I’ll find answers.",
            };


            string speakerName = "You";  // You can change this or make it dynamic if needed

            dialogueManager.StartDialogue(openingLines, speakerName, OnDialogueComplete);
        }


    }

    void OnDialogueComplete()
    {
        Debug.Log("Opening dialogue finished!");
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().canMove = true;

    }
}
