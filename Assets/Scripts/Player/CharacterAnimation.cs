using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ComboAttack))]
[RequireComponent(typeof(CharacterMovement))]
public class AnimPlayer : MonoBehaviour
{
    private CharacterMovement _movement;
    private Animator _animator;
    private ComboAttack _attack;

    public class Params
    {
        public static readonly int Speed = Animator.StringToHash(nameof(Speed));
        public static readonly int Attack1 = Animator.StringToHash(nameof(Attack1));
        public static readonly int Attack2 = Animator.StringToHash(nameof(Attack2));
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _attack = GetComponent<ComboAttack>();
        _movement = GetComponent<CharacterMovement>();

        _attack.Attacking += SetupAnimationAttack;
    }

    private void OnDestroy()
    {
        _attack.Attacking -= SetupAnimationAttack;
    }

    private void Update()
    {
        if (_movement.IsRunning == false)
            return;

        SetupAnimationRun(_movement.CurrentSpeed);
    }

    private void SetupAnimationRun(float speed)
    {
        _animator.SetFloat(Params.Speed, Mathf.Abs(speed));
    }

    private void SetupAnimationAttack(int attackIndex)
    {
        if (attackIndex == 1)
        {
            _animator.SetTrigger(Params.Attack1);
        }
        else if (attackIndex == 2)
        {
            _animator.SetTrigger(Params.Attack2);
        }
    }
}