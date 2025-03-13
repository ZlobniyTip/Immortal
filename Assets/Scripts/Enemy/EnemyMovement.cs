using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyScaning))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyAttack))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _delayBetweenFollowing = 2;
    [SerializeField] private float _speed = 3;

    private EnemyScaning _scaning;
    private EnemyAttack _attack;
    private NavMeshAgent _navMesh;

    public event Action<bool> Running;

    public bool IsRunning { get; private set; } = false;

    private void Awake()
    {
        _scaning = GetComponent<EnemyScaning>();
        _navMesh = GetComponent<NavMeshAgent>();
        _attack = GetComponent<EnemyAttack>();

        _scaning.CharacterFinded += StartFollowing;
    }

    private void StartFollowing(Character character)
    {
        StartCoroutine(FollowCharacter(character));
    }

    private IEnumerator FollowCharacter(Character character)
    {
        var delay = new WaitForSeconds(_delayBetweenFollowing);

        while (character)
        {
            MoveToTarget(character);

            yield return delay;
        }
    }

    private void MoveToTarget(Character target)
    {
        if (_attack.IsAttack)
        {
            _navMesh.speed = 0;
            IsRunning = false;
            Running?.Invoke(IsRunning);
            return;
        }

        _navMesh.speed = _speed;
        _navMesh.SetDestination(target.transform.position);

        if (IsRunning == false)
        {
            IsRunning = true;
            Running?.Invoke(IsRunning);
        }
    }
}