using UnityEngine;

public class EnemyGUI : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Bar _healthBar;

    private void OnEnable()
    {
        _enemy.OnHealthChange += SetHealthBarValue;
    }

    private void OnDestroy()
    {
        _enemy.OnHealthChange -= SetHealthBarValue;
    }

    private void SetHealthBarValue(int value)
    {
        var fraction = (float)value / (float)_enemy.MaxHealth;

        _healthBar.SetValue(fraction);
    }
}
