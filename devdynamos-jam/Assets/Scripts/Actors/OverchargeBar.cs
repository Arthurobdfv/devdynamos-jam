using UnityEngine;

public class OverchargeBar : MonoBehaviour
{
    [SerializeField] private RobotBehaviour robotBehaviour;
    [SerializeField] private SpriteRenderer fillbarRenderer;
    [SerializeField] private Material material;
    // Start is called before the first frame update
    void Start()
    {
        robotBehaviour = GetComponentInParent<RobotBehaviour>() ?? throw new MissingComponentException(nameof(RobotBehaviour));
        material = fillbarRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        material.SetFloat("FillBar",robotBehaviour.CurrentOverloadRate);
    }
}
