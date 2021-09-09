using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake (float duration, AnimationCurve curve)
    {
        Vector3 originalPos = transform.position;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float str = curve.Evaluate(elapsed / duration);
            transform.position = originalPos + Random.insideUnitSphere * str;

            yield return null;
        }

        transform.position = originalPos;
    }
}
