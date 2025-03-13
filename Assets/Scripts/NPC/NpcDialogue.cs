using System.Collections.Generic;
using UnityEngine;

public class NpcDialogue : MonoBehaviour
{
    [SerializeField] private List<string> _texts;

    public string GiveTextDialogue()
    {
        int random = Random.RandomRange(0, _texts.Count);

        return _texts[random];
    }
}