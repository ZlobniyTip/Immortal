public class HealthBar : Bar
{
    private Health _health;

    private void OnDestroy()
    {
        _health.HealthChanged -= OnValueChanged;
    }

    public void SubscribeHealthBar(Health health)
    {
        _health = health;
        _health.HealthChanged += OnValueChanged;
    }
}