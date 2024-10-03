using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DefaultObjectPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private int _capacity;
    
    private List<GameObject> _pool = new List<GameObject>();

    public int Capacity => _capacity;

    public void Initialize(GameObject prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawned = Instantiate(prefab, _container);
            spawned.SetActive(false);

            _pool.Add(spawned);
        }
    }

    public void AddObject(GameObject gameObject)
    {
        _pool.Add(gameObject);
        gameObject.transform.parent = _container;
        gameObject.SetActive(false);
    }

    public bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);

        return result != null;
    }

    public void ResetPool()
    {
        foreach (var item in _pool)
        {
            item.SetActive(false);
        }
    }

    public List<GameObject> GetObjects()
    {
        return _pool;
    }
}
