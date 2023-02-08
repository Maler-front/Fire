using UnityEngine;

[RequireComponent(
    typeof(CharacterController) 
    //typeof(Rigidbody)
    )]

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _interactRange;
    [SerializeField]
    private Inventory _inventory;

    //private Rigidbody _rigitbody;
    private Camera _camera;
    private CharacterController _characterController;

    [SerializeField]
    private MouseLookHor _mouseLookHor;
    [SerializeField]
    private MouseLookVert _mouseLookVert;

    void Start()
    {
        //_rigitbody = GetComponent<Rigidbody>();
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main;
    }

    void Update()
    {
        Moving();
        if (Input.GetKeyDown(KeyCode.F))
            InteractWithObject();

        if (Input.GetKeyDown(KeyCode.E))
            _inventory.ThrowItem(KeyCode.E);
        if (Input.GetKeyDown(KeyCode.Q))
            _inventory.ThrowItem(KeyCode.Q);

        if (Input.GetMouseButton(1))
            _inventory.UseItem(1);
        if (Input.GetMouseButton(0))
            _inventory.UseItem(0);
    }

    private void Moving()
    {
        var deltaX = Input.GetAxis("Horizontal") * _speed;
        var deltaZ = Input.GetAxis("Vertical") * _speed;
        var motion = new Vector3(deltaX, 0, deltaZ);
        motion = Vector3.ClampMagnitude(motion, _speed);
        motion.y = -9.8f;
        motion *= Time.deltaTime;

        motion = transform.TransformDirection(motion);
        _characterController.Move(motion);
    }

    private void InteractWithObject()
    {
        var point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
        var ray = _camera.ScreenPointToRay(point);

        if (Physics.Raycast(ray, out var hit, _interactRange))
        {
            InteractableObject interactObject = hit.transform.GetComponent<InteractableObject>();
            if (interactObject != null)
                if (interactObject.Interactable)
                    interactObject.Interact(gameObject);
        }
    }

    private void OnEnable()
    {
        _mouseLookHor.enabled = true;
        _mouseLookVert.enabled = true;
    }

    private void OnDisable()
    {
        _mouseLookVert.enabled = false;
        _mouseLookHor.enabled = false;
    }
}
