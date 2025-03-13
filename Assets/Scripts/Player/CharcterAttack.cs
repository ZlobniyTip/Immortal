using UnityEngine;

public class CharcterAttack : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform _weaponPoint;

    private Weapon _currentWeapon;
    private float nextAttackTime = 0f;

    private void Start()
    {
        _currentWeapon = Instantiate(_weapon, _weaponPoint);
    }

    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                AttackNearbyEnemies();
                nextAttackTime = Time.time + _currentWeapon.AttackRate;
            }
        }
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
}