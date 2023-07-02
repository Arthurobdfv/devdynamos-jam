using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private bool _includeMouseOnCameraPos = false;
    private List<Vector3> _targets;
    public void Start()
    {
        RefreshCamera();
    }

    public void LateUpdate()
    {
        CalculatePosition();
    }

    private Vector3 CalculatePosition()
    {
        var sum = Vector3.zero;
        int count = 0;
        foreach (var target in _targets)
        {
            sum += target;
            count++;
        }
        if (_includeMouseOnCameraPos)
        {
            sum += GetMousePosition();
            count++;
        }

        return sum / count;
    }

    public void RefreshCamera()
    {
        _targets = new List<Vector3>(FindObjectsOfType<CameraFollowTarget>().Select(x => x.transform.position));
    }

    private Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
