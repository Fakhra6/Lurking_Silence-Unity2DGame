using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private HashSet<string> items = new HashSet<string>();

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        //else
        //{
        //    Destroy(gameObject); // Prevent duplicates
        //}
    }

    public void AddItem(string item)
    {
        items.Add(item);
        Debug.Log($"Item added: {item}");
    }

    public bool HasItem(string item)
    {
        return items.Contains(item);
    }

    public void RemoveItem(string item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log($"Item removed: {item}");
        }
    }
}
