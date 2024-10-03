using UnityEngine;
using UnityEngine.Events;

public abstract class Character : MonoBehaviour, IDamageable
{
    [SerializeField] private int _health;
    [SerializeField] private float _speed;

    protected int CurrentHealth;
    protected Rigidbody2D Rigidbody;

    public int MaxHealth => _health;
    public float Speed => _speed;

    public event UnityAction<int> OnHealthChange;
    public virtual event UnityAction<Vector2> OnMoved;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        CurrentHealth = _health;
    }

    protected void Move(Vector2 direction)
    {
        var moveVector = direction * Speed;

        Rigidbody.velocity = moveVector;

        OnMoved?.Invoke(moveVector);
    }
  
    public void Damage(int damage)
    {
        CurrentHealth -= damage;

        OnHealthChange?.Invoke(CurrentHealth);

        if (CurrentHealth <= 0)
        {
            OnDie();
        }
    }

    protected void Heal()
    {
        CurrentHealth = _health;
        
        OnHealthChange?.Invoke(CurrentHealth);
    }

    protected abstract void OnDie();
}
