using UnityEngine;

public abstract class UsibleObject : MonoBehaviour
{
    [SerializeField] private bool _usable = true;

    public bool Usable
    {
        get { return _usable; }
        set { _usable = value; }//�������� ���������
    }

    abstract public void Use();
}
