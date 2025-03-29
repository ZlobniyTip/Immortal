using System.Collections.Generic;
using UnityEngine;

public class NpcDialogue : MonoBehaviour
{
    [SerializeField] private GameObject _dialogueButton;
    [SerializeField] private GameObject _tradeButton;
    [SerializeField] private bool _isTrader = false;

    [SerializeField] private List<string> _texts;

    public string GiveTextDialogue()
    {
        int random = Random.RandomRange(0, _texts.Count);

        return _texts[random];
    }

    public void SwitchDialogueButton(bool isAvtive)
    {
        if (_isTrader)
        {
            _tradeButton.SetActive(isAvtive);
        }
        else
        {
            _dialogueButton.SetActive(isAvtive);
        }
    }
}