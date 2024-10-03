using System;
using UnityEngine;

public class BasicEnemyAnimation : CharacterAnimation
{
    private Enemy _enemy;
    private readonly int _speedParameter = Animator.StringToHash("Speed");
    private readonly int _attackParameter = Animator.StringToHash("Attack");
    private readonly int _dieParameter = Animator.StringToHash("Die");

    protected override void OnAwake()
    {
        _enemy = GetComponent<Enemy>();
        Animator.keepAnimatorStateOnDisable = true;
    }

    private void OnEnable()
    {
        _enemy.OnMoved += MoveAnimation;
        _enemy.OnAttack += AttackAnimation;
        _enemy.OnDied += DieAnimation;
    }

    private void OnDisable()
    {
        _enemy.OnMoved -= MoveAnimation;
        _enemy.OnAttack -= AttackAnimation;
        _enemy.OnDied -= DieAnimation;
    }

    private void AttackAnimation()
    {
        Animator.SetTrigger(_attackParameter);
    }

    private void MoveAnimation(Vector2 moveVector)
    {
        var direction = moveVector.x < 0;
        transform.rotation = Quaternion.Euler(0, 180 * Convert.ToInt32(direction), 0);
        Animator.SetFloat(_speedParameter, moveVector.magnitude);
    }

    private void DieAnimation()
    {
        Animator.SetTrigger(_dieParameter);
    }
}
