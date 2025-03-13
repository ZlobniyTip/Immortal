using TMPro;
using UnityEngine;

public class Talk : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _text;

    private bool _inTalking = false;

    private void Update()
    {
        if (_inTalking)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                _panel.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out NpcDialogue npc))
        {
            _inTalking = true;
            _text.text = npc.GiveTextDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out NpcDialogue npc))
        {
            _panel.SetActive(false);
            _inTalking = false;
        }
    }
}