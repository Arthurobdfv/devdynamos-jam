using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartAnimation : MonoBehaviour
{
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _jumpHeight;
    private Vector3 _initialPosition;

    private float Constant = Mathf.Deg2Rad * 180;

    public void Start()
    {
        _initialPosition = transform.position;
        StartCoroutine(StartAnimation(SceneManage.Instance.InitialAnimationDuration));
    }

    public IEnumerator StartAnimation(float animationTime)
    {
        var time = 0f;
        while (time <= animationTime)
        {
            transform.position = Vector3.Lerp(_initialPosition, _endPoint.position, time / animationTime) + new Vector3(0f, Mathf.Sin(Constant * (time / animationTime)) * _jumpHeight, 0f);
            yield return new WaitForFixedUpdate();
            time += Time.deltaTime;
        }
        transform.position = _endPoint.position;
    }
}
