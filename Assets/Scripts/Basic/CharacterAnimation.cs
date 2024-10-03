using UnityEngine;

[RequireComponent(typeof(Character))]
[RequireComponent(typeof(Animator))]
public abstract class CharacterAnimation : MonoBehaviour
{
    protected Animator Animator;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        OnAwake();
    }

    protected abstract void OnAwake();
}
