using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBar : MonoBehaviour
{
    [SerializeField] private FillBar fuelBar;
    [SerializeField] private SpriteRenderer fillbarRenderer;
    [SerializeField] private SpriteRenderer iconBarRenderer;
    [SerializeField] private Material material;
    [SerializeField] private float _blinkCycleDuration;
    // Start is called before the first frame update
    void Start()
    {
        fuelBar = GetComponentInParent<FillBar>() ?? throw new MissingComponentException(nameof(FillBar));
        iconBarRenderer = GetComponent<SpriteRenderer>();
        material = fillbarRenderer.material;
        StartCoroutine(BlinkFuel());
    }
    // Update is called once per frame
    void Update()
    {
        material.SetFloat("_FillBar", fuelBar.CurrentFillRate);
        if (fuelBar.CurrentFillRate > 0f) StopCoroutine(BlinkFuel());
    }

    private IEnumerator BlinkFuel()
    {
        yield return new WaitForSeconds(SceneManage.Instance.InitialAnimationDuration);
        var time = 0f;
        var halfCycle = _blinkCycleDuration / 2;
        while (fuelBar.CurrentFillRate == 0)
        {

            SetIconEnable(time <= halfCycle);
            yield return new WaitForEndOfFrame();
            time = (time + Time.deltaTime) % _blinkCycleDuration;
        }
        SetIconEnable(true);
    }

    private void SetIconEnable(bool enable)
    {
        fillbarRenderer.enabled = enable;
        iconBarRenderer.enabled = enable;
    }
}
