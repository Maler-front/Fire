using UnityEngine;

public class InventoryItem
{
    private GameObject _item;
    private UsibleObject _usibleObject;
    private Vector3 _positionInSlot;
    private Quaternion _rotationInSlot;

    public InventoryItem(GameObject item, Vector3 positionInSlot, Quaternion rotationInSlot)
    {
        if (_item == null)
        {
            _item = item;
            _usibleObject = _item.GetComponent<UsibleObject>();
            _positionInSlot = positionInSlot;
            _rotationInSlot = rotationInSlot;
        }
    }

    public GameObject Item
    {
        get { return _item; }
        set { }
    }

    public UsibleObject UsibleObject
    {
        get { return _usibleObject; }
        set { }
    }

    public Vector3 PositionInSlot
    {
        get { return _positionInSlot; }
        set { }
    }

    public Quaternion RotationInSlot
    {
        get { return _rotationInSlot; }
        set { }
    }
}
