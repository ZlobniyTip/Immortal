using System.Collections;
using UnityEngine;

public class Enemy : Health
{
    [SerializeField] private DropLoot _dropLoot;
    [SerializeField] private string _name;

    private EnemyAnimation _animation;
    private EnemyScaning _scaning;
    private EnemyAttack _attack;
    private EnemyMovement _enemyMovement;
    private Rigidbody _rigidbody;

    public string Name => _name;
    
    private void Awake()
    {
        _attack = GetComponent<EnemyAttack>();
        _animation = GetComponent<EnemyAnimation>();
        _scaning = GetComponent<EnemyScaning>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (CurrentHealth <= 0)
        {
            StartCoroutine(Dead());

            _animation.enabled = false;
            _attack.enabled = false;
            _scaning.StopSearchPlayer();
            _enemyMovement.StopMoving();
            _rigidbody.detectCollisions = false;

            _dropLoot.DropItems();
        }
    }

    protected override IEnumerator Dead()
    {
        var delay = new WaitForSeconds(2);

        yield return delay;

        Destroy(gameObject);
    }
}