using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private bool _includeMouseOnCameraPos = false;
    [SerializeField] private List<GameObject> _targets;
    [SerializeField] private float _mouseWeight = 1;
    [SerializeField] private float _cameraInitialSize = 5;
    [SerializeField] private float _cameraMaxSize = 12;
    public void Start()
    {
        RefreshCamera();
    }

    public void LateUpdate()
    {

        transform.position = CalculatePosition();
        CalculateCameraSize();
    }

    private Vector3 CalculatePosition()
    {
        var sum = Vector3.zero;
        float count = 0;


            foreach (var target in _targets)
            {
                sum += target.transform.position;
                count++;
            }
            if (_includeMouseOnCameraPos && !Application.isMobilePlatform)
            {
                sum += GetMousePosition() * _mouseWeight;
                count += _mouseWeight;
            }

        var avg = sum / count;
        return avg;
    }

    private void CalculateCameraSize()
    {
        var distance = _targets[1].transform.position - _targets[0].transform.position;
        Camera.main.orthographicSize = Mathf.Clamp(_cameraInitialSize, (_cameraInitialSize + distance.magnitude) / 2, 15);
    }

    public void RefreshCamera()
    {
        var test = FindObjectsOfType<CameraFollowTarget>();
        _targets = new List<GameObject>(FindObjectsOfType<CameraFollowTarget>().Select(x => x.gameObject));
    }

    private Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
