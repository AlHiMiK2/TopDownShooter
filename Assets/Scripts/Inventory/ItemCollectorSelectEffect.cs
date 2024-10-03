using UnityEngine;

[RequireComponent(typeof(ItemCollector))]
public class ItemCollectorSelectEffect : MonoBehaviour
{
    [SerializeField] private Transform _effect;

    private ItemCollector _collector;

    private void Awake()
    {
        _collector = GetComponent<ItemCollector>();
    }

    private void OnEnable()
    {
        _collector.OnItemSelected += SetEffect;
    }

    private void OnDisable()
    {
        _collector.OnItemSelected -= SetEffect;
    }

    private void SetEffect(Item item)
    {
        if (item != null)
        {
            _effect.gameObject.SetActive(true);
            _effect.transform.position = item.transform.position;
        }
        else
        {
            _effect.gameObject.SetActive(false);
        }
    }
}
