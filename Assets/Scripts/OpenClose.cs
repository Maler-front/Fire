using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class OpenClose : InteractableObject
{
    [SerializeField]
    private float _duration;
    [Range(-90f, 90f)] [SerializeField]
    private float _openAngle = 90f;
    [SerializeField]
    private AnimationCurve _animationCurve;

    private Vector3 _startRotation;
    private Coroutine _rotateCoroutine;

    private void Start()
    {
        _startRotation = transform.eulerAngles;
    }

    public override void Interact(GameObject _player)
    {
        switch (GetDoorState())
        {
            case DoorState.Open:
                {
                    Close();
                    break;
                }
            case DoorState.Close:
                {
                    Open();
                    break;
                }
            default: { break; }
        }
    }

    private void Open()
    {
        if (_rotateCoroutine != null)
            StopCoroutine(_rotateCoroutine);

        _rotateCoroutine = StartCoroutine(Rotate(GetCurrentAngle(), _openAngle));
    }

    private void Close()
    {
        if (_rotateCoroutine != null)
            StopCoroutine(_rotateCoroutine);

        _rotateCoroutine = StartCoroutine(Rotate(GetCurrentAngle(), 0));
    }

    private IEnumerator Rotate(float from, float to)
    {
        for(float i = 0; i < 1; i += Time.deltaTime / _duration)
        {
            transform.rotation = Quaternion.Lerp(
                Quaternion.Euler(_startRotation.x, _startRotation.y + from, _startRotation.z),
                Quaternion.Euler(_startRotation.x, _startRotation.y + to, _startRotation.z),
                _animationCurve.Evaluate(i)
                );

            yield return null;
        }

        transform.rotation = Quaternion.Euler(_startRotation.x, _startRotation.y + to, _startRotation.z);
        _rotateCoroutine = null;
    }

    private float GetCurrentAngle()
    {
        float currentAngle = Quaternion.Angle(Quaternion.Euler(_startRotation), transform.rotation);
        currentAngle *= _openAngle > 0 ? 1 : -1;
        return currentAngle;
    }

    private DoorState GetDoorState()
    {
        if (Mathf.Approximately(0, GetCurrentAngle()))
            return DoorState.Close;
        if (Mathf.Approximately(_openAngle, GetCurrentAngle()))
            return DoorState.Open;

        return DoorState.Undefined;
    }

    private enum DoorState
    {
        Open,
        Close, 
        Undefined
    }
}
