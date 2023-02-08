using System.Collections;
using TMPro;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    Coroutine _coroutine;

    private IEnumerator FadeOutCoroutine(GameObject fadeOutLeaf, bool fade, float fadeFramesDuration)
    {
        var step = 1f / (60f * fadeFramesDuration);
        Material material = null;
        Color color;

        Renderer renderer = fadeOutLeaf.GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
            Debug.Log("Fading out with renderer");
        }

        TextMeshPro textMeshPro = fadeOutLeaf.GetComponent<TextMeshPro>();
        if (textMeshPro != null)
        {
            material = textMeshPro.material;
            Debug.Log("Fading out with text");
        }
        Debug.Log("lol");

        if (material != null)
        {
            color = material.GetColor("_Color");
            Debug.Log("Color.a = " + color.a);

            if (fade)
            {
                while (color.a > 0f)
                {
                    yield return new WaitForEndOfFrame();
                    color.a = Mathf.Clamp(color.a - step, 0f, 1f);
                    material.SetColor("_Color", color);
                }
            }
            else
            {
                while (color.a < 1f)
                {
                    yield return new WaitForEndOfFrame();
                    color.a = Mathf.Clamp(color.a + step, 0f, 1f);
                    material.SetColor("_Color", color);
                }
            }
        }

        _coroutine = null;
    }

    public void FadeOut(GameObject fadeOutLeaf, float fadeFramesDuration)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(FadeOutCoroutine(fadeOutLeaf, true, fadeFramesDuration));
    }

    public void FadeIn(GameObject fadeOutLeaf, float fadeFramesDuration)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(FadeOutCoroutine(fadeOutLeaf, false, fadeFramesDuration));
    }
}
