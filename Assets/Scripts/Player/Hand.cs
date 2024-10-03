using UnityEngine;
using Zenject;

public class Hand : MonoBehaviour
{
    [SerializeField] private float _maxDistance;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Transform _itemOrigin;
    [SerializeField] private int _itemDefaultSortLayer;
    [SerializeField] private int _itemCustomSortLayer;

    private Camera _camera;
    private PlayerInput _input;
    private Item _currentItem;

    [Inject]
    private void Construct(PlayerInput input)
    {
        _input = input;
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Aim();
    }

    public void TakeItem(Item item)
    {
        if(_currentItem)
            _currentItem.gameObject.SetActive(false);

        if (item)
        {
            item.transform.parent = _itemOrigin;
            item.transform.SetPositionAndRotation(_itemOrigin.position, transform.rotation);
            item.SetLayer(_itemCustomSortLayer);
            item.gameObject.SetActive(true);
        }
        
        _currentItem = item;
    }

    public void RemoveItem()
    {
        if (!_currentItem) return;
        
        _currentItem.transform.parent = null;
        _currentItem.SetLayer(_itemDefaultSortLayer);
        _currentItem.gameObject.SetActive(true);
        _currentItem = null;
    }

    public void UseItem()
    {
        if (_currentItem)
        {
            _currentItem.Use();
        }
    }

    private void Aim()
    {
        Vector3 target = _camera.ScreenToWorldPoint(_input.GetMousePosition());
        Vector3 direction = target - transform.position;

        bool isFlip = direction.x < 0;

        transform.localPosition = Vector3.ClampMagnitude(direction, _maxDistance) + _offset;

        if(isFlip)
            transform.localRotation = Quaternion.Euler(180, 0, -Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        else
            transform.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }
}
