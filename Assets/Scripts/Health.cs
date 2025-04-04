using System;
using System.Collections;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected int _maxValue;

    public int CurrentHealth { get; private set; }

    public event Action Died;
    public event Action<int, int> HealthChanged;

    protected virtual void Start()
    {
        CurrentHealth = _maxValue;
        HealthChanged?.Invoke(CurrentHealth, _maxValue);
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        HealthChanged?.Invoke(CurrentHealth, _maxValue);

        if (CurrentHealth <= 0)
        {
            Died?.Invoke();
        }
    }

    protected abstract IEnumerator Dead();
}