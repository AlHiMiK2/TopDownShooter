using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private float _itemCheckerRadius;

    private PlayerInput _input;
    private Item _selectedItem;
    private Player _player;

    public event UnityAction<Item> OnItemSelected; 

    [Inject]
    private void Construct(PlayerInput input, Player player)
    {
        _input = input;
        _player = player;
    }

    private void OnEnable()
    {
        _input.OnTakeButtonClick += TakeItem;
    }

    private void OnDisable()
    {
        _input.OnTakeButtonClick -= TakeItem;
    }

    private void TakeItem()
    {
        if (_selectedItem)
        {
            _inventory.TakeItem(_selectedItem);
        }
    }

    private void FixedUpdate()
    {
        CheckItems();
    }

    private void CheckItems()
    {
        RaycastHit2D[] castedItems = Physics2D.CircleCastAll(_player.transform.position, _itemCheckerRadius, Vector2.zero);

        if (castedItems != null)
        {
            foreach (var t in castedItems)
            {
                if (t.transform.TryGetComponent(out Item item))
                {
                    _selectedItem = item;
                    break;
                } 
                
                _selectedItem = null;
            }
        }
        
        OnItemSelected?.Invoke(_selectedItem);
    }
}
