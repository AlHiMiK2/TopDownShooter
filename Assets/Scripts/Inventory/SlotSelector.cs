using UnityEngine;
using Zenject;

public class SlotSelector : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    private PlayerInput _input;

    [Inject]
    private void Construct(PlayerInput input)
    {
        _input = input;
    }

    private void Start()
    {
        SelectSlot(1);
    }

    private void OnEnable()
    {
        _input.OnHotbarButtonClick += SelectSlot;
    }

    private void OnDisable()
    {
        _input.OnHotbarButtonClick -= SelectSlot;
    }

    private void SelectSlot(int index)
    {
        _inventory.SelectSlot(index - 1);
    }
}
