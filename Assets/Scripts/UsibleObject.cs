using UnityEngine;

public abstract class UsibleObject : MonoBehaviour
{
    [SerializeField] private bool _usable = true;

    public bool Usable
    {
        get { return _usable; }
        set { _usable = value; }//Временно публичный
    }

    abstract public void Use();
}
