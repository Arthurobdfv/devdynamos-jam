using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBar : MonoBehaviour
{
    [SerializeField] private FillBar fuelBar;
    [SerializeField] private SpriteRenderer fillbarRenderer;
    [SerializeField] private Material material;
    // Start is called before the first frame update
    void Start()
    {
        fuelBar = GetComponentInParent<FillBar>() ?? throw new MissingComponentException(nameof(FillBar));
        material = fillbarRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        material.SetFloat("_FillBar", fuelBar.CurrentFillRate);
    }
}
