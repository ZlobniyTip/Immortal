using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyAttack))]
public class EnemyAnimation : MonoBehaviour
{
    public class Params
    {
        public static readonly int Speed = Animator.StringToHash(nameof(Speed));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
        public static readonly int TakeDamage = Animator.StringToHash(nameof(TakeDamage));
        public static readonly int Die = Animator.StringToHash(nameof(Die));
    }

    private Animator _animator;
    private EnemyMovement _movement;
    private EnemyAttack _attack;
    private Enemy _enemy;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<EnemyMovement>();
        _attack = GetComponent<EnemyAttack>();
        _enemy = GetComponent<Enemy>();

        _attack.Attacking += SetupAnimationAttack;
        _enemy.Died += SetupAnimationDie;
        _enemy.HealthChanged += SetupAnimationTakeDamage;
    }

    private void OnDisable()
    {
        _attack.Attacking -= SetupAnimationAttack;
        _enemy.Died -= SetupAnimationDie;
        _enemy.HealthChanged -= SetupAnimationTakeDamage;
    }

    private void Update()
    {
        SetupAnimationRun(_movement.Speed);
    }

    private void SetupAnimationRun(float speed)
    {
        _animator.SetFloat(Params.Speed, Mathf.Abs(speed));
    }

    private void SetupAnimationAttack()
    {
        _animator.SetTrigger(Params.Attack);
    }

    private void SetupAnimationDie()
    {
        _animator.SetTrigger(Params.Die);
    }

    private void SetupAnimationTakeDamage(int health, int maxHealth)
    {
        _animator.SetTrigger(Params.TakeDamage);
    }
}