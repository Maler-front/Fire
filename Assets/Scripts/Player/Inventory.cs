using System.Collections;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private PlayerInput _player;

    [SerializeField]
    private InventoryItem _leftHand;
    [SerializeField]
    private InventoryItem _rightHand;
    [SerializeField]
    private InventoryItem _head;

    [SerializeField]
    private BlackoutScript _blackout;
    [SerializeField]
    private Transform _leftHandTransform;
    [SerializeField]
    private Transform _rightHandTransform;
    [SerializeField]
    private float _moovingSpeed;
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private float _minDistance;
    [SerializeField]
    private float _minAngle;
    [SerializeField]
    private float _throwStrength;

    private bool HandsIsFull()
    {
        return _leftHand != null && _rightHand != null;
    }

    private void Start()
    {
        _leftHand = null;
        _rightHand = null;
        _head = null;
    }

    void Update()
    {
        if (_rightHand == null & _leftHand != null)
        {
            _rightHand = _leftHand;
            _leftHand = null;
            _rightHand.Item.transform.parent = _rightHandTransform;
            StartCoroutine(MoveItemToSlot(_rightHand, 1f));
        }
    }

    //-----------------------------------------------------
    private IEnumerator MoveItemToSlot(InventoryItem inventoryItem, float seconds, bool needToBlackout = false, bool needToFreezePlayer = false)
    {
        if(needToFreezePlayer)
            _player.enabled = false;

        if (needToBlackout)
            _blackout.On(seconds / 2);

        float step = 1f / (60f * seconds / 3 * 2);
        float distance = Vector3.Distance(inventoryItem.Item.transform.localPosition, inventoryItem.PositionInSlot);
        float angle = Quaternion.Angle(inventoryItem.Item.transform.localRotation, inventoryItem.RotationInSlot);

        while (distance > _minDistance && angle > _minAngle)
        {
            yield return new WaitForEndOfFrame();
            if (distance > _minDistance)
                inventoryItem.Item.transform.localPosition = Vector3.MoveTowards(
                    inventoryItem.Item.transform.localPosition, 
                    inventoryItem.PositionInSlot, 
                    step * distance);
            if (angle > _minAngle)
                inventoryItem.Item.transform.localRotation = Quaternion.RotateTowards(
                    inventoryItem.Item.transform.localRotation, 
                    inventoryItem.RotationInSlot, 
                    step * angle);

            distance = Vector3.Distance(inventoryItem.Item.transform.localPosition, inventoryItem.PositionInSlot);
            angle = Quaternion.Angle(inventoryItem.Item.transform.localRotation, inventoryItem.RotationInSlot);
        }

        if (_head != null) _head.Item.SetActive(false);

        if(needToFreezePlayer)
            _player.enabled = true;

        if(needToBlackout)
            _blackout.Off(seconds / 2);
    }

    //-----------------------------------------------------
    public void GrabItemInHands(GameObject item, Vector3 localPosition, Quaternion localRotation)
    {
        if (!HandsIsFull())
        {
            _leftHand = new InventoryItem(item, localPosition, localRotation);
            PrepareItem(item);
            StartCoroutine(MoveItemToSlot(_leftHand, 2f, false, true));
        }
        else Debug.Log("You can grab only 2 items!");
    }

    public void GrabItemOnHead(GameObject item)
    {
        if (_head == null)
        {
            _head = new InventoryItem(item, new Vector3(0f, 0f, 0.18f), Quaternion.identity);
            PrepareItem(item);
            StartCoroutine(MoveItemToSlot(_head, 2f, true, true));
        }
        else Debug.Log("You can wear only 1 item on your head");
    }

    public void ThrowItem(KeyCode keyCode)
    {
        GameObject item = null;
        switch (keyCode)
        {
            case KeyCode.E:
                {
                    item = _rightHand.Item;
                    _rightHand = null;
                    break;
                }
            case KeyCode.Q:
                {
                    item = _leftHand.Item;
                    _leftHand = null;
                    break;
                }
        }

        if(item != null)
        {
            var itemsRigitbody = item.GetComponent<Rigidbody>();
            itemsRigitbody.constraints = RigidbodyConstraints.None;
            itemsRigitbody.AddRelativeForce(new Vector3(0, 0, _throwStrength));
            item.transform.parent = null;

            var itemsCollider = item.GetComponent<Collider>();
            itemsCollider.enabled = true;

            item.GetComponent<LiftingObject>().Interactable = true;
        }
    }

    public void UseItem(int mouseButton)
    {
        UsibleObject usibleObject = null;

        switch (mouseButton)
        {
            case 0:
                {
                    usibleObject = _rightHand.UsibleObject;
                    break;
                }
            case 1:
                {
                    usibleObject = _leftHand.UsibleObject;
                    break;
                }
        }

        if (usibleObject != null)
            if(usibleObject.Usable)
                usibleObject.Use();
    }

    //-----------------------------------------------------
    private void PrepareItem(GameObject item)
    {
        var liftingObject = item.GetComponent<LiftingObject>();
        if (liftingObject != null)
            switch (liftingObject.Place)
            {
                case LiftingObject.PickUpPlace.Face:
                    {
                        item.transform.parent = Camera.main.transform;
                        break;
                    }
                case LiftingObject.PickUpPlace.Hands:
                    {
                        item.transform.parent = _leftHandTransform;
                        break;
                    }
            }

        item.GetComponent<Collider>().enabled = false;
        item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    
}
