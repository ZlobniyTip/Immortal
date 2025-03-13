using System;
using System.Collections;
using UnityEngine;

public class EnemyScaning : MonoBehaviour
{
    [SerializeField] private float _radius;

    private Coroutine _search;
    private float _delayBetweenFind = 1;

    public event Action<Character> CharacterFinded;

    public Character Target { get; private set; }

    private void Start()
    {
        _search = StartCoroutine(SearchPlayer());
    }

    public void StopSearchPlayer()
    {
        Target = null;
        StopCoroutine(_search);
    }

    private IEnumerator SearchPlayer()
    {
        var delayFind = new WaitForSeconds(_delayBetweenFind);

        while (Target == null)
        {
            Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);
            Rigidbody rigidbody;

            for (int i = 0; i < overlappedColliders.Length; i++)
            {
                rigidbody = overlappedColliders[i].attachedRigidbody;

                if (rigidbody)
                {
                    if (rigidbody.gameObject.TryGetComponent(out Character enemy))
                    {
                        Target = enemy;
                        CharacterFinded?.Invoke(enemy);
                    }
                }
            }

            yield return delayFind;
        }
    }
}