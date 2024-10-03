using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private float _animationDuration;
    [SerializeField] private float _raycastLenght;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Animator _animator;

    private float _speed;
    private int _damage;

    private readonly int _isHitParameter = Animator.StringToHash("IsHit");
    
    protected bool IsHit = false;
    protected float LifeTime;
    protected float ElapsedLifeTime;
    
    public void Init(float speed, int damage, float lifeTime)
    {
        _speed = speed;
        _damage = damage;
        LifeTime = lifeTime;
    }

    protected void Move()
    {
        transform.Translate(_speed * Time.deltaTime * transform.right, Space.World);
    }

    protected void Damage(RaycastHit2D hit)
    {
        if (hit.transform.TryGetComponent(out IDamageable damageable))
            damageable.Damage(_damage);
        else
            DieWithAnimation();
    }
    
    protected bool TryGetHit(out RaycastHit2D hit)
    {
        var selfTransform = transform;
        hit = Physics2D.Raycast(selfTransform.position, -selfTransform.right, _raycastLenght, _layerMask);

        return hit.collider;
    }

    protected void DieWithAnimation()
    {
        _animator.SetTrigger(_isHitParameter);
        Destroy(gameObject ,_animationDuration);
    }
}
