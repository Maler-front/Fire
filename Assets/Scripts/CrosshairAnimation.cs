using System.Collections;
using UnityEngine;

public class CrosshairAnimation : MonoBehaviour
{
    [SerializeField]
    private PlayerInput _playerInput;
    [SerializeField][Range(1, 300)]
    private int _speed;
    [SerializeField][Range(0, 0.5f)]
    private float _timeForReaction;

    private Coroutine _coroutine;
    private RectTransform _rectTransform;
    private float _currentTimeForReaction;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        var objectInCrosshair = _playerInput.ThrowRay();
        if (_coroutine == null)
        {
            if (objectInCrosshair.GetComponent<InteractableObject>() != null)
                if (objectInCrosshair.GetComponent<InteractableObject>().Interactable)
                    _coroutine = StartCoroutine(Animate());
        }
        else
        {
            bool stopCoroutine = false;
            if (objectInCrosshair.GetComponent<InteractableObject>() == null)
            {
                _currentTimeForReaction -= Time.deltaTime;
                stopCoroutine = true;
            }
            else
            {
                if (!objectInCrosshair.GetComponent<InteractableObject>().Interactable)
                {
                    _currentTimeForReaction -= Time.deltaTime;
                    stopCoroutine = true;
                }
                else
                {
                    _currentTimeForReaction = _timeForReaction;
                }
            }

            if (stopCoroutine && _currentTimeForReaction <= 0f)
            {
                StopCoroutine(_coroutine);
                _rectTransform.rotation = new Quaternion(0,0,0,1);
                _coroutine = null;
            }
        }
    }

    private IEnumerator Animate()
    {
        while(true)
        {
            yield return new WaitForEndOfFrame();

            _rectTransform.rotation = Quaternion.Euler(new Vector3(_rectTransform.eulerAngles.x, _rectTransform.eulerAngles.y, _rectTransform.eulerAngles.z + _speed * Time.deltaTime));
            Debug.Log(_rectTransform.rotation);
        }
    }
}
