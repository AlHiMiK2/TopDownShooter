using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public Sprite GetSprite()
    {
        return _renderer.sprite;
    }

    public void SetLayer(int layer)
    {
        _renderer.sortingOrder = layer;
    }

    public abstract void Use();
}
