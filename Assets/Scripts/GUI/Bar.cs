using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] private Image _bar;
    [SerializeField, Range(0, 100)] private float _smoothness;

    private float _targetValue = 1;

    private void Update()
    {
        _bar.fillAmount = Mathf.Lerp(_bar.fillAmount, _targetValue, _smoothness * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void SetValue(float value)
    {
        _targetValue = value;
    }
}
