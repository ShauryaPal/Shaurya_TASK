using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShakeManager : MonoBehaviour 
{
    public static CameraShakeManager instance;
    public AnimationCurve shakeCurve;
    public float strengthMultiplier;

    private void Awake()
    {
        instance = this;
    }

    public void ShakeCamera(float duration = 1f)
    {
        StartCoroutine(Shaking(duration));
    }
    
    private IEnumerator Shaking(float duration) {
        var startPosition = transform.position;
        var elapsedTime = 0f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            var strength = shakeCurve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * (strength * strengthMultiplier);
            yield return null;
        }

        transform.position = startPosition;
    }
}