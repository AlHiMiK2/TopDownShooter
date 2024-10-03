using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    private Input _input;

    public event UnityAction<int> OnHotbarButtonClick;
    public event UnityAction OnTakeButtonClick;
    public event UnityAction OnDropButtonClick;

    private void Awake()
    {
        _input = new Input();

        _input.Player.Hotbar1.performed += context => OnHotbarButtonClick?.Invoke(1);
        _input.Player.Hotbar2.performed += context => OnHotbarButtonClick?.Invoke(2);
        _input.Player.Hotbar3.performed += context => OnHotbarButtonClick?.Invoke(3);
        _input.Player.Hotbar4.performed += context => OnHotbarButtonClick?.Invoke(4);
        _input.Player.Hotbar5.performed += context => OnHotbarButtonClick?.Invoke(5);
        _input.Player.TakeItem.performed += context => OnTakeButtonClick?.Invoke();
        _input.Player.DropItem.performed += context => OnDropButtonClick?.Invoke();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    public Vector2 GetMoveDirection()
    {
        return _input.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetMousePosition()
    {
        return _input.Player.Mouse.ReadValue<Vector2>();
    }

    public bool GetUseButtonClick()
    {
        return _input.Player.Use.IsPressed();
    }
}
