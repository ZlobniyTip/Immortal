using System;
using UnityEngine;
using Photon.Pun;

public class ComboAttack : MonoBehaviourPunCallbacks
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform _weaponPoint;

    [SerializeField] private float _comboWindow = 2f;
    [SerializeField] private int _maxComboHits = 2;

    private Weapon _currentWeapon;
    private int _currentComboIndex = 0;
    private float _nextAttackTime = 0f;
    private float _lastAttackTime = 0f;

    public event Action<int> Attacking;

    private void Update()
    {
        if (Time.time >= _nextAttackTime)
        {
            if (photonView.IsMine)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Attack();
                    ResetComboTimer();
                }
                else if (Time.time < _lastAttackTime + _comboWindow)
                {
                    ResetCombo();
                }
            }
        }
    }

    public void EquipItem(Item item)
    {
        if (_currentWeapon != null)
        {
            Destroy(_currentWeapon.gameObject);
        }

        if (item.Type == ItemType.Weapon)
        {
            Weapon weapon = item as Weapon;
            _currentWeapon = Instantiate(weapon, _weaponPoint);
        }
    }

    private void Attack()
    {
        if (_currentComboIndex == 0)
        {
            AttackNearbyEnemies();
            Attacking?.Invoke(1);
        }
        else
        {
            CircleAttack();
            Attacking?.Invoke(2);
        }

        _currentComboIndex++;

        if (_currentComboIndex >= _maxComboHits)
        {
            ResetCombo();
        }

        _nextAttackTime = Time.time + _currentWeapon.DelayBetweenAttack;
        _lastAttackTime = Time.time;
    }

    private void ResetCombo()
    {
        _currentComboIndex = 0;
    }

    private void ResetComboTimer()
    {
        _lastAttackTime = Time.time;
    }

    private void AttackNearbyEnemies()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, _currentWeapon.AttackRange, enemyLayer);

        foreach (Collider enemyCollider in enemiesInRange)
        {
            Enemy enemy = enemyCollider.GetComponent<Enemy>();

            if (enemy != null)
            {
                Vector3 directionToEnemy = enemyCollider.transform.position - transform.position;
                float angle = Vector3.Angle(transform.forward, directionToEnemy);

                if (angle <= _currentWeapon.AttackAngle)
                {
                    enemy.TakeDamage(_currentWeapon.Damage);
                }
            }
        }
    }

    private void CircleAttack()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, _currentWeapon.AttackRange, enemyLayer);

        foreach (Collider enemyCollider in enemiesInRange)
        {
            Enemy enemy = enemyCollider.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(_currentWeapon.Damage);
            }
        }
    }
}