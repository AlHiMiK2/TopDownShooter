using System;
using UnityEngine;

[Serializable]
public class Slot
{
    private Item _currentItem;

    private SlotView _slotView;

    public void Init(SlotView slotView)
    {
        _slotView = slotView;
    }

    public void Select()
    {
        _slotView.EnableFrame();
    }

    public void Deselect()
    {
        _slotView.DisableFrame();
    }

    public void SetItem(Item item)
    {
        RemoveItem();
        
        Select();
        
        _currentItem = item;
        
        _slotView.ChangeImage(_currentItem.GetSprite(), Color.white);
    }
    
    public void RemoveItem()
    {
        _slotView.ChangeImage(null, Color.clear);
        _currentItem = null;
    }
    
    public Item GetItem()
    {
        return _currentItem;
    }
}
