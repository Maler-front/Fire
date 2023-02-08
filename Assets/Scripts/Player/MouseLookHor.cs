using UnityEngine;

public class MouseLookHor : MonoBehaviour
{
    [SerializeField] private float _sensivity = 9f;

    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * _sensivity, 0);
    }
}
