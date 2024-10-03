using UnityEngine;
using Zenject;

public class GUI : MonoBehaviour
{
    [SerializeField] private SlotView _slotViewPrefab;
    [SerializeField] private Transform _slotViewContainer;
    [SerializeField] private Bar _healthBar;
    [SerializeField] private Bar _bulletBar;

    private Player _player;

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }

    private void OnEnable()
    {
        _player.OnHealthChange += SetHealthBarValue;
    }

    private void OnDisable()
    {
        _player.OnHealthChange -= SetHealthBarValue;
    }

    public SlotView CreateSlotView()
    {
        return Instantiate(_slotViewPrefab, _slotViewContainer);
    }

    private void SetHealthBarValue(int value)
    {
        var fraction = (float)value / (float)_player.MaxHealth;

        _healthBar.SetValue(fraction);
    }

    public void SetBulletBarValue(float value)
    {
        _bulletBar.SetValue(value);
    }
}
