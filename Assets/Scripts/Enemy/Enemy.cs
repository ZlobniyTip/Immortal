using UnityEngine;

public class Enemy : Health
{
    private EnemyAnimation _animation;
    private EnemyScaning _scaning;
    
    private void Awake()
    {
        _animation = GetComponent<EnemyAnimation>();
        _scaning = GetComponent<EnemyScaning>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (_maxValue <= 0)
        {
            _animation.enabled = false;
            _scaning.StopSearchPlayer();
        }
    }
}