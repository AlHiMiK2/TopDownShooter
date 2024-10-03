using UnityEngine;
using Zenject;

public class EnemySpawner : DefaultObjectPool
{
    [SerializeField] private GameObject _enemyPrefab;
    [Header("Main Settings")]
    [SerializeField] private float _cooldown;
    [SerializeField] private bool _isInfinity;
    [SerializeField] private int _spawnCount;

    private Enemy.Factory _factory;

    private float _timeAfterSpawn;
    private int _spawnedCount;

    private bool _isSpawning;

    public bool IsEnd { get; private set; }

    [Inject]
    public void Construct(Enemy.Factory factory)
    {
        _factory = factory;
    }

    private void Start()
    {
        for (int i = 0; i < Capacity; i++)
        {
            AddObject(_factory.Create(_enemyPrefab).gameObject);
        }
    }

    private void Update()
    {
        _timeAfterSpawn += Time.deltaTime;


        if (_isSpawning)
        {
            if (_cooldown < _timeAfterSpawn)
            {
                if (_spawnedCount < _spawnCount)
                {
                    Spawn();
                }
                else if (_isInfinity)
                {
                    Spawn();
                }
            }
        }

        if (_spawnedCount >= _spawnCount & !_isInfinity)
        {
            IsEnd = GetObjects().Find(x => x.activeSelf) == null;
        }
    }

    private void Spawn()
    {
        if (!TryGetObject(out GameObject enemy)) return;
        enemy.transform.position = transform.position + Random.insideUnitSphere;
        enemy.SetActive(true);
        _spawnedCount++;
        _timeAfterSpawn = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            _isSpawning = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _isSpawning = false;
        }
    }

    private void OnDisable()
    {
        IsEnd = true;
    }
}
