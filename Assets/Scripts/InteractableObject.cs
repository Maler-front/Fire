using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField] private bool _interactable = true;
    
    public bool Interactable
    {
        get { return _interactable; }
        set { _interactable = value; }//Временно публичный
    }

    abstract public void Interact(GameObject _player);
}
