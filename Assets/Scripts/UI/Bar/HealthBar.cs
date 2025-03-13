using UnityEngine;

public class HealthBar : Bar
{
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.HealthChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnValueChanged;
    }
}