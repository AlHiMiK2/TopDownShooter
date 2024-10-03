using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private SpriteRenderer _renderer;

    private bool _isOpen;

    private void Update()
    {
        if (!_isOpen)
        {
            if (_spawner)
            {
                if (_spawner.IsEnd)
                {
                    _renderer.enabled = false;
                    _collider.enabled = false;

                    _audioSource.Play();

                    _isOpen = true;
                }
            }
        }
    }
}
