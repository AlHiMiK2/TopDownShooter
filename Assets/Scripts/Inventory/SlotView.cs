using UnityEngine;
using UnityEngine.UI;

public class SlotView : MonoBehaviour
{
    [SerializeField] private Image _slotImage;
    [SerializeField] private Image _slotFrame;
    [SerializeField] private Sprite _slotFrameDefault;
    [SerializeField] private Sprite _slotFrameSelected;

    public void ChangeImage(Sprite sprite, Color color)
    {
        _slotImage.sprite = sprite;
        _slotImage.color = color;
    }

    public void EnableFrame()
    {
        _slotFrame.sprite = _slotFrameSelected;
    }
    
    public void DisableFrame()
    {
        _slotFrame.sprite = _slotFrameDefault;
    }
}
