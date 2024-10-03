using UnityEngine;
using UnityEngine.Events;

public class Slime : Enemy
{
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _viewDistance;

    private float _elapsedTimeAfterAttack;
    private Vector3 _lastPlayerPosition;
    
    public override event UnityAction<Vector2> OnMoved;
    public override event UnityAction OnAttack;
    
    protected override void OnSpawned()
    {
        _elapsedTimeAfterAttack = _attackCooldown;
        _lastPlayerPosition = transform.position;
    }

    private void Update()
    {
        if(IsDie) return;
        
        var distanceToPlayer = Vector2.Distance(Player.transform.position, transform.position);
        
        _elapsedTimeAfterAttack += Time.deltaTime;

        if (distanceToPlayer < _viewDistance)
        {
            Agent.speed = _attackSpeed;
            Agent.SetDestination(Player.transform.position);
            OnMoved?.Invoke(Agent.velocity);
            _lastPlayerPosition = Player.transform.position;
        }
        else
        {
            Agent.speed = _attackSpeed;
            Agent.SetDestination(_lastPlayerPosition);
            OnMoved?.Invoke(Agent.velocity);
            return;
        }
        
        if(_elapsedTimeAfterAttack < _attackCooldown) return;
        if (!(distanceToPlayer < _attackDistance)) return;
        
        Player.Damage(_damage);
        OnAttack?.Invoke();
        _elapsedTimeAfterAttack = 0;
    }
}
