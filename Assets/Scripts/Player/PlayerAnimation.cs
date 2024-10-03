using UnityEngine;

public class PlayerAnimation : CharacterAnimation
{
    private SpriteRenderer _renderer;
    private Player _player;
    private readonly int _speedParameter = Animator.StringToHash("Speed");

    protected override void OnAwake()
    {
        _player = GetComponent<Player>();
        _renderer = GetComponent<SpriteRenderer>();
    }
    
    private void OnEnable()
    {
        _player.OnMoved += MoveAnimation;
    }

    private void OnDisable()
    {
        _player.OnMoved -= MoveAnimation;
    }

    private void MoveAnimation(Vector2 moveVector)
    {
        _renderer.flipX = moveVector.x < 0;
        Animator.SetFloat(_speedParameter, moveVector.magnitude);
    }
}
