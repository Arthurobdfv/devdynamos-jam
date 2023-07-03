using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartAnimation : MonoBehaviour
{
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _jumpHeight;
    private Vector3 _initialPosition;

    [SerializeField] GameObject _shipOpenSmoke;
    [SerializeField] GameObject _hitGroundSmoke;
    [SerializeField] List<AudioClip> _spaceshopOpenSound;
    [SerializeField] AudioClip _spaceshipJump;

    private float Constant = Mathf.Deg2Rad * 180;

    public void Start()
    {
        _initialPosition = transform.position;
        StartCoroutine(StartAnimation(SceneManage.Instance.InitialAnimationDuration, SceneManage.Instance.InitialAnimationDelay));
    }

    public IEnumerator StartAnimation(float animationTime, float animationDelay)
    {
        //Instantiate smoke
        AudioManager.PlayFromRandomClips(_spaceshopOpenSound.ToArray());
        if(_shipOpenSmoke != null) Instantiate(_shipOpenSmoke);
        yield return new WaitForSeconds(animationDelay);

        var time = 0f;
        var totalAnimationTime = animationTime - animationDelay;
        if(_spaceshipJump != null) AudioManager.PlaySound(_spaceshipJump);
        while (time <= totalAnimationTime)
        {
            var animationTimeRate = time / totalAnimationTime;
            transform.position = Vector3.Lerp(_initialPosition, _endPoint.position, animationTimeRate) + new Vector3(0f, Mathf.Sin(Constant * animationTimeRate) * _jumpHeight, 0f);
            yield return new WaitForFixedUpdate();
            time += Time.deltaTime;
        }
        transform.position = _endPoint.position;
        // instantiate smoke
        if (_hitGroundSmoke != null) Instantiate(_hitGroundSmoke, transform.position, Quaternion.identity);
    }
}
