using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyAttack))]
public class EnemyAnimation : MonoBehaviour
{
    public class Params
    {
        public static readonly int IsRunning = Animator.StringToHash(nameof(IsRunning));
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

        _movement.Running += SetupAnimationRun;
        _attack.Attacking += SetupAnimationAttack;
        _enemy.Died += SetupAnimationDie;
        _enemy.HealthChanged += SetupAnimationTakeDamage;
    }

    private void OnDisable()
    {
        _movement.Running -= SetupAnimationRun;
        _attack.Attacking -= SetupAnimationAttack;
        _enemy.Died -= SetupAnimationDie;
        _enemy.HealthChanged -= SetupAnimationTakeDamage;
    }

    private void SetupAnimationRun(bool isRunning)
    {
        _animator.SetBool(Params.IsRunning, isRunning);
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