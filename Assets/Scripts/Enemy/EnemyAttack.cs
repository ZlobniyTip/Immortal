using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyScaning))]
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;

    private Coroutine _attack;
    private Enemy _enemy;
    private EnemyScaning _enemyScaning;
    private Character _character;
    private float _distance;

    public event Action Attacking;

    public bool IsAttack { get; private set; }

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _enemyScaning = GetComponent<EnemyScaning>();

        _enemy.Died += StopAttacking;
        _enemyScaning.CharacterFinded += StartAttacking;
    }

    private void OnDestroy()
    {
        _enemy.Died -= StopAttacking;
        _enemyScaning.CharacterFinded -= StartAttacking;
    }

    private IEnumerator Attack()
    {
        var delay = new WaitForSeconds(_weapon.DelayBetweenAttack);

        while (_character)
        {
            _distance = Vector3.Distance(transform.position, _enemyScaning.Target.transform.position);

            if (_distance <= _weapon.AttackDistance)
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

    private void StopAttacking()
    {
        StopCoroutine(_attack);
    }

    private void StartAttacking(Character character)
    {
        _character = character;
        _attack = StartCoroutine(Attack());
    }
}