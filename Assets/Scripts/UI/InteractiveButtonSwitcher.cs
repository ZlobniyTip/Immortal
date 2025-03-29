using UnityEngine;

public class InteractiveButtonSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _button;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            _button.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            _button.SetActive(false);
        }
    }

    private void SingPlayerAction()
    {

    }
}