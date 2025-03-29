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

    private Character _character;

    public float Speed => _navMesh.velocity.magnitude;

    private void Awake()
    {
        _scaning = GetComponent<EnemyScaning>();
        _navMesh = GetComponent<NavMeshAgent>();
        _attack = GetComponent<EnemyAttack>();

        _scaning.CharacterFinded += StartFollowing;
    }

    public void StopMoving()
    {
        _character = null;
        _scaning.CharacterFinded -= StartFollowing;
    }

    private void StartFollowing(Character character)
    {
        _character = character;

        StartCoroutine(FollowCharacter());
    }

    private IEnumerator FollowCharacter()
    {
        var delay = new WaitForSeconds(_delayBetweenFollowing);

        while (_character)
        {
            MoveToTarget(_character);

            yield return delay;
        }
    }

    private void MoveToTarget(Character target)
    {
        if (_attack.IsAttack)
        {
            _navMesh.speed = 0;
            return;
        }

        _navMesh.speed = _speed;
        _navMesh.SetDestination(target.transform.position);
    }
}