using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _slotCount;
    [SerializeField] private GUI _gui;

    private Slot _selectedSlot;
    private List<Slot> _slots;
    private PlayerInput _input;

    public event UnityAction<Item> OnTakeItem; 
    public event UnityAction OnRemoveItem; 

    [Inject]
    private void Construct(PlayerInput input)
    {
        _input = input;
    }

    private void OnEnable()
    {
        _input.OnDropButtonClick += RemoveItem;
    }

    private void OnDisable()
    {
        _input.OnDropButtonClick -= RemoveItem;
    }

    private void Awake()
    {
        CreateSlots();
    }

    private void CreateSlots()
    {
        _slots = new List<Slot>();

        for (var i = 0; i < _slotCount; i++)
        {
            var createdSlot = new Slot();
            var createdSlotView = _gui.CreateSlotView();
            createdSlot.Init(createdSlotView);

            _slots.Add(createdSlot);
        }
    }

    public void TakeItem(Item item)
    {
        _selectedSlot.SetItem(item);
        
        if(_selectedSlot.GetItem())
            OnRemoveItem?.Invoke();
        
        OnTakeItem?.Invoke(item);
    }

    private void RemoveItem()
    {
        _selectedSlot.RemoveItem();
        OnRemoveItem?.Invoke();
    }

    public void SelectSlot(int index)
    {
        _selectedSlot?.Deselect();
        _selectedSlot = _slots[index];
        _selectedSlot.Select();
        OnTakeItem?.Invoke(_selectedSlot.GetItem());
    }
}
