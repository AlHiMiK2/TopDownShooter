using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class Gun : Item
{
    [Header("Main")]
    [SerializeField] private float _cooldown;
    [SerializeField] private int _bulletCount;
    [SerializeField] private float _reloadDuration;
    [SerializeField] private Transform _shootOrigin;
    [SerializeField] private AudioSource _audioSource;
    [Header("Recoil")]
    [SerializeField] private Vector3 _recoilMove;
    [SerializeField] private Vector3 _recoilRotation;
    [SerializeField] private float _recoilDuration;
    [Header("Bullet")]
    [SerializeField] private Bullet _prefab;
    [SerializeField] private int _minDamage;
    [SerializeField] private int _maxDamage;
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;

    private GUI _gui;

    private float _timeAfterShoot = 0;
    private int _currentBulletCount;
    private bool _isReloading; 

    [Inject]
    private void Construct(GUI gui)
    {
        _gui = gui;
    }

    private void OnEnable()
    {
        _gui.SetBulletBarValue(Convert.ToSingle(_currentBulletCount) / Convert.ToSingle(_bulletCount));
        Reload();
    }

    private void Start()
    {
        _currentBulletCount = _bulletCount;
    }

    public override void Use()
    {
        Shoot();
    }

    private void Update()
    {
        _timeAfterShoot += Time.deltaTime;
    }

    private void Shoot()
    {
        if(_currentBulletCount > 0 && !_isReloading)
        {
            if (!(_timeAfterShoot >= _cooldown)) return;
            
            var bullet = Instantiate(_prefab, _shootOrigin.position, _shootOrigin.rotation);

            InitBullet(bullet);
            Recoil();
            _audioSource.PlayOneShot(_audioSource.clip);
            _currentBulletCount--;
            _gui.SetBulletBarValue(Convert.ToSingle(_currentBulletCount) / Convert.ToSingle(_bulletCount));

            _timeAfterShoot = 0;
        }
        else if(!_isReloading)
        {
            Reload();
        }
    }

    private void Reload()
    {
        StartCoroutine(ReloadOneBullet());
    }

    private IEnumerator ReloadOneBullet()
    {
        _isReloading = true;

        while (true)
        {
            if (_currentBulletCount >= _bulletCount)
            {
                _gui.SetBulletBarValue((Convert.ToSingle(_currentBulletCount) / Convert.ToSingle(_bulletCount)) + 1);
                _isReloading = false;
                yield break;
            }

            _gui.SetBulletBarValue(Convert.ToSingle(_currentBulletCount) / Convert.ToSingle(_bulletCount));
            _currentBulletCount++;

            yield return new WaitForSeconds(_reloadDuration);
        }
    }

    private void InitBullet(Bullet bullet)
    {
        bullet.Init(_speed, UnityEngine.Random.Range(_minDamage, _maxDamage), _lifeTime);
        bullet.transform.SetPositionAndRotation(_shootOrigin.position, transform.rotation);
        bullet.gameObject.SetActive(true);
    }

    private void Recoil()
    {
        var parent = transform.parent;
        parent.DOLocalMove(_recoilMove, _recoilDuration / 2).SetLoops(2, LoopType.Yoyo);
        parent.DOLocalRotate(_recoilRotation, _recoilDuration / 2).SetLoops(2, LoopType.Yoyo);
    }
}
