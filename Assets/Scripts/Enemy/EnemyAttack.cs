using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyScaning))]
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;

    private EnemyScaning _enemyScaning;
    private Character _character;
    private float _distance;

    public event Action Attacking;

    public bool IsAttack { get; private set; }

    private void Awake()
    {
        _enemyScaning = GetComponent<EnemyScaning>();

        _enemyScaning.CharacterFinded += StartAttacking;
    }

    private void OnDisable()
    {
        StopCoroutine(Attack());
        _enemyScaning.CharacterFinded -= StartAttacking;
    }

    private IEnumerator Attack()
    {
        var delay = new WaitForSeconds(_weapon.DelayBetweenAttack);

        while (_character)
        {
            _distance = Vector3.Distance(transform.position, _enemyScaning.Target.transform.position);

            if (_distance <= _weapon.AttackRange)
            {
                Attacking?.Invoke();
                IsAttack = true;
                _character.TakeDamage(_weapon.Damage);
            }
            else
            {
                IsAttack = false;
            }

            yield return delay;
        }
    }

    private void StartAttacking(Character character)
    {
        _character = character;
        StartCoroutine(Attack());
    }
}