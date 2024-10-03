using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SphereSimulationPro : Enemy
{
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _shootDistance;
    [SerializeField] private float _cooldownBetweenShoot;
    [SerializeField] private int _shootCount;
    [SerializeField] private Transform _shootOrigin;
    [SerializeField] private int _bulletDamage;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletLifeTime;
    [SerializeField] private PistolBullet _bulletPrefab;
    [SerializeField] private float _viewDistance;

    private float _elapsedTimeAfterAttack;
    private int _currentShootCount;
    
    public override event UnityAction<Vector2> OnMoved;
    public override event UnityAction OnAttack;
    
    protected override void OnSpawned()
    {
        _elapsedTimeAfterAttack = _attackCooldown;
    }

    private void Update()
    {
        if(IsDie) return;
        
        var distanceToPlayer = Vector2.Distance(Player.transform.position, transform.position);
        
        _elapsedTimeAfterAttack += Time.deltaTime;

        if (!(distanceToPlayer < _viewDistance)) return;
        Agent.speed = _attackSpeed;
        Agent.SetDestination(Player.transform.position);
        OnMoved?.Invoke(Agent.velocity);

        if(_elapsedTimeAfterAttack < _attackCooldown) return;
        if (!(distanceToPlayer < _shootDistance)) return;

        StartCoroutine(ShootCoroutine());
        OnAttack?.Invoke();
        _elapsedTimeAfterAttack = 0;
    }

    private IEnumerator ShootCoroutine()
    {
        if (_currentShootCount < _shootCount)
        {
            var bulletDirection = Player.transform.position - _shootOrigin.position;
            var bulletRotation = Quaternion.Euler(0, 0, Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg);
            
            Instantiate(_bulletPrefab, _shootOrigin.position, bulletRotation)
                .Init(_bulletSpeed, _bulletDamage, _bulletLifeTime);
            _currentShootCount++;
            yield return new WaitForSeconds(_cooldownBetweenShoot);
        }
        else
        {
            _currentShootCount = 0;
            yield break;
        }
    }
}
