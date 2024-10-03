using UnityEngine;
using UnityEngine.Events;

public class Ghost : Enemy
{
    [SerializeField] private float _escapeSpeed;
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

        if (_elapsedTimeAfterAttack > _attackCooldown)
        {
            if (distanceToPlayer < _viewDistance)
            {
                Agent.speed = Speed;
                Agent.SetDestination(Player.transform.position);
                OnMoved?.Invoke(Agent.velocity);
                _lastPlayerPosition = Player.transform.position;
                
                if (!(distanceToPlayer < _attackDistance)) return;
                Player.Damage(_damage);
                _elapsedTimeAfterAttack = 0;
                OnAttack?.Invoke();
            }
            else
            {
                Agent.speed = Speed;
                Agent.SetDestination(_lastPlayerPosition);
                OnMoved?.Invoke(Agent.velocity);
            }
        }

        Agent.speed = _escapeSpeed;
        Agent.SetDestination(transform.position + (transform.position - Player.transform.position) * 5);
        OnMoved?.Invoke(Agent.velocity);
    }
}
