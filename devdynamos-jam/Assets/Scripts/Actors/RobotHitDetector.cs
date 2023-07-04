using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotHitDetector : MonoBehaviour
{
    [SerializeField] private RobotBehaviour _robotBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        _robotBehaviour = GetComponentInParent<RobotBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _robotBehaviour.OnHit(collision);
    }
}
