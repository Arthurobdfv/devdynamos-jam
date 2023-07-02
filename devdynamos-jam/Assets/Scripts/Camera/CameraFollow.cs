using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private bool _includeMouseOnCameraPos = false;
    [SerializeField] private List<GameObject> _targets;
    [SerializeField] private float _mouseWeight = 1;
    public void Start()
    {
        RefreshCamera();
    }

    public void LateUpdate()
    {
        transform.position = CalculatePosition();
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
        if (_includeMouseOnCameraPos)
        {
            sum += GetMousePosition() * _mouseWeight;
            count+= _mouseWeight;
        }

        return sum / count;
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
