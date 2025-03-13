using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _attackDistance = 1;
    [SerializeField] private float _delayBetweenAttack = 1;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackAngle = 45f;
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private float _attackRate = 1f;

    public float AttackRate => _attackRate;
    public float AttackRange => _attackRange;
    public float AttackAngle => _attackAngle;
    public int Damage => _damage;
    public float AttackDistance => _attackDistance;
    public float DelayBetweenAttack => _delayBetweenAttack;
}