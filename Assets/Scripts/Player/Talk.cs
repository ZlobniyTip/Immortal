using TMPro;
using UnityEngine;

public class Talk : MonoBehaviour
{
    private TMP_Text _dialogueText;
    private GameObject _dialoguePanel;
    private GameObject _tradePanel;
    private bool _inTalking = false;

    private void Update()
    {
        if (_inTalking)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                _dialoguePanel.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                _tradePanel.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out NpcDialogue npc))
        {
            npc.SwitchDialogueButton(true);
            _inTalking = true;
            _dialogueText.text = npc.GiveTextDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out NpcDialogue npc))
        {
            npc.SwitchDialogueButton(false);
            _dialoguePanel.SetActive(false);
            _tradePanel.SetActive(false);
            _inTalking = false;
        }
    }

    public void GetLinkTalkPanel(GameObject dialoguePanel, GameObject tradePanel, TMP_Text dialogueText)
    {
        _tradePanel = tradePanel;
        _dialoguePanel = dialoguePanel;
        _dialogueText = dialogueText;
    }
}