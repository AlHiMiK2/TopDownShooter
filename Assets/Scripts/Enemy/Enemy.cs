using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : Character
{
    [SerializeField] private Collider2D _collider;
    
    protected NavMeshAgent Agent;
    
    protected Player Player;
    protected bool IsDie;
    
    public abstract event UnityAction OnAttack;
    public event UnityAction OnDied;
    
    [Inject] 
    private void Construct(Player player) 
    {
        Player = player;
    }

    private void OnEnable()
    {
        _collider.enabled = true;
        IsDie = false;
        OnSpawned();
    }

    protected abstract void OnSpawned();
    
    private void Start()
    {
        CurrentHealth = MaxHealth;
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
    }

    protected override void OnDie()
    {
        _collider.enabled = false;
        IsDie = true;
        OnDied?.Invoke();
    }

    public void Die()
    {
        Heal();
        gameObject.SetActive(false);
    }

    public class Factory : PlaceholderFactory<UnityEngine.Object, Enemy>
    {

    }
}
