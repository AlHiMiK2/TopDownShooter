using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private PistolBullet _bulletPrefab;
    [SerializeField] private int _capacity;
    
    private List<PistolBullet> _pool = new List<PistolBullet>();

    public int Capacity => _capacity;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < _capacity; i++)
        {
            PistolBullet spawned = Instantiate(_bulletPrefab, _container);
            spawned.gameObject.SetActive(false);

            _pool.Add(spawned);
        }
    }

    public bool TryGetObject(out PistolBullet result)
    {
        result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return result != null;
    }

    public void ResetPool()
    {
        foreach (var item in _pool)
        {
            item.gameObject.SetActive(false);
        }
    }
}
