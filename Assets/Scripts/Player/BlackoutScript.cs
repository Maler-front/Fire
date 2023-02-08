using System.Collections;
using UnityEngine;

public class BlackoutScript : MonoBehaviour
{
    Coroutine _coroutine;

    void Start()
    {
        _coroutine = StartCoroutine(Blackout(false));
    }

    private IEnumerator Blackout(bool isBlack = false, float fadeFramesDuration = 1f, string coroutineWaitFor = "")
    {
        float step;

        if(coroutineWaitFor != "")
        {
            yield return StartCoroutine(coroutineWaitFor);
        }

        step = 1f / (60f * fadeFramesDuration);
        Material material = Camera.main.GetComponentInChildren<Renderer>().material;
        Color color = material.GetColor("_Color");

        if (isBlack)
        {
            while (color.a < 1f)
            {
                yield return new WaitForEndOfFrame();
                color.a = Mathf.Clamp(color.a + step, 0f, 1f);
                material.SetColor("_Color", color);
            }
        }
        else
        {
            while (color.a > 0f)
            {
                yield return new WaitForEndOfFrame();
                color.a = Mathf.Clamp(color.a - step, 0f, 1f);
                material.SetColor("_Color", color);
            }
        }

        _coroutine = null;
    }

    public void On(float seconds)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Blackout(true, seconds));
    }

    public void Off(float seconds)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Blackout(false, seconds));
    }

    public void OnOff(float seconds)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Blackout(fadeFramesDuration: seconds / 2, coroutineWaitFor: "Blackout(true, " + seconds / 2 + ", \"\")"));
    }
}
