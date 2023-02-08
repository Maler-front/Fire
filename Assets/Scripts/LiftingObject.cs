using UnityEngine;

public class LiftingObject : InteractableObject
{
    [SerializeField]
    private PickUpPlace _pickUpPlace;
    [SerializeField]
    private Vector3 _inventoryPosition;
    [SerializeField]
    private Quaternion _inventoryRotation;

    public PickUpPlace Place
    {
        get { return _pickUpPlace; }
        set { }
    }

    public override void Interact(GameObject _player)
    {
        switch (_pickUpPlace) {
            case PickUpPlace.Face:
                {
                    _player.GetComponentInChildren<Inventory>().GrabItemOnHead(gameObject);
                    break;
                }
            case PickUpPlace.Hands:
                {
                    _player.GetComponentInChildren<Inventory>().GrabItemInHands(gameObject, _inventoryPosition, _inventoryRotation);
                    break;
                }
        }
    }

    public enum PickUpPlace
    {
        Face,
        Hands
    }
}
