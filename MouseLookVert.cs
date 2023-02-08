using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookVert : MonoBehaviour
{
    [SerializeField] private float _sensivity = 9f;

    private float _maximum = 70f;
    private float _minimum = -70f;
    private float _rotationX = 0f;

    void Update()
    {
        _rotationX -= Input.GetAxis("Mouse Y") * _sensivity;
        _rotationX = Mathf.Clamp(_rotationX, _minimum, _maximum);
        var rotationY = transform.localEulerAngles.y;

        transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
    }
}
