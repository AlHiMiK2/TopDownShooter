using UnityEngine;
using Zenject;

public class Eyes : MonoBehaviour
{
    [SerializeField] private float _maxDistance;

    private Camera _camera;
    private PlayerInput _input;

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
        Look();
    }

    private void Look()
    {
        Vector3 target = _camera.ScreenToWorldPoint(_input.GetMousePosition());
        transform.localPosition = Vector3.ClampMagnitude(target - transform.position, _maxDistance);
    }
}
