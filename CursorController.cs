using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField]
    private bool _locked = false;

    private void Start()
    {
        Locked = true;//Временно да появления меню
    }

    public bool Locked 
    {
        get { return _locked; }
        set 
        {
            if (value)
                Off();
            else
                On();
            _locked = value;
        }
    }

    private void Off()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void On()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
