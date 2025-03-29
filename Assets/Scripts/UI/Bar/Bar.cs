using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    private readonly float _recoveryRate = 0.6f;

    [SerializeField] protected Slider _barFilling;
    [SerializeField] private TMP_Text _text;

    private Coroutine _changeValue;

    public void OnValueChanged(int value, int maxValue)
    {
        if (_changeValue != null)
        {
            StopCoroutine(_changeValue);
        }

        _text.text = $"{value} / {maxValue}";
        _changeValue = StartCoroutine(ChangeHealthBar((float)value / maxValue));
    }

    private IEnumerator ChangeHealthBar(float target)
    {
        while (_barFilling.value != target)
        {
            _barFilling.value = Mathf.MoveTowards(_barFilling.value, target, _recoveryRate * Time.deltaTime);

            yield return null;
        }

        yield break;
    }
}