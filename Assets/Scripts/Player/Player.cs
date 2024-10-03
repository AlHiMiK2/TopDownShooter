using UnityEngine;
using Zenject;

public class Player : Character
{
    [SerializeField] private Hand _hand;

    private PlayerInput _input;
    private Inventory _inventory;
    private Vector2 _spawnPosition;
    
    [Inject]
    private void Construct(PlayerInput input, Inventory inventory)
    {
        _input = input;
        _inventory = inventory;
    }
    
    private void Start()
    {
        CurrentHealth = MaxHealth;
        _spawnPosition = transform.position;
    }

    private void OnEnable()
    {
        _inventory.OnTakeItem += TakeItem;
        _inventory.OnRemoveItem += RemoveItem;
    }

    private void OnDisable()
    {
        _inventory.OnTakeItem -= TakeItem;
        _inventory.OnRemoveItem -= RemoveItem;
    }

    private void Update()
    {
        Move(_input.GetMoveDirection());

        if (_input.GetUseButtonClick())
            UseHand();
    }

    private void UseHand()
    {
        _hand.UseItem();
    }

    private void TakeItem(Item item)
    {
        _hand.TakeItem(item);
    }

    private void RemoveItem()
    {
        _hand.RemoveItem();
    }

    protected override void OnDie()
    {
        transform.position = _spawnPosition;
        Heal();
    }
}
