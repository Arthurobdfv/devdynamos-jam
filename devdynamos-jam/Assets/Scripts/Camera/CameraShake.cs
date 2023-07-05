using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    /*public IEnumerator Shake(float duration = .15f, float magnitude = .4f)
    {
        Vector3 _startPos = transform.localPosition;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, _startPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = _startPos;
    }*/
}
