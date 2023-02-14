using System.Collections;
using UnityEngine;

public class Match : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _fire;
    [SerializeField]
    private Light _light;
    [SerializeField]
    private AnimationCurve _animationCurve;

    private float _lightIntensity;
    private Coroutine _coroutine = null;

    private void Start()
    {
        if (_fire == null)
            throw new System.Exception("_fire in object \"" + gameObject.name + "\" doesn't found");
        if (_light == null)
            throw new System.Exception("_light in object \"" + gameObject.name + "\" doesn't found");

        _lightIntensity = _light.intensity;
    }

    public void Burn(bool burning, float durationSeconds)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (!burning)
        {
            _fire.Stop();
            _coroutine = StartCoroutine(Light(0f, durationSeconds));
        }
        else
        {
            _fire.Play();
            _coroutine = StartCoroutine(Light(_lightIntensity, durationSeconds));
        }
    }

    private IEnumerator Light(float to, float durationSeconds)
    {
        for (float i = 0; i < 1f; i += Time.deltaTime / durationSeconds)
        {
            _light.intensity = Mathf.Lerp(
                    _light.intensity,
                    to,
                    _animationCurve.Evaluate(i)
                );

            yield return null;
        }

        _light.intensity = to;
        _coroutine = null;
    }
}
