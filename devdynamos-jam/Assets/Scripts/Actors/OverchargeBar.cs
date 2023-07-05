using UnityEngine;

public class OverchargeBar : MonoBehaviour
{
    [SerializeField] private RobotBehaviour robotBehaviour;
    [SerializeField] private SpriteRenderer fillbarRenderer;
    [SerializeField] private SpriteRenderer filliconRenderer;
    [SerializeField] private Material material;
    [SerializeField] private float _timeShowing;
    [SerializeField] private float _fadeoutTime;
    private float _timer;
    [SerializeField] private float _currentFill;
    private bool _dissappeared;

    private float CurrentFill
    {
        get
        {
            return _currentFill;
        }
        set
        {
            if(_currentFill != value)
            {
                OnValueChange(_currentFill, value);
                _currentFill = value;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _dissappeared = false;
        robotBehaviour = GetComponentInParent<RobotBehaviour>() ?? throw new MissingComponentException(nameof(RobotBehaviour));
        material = fillbarRenderer.material;
        filliconRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentFill = robotBehaviour.CurrentOverloadRate;
        material.SetFloat("_FillBar", robotBehaviour.CurrentOverloadRate);
        CheckFadeOut();
        _timer += Time.deltaTime;
    }

    private void OnValueChange(float oldValue, float newValue)
    {
        if (newValue < oldValue)
        {
            SetFullVisibility();
            ResetTimer();
        }
    }

    private void SetFullVisibility()
    {
        SetAlphaVisibility(1f);
        _dissappeared = false;
    }

    private void SetAlphaVisibility(float alpha)
    {
        filliconRenderer.color = new Color(filliconRenderer.color.r, filliconRenderer.color.g, filliconRenderer.color.b, alpha);
        material.SetFloat("_AlphaRate", alpha);
    }

    private void ResetTimer()
    {
        _timer = 0f;
    }
    private void CheckFadeOut()
    {
        if(_timer > _timeShowing && !_dissappeared)
        {
            var mod = (_timer % _timeShowing);
            var alpharate = 1 - (mod / _fadeoutTime);
            SetAlphaVisibility(alpharate);
            if(alpharate < .05f)
            {
                _dissappeared = true;
                SetAlphaVisibility(0f);
            }
        }
    }
}
