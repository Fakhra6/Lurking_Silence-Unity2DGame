using UnityEngine;
using TMPro;

public class FragmentUI : MonoBehaviour
{
    public TMP_Text fragmentText;

    void Update()
    {
        fragmentText.text = $"Fragments: {MemoryFragment.fragmentsCollected} / {MemoryFragment.totalFragments}";
    }
}
