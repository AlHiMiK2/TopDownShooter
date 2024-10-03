using UnityEngine;
using Zenject;

public class CameraFollow : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float _smoothness;
    [SerializeField] private Vector3 _offset;

    private Transform _target;

    [Inject]
    private void Construct(Player player)
    {
        _target = player.transform;
    }

    private void FixedUpdate()
    {
        if (_target)
        {
            var targetPosition = Vector3.Lerp(transform.position, _target.position, _smoothness * Time.fixedDeltaTime) + _offset;
            targetPosition.z = transform.position.z;
            transform.position = targetPosition;
        }
    }
}
