using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSlider : MonoBehaviour
{
    [SerializeField] private SpriteMask mask;

    private const float VALUE_FOR_MAX_SCALE = 0.4f;

    private float currentValue = 20;
    private float maxValue = 100;

    public void Init(float currentValue, float maxValue)
    {
        this.currentValue = currentValue;
        this.maxValue = maxValue;
    }

    public void ChangeValue(float newValue)
    {
        currentValue = newValue;
        UpdateSlider();
    }

    private void UpdateSlider()
    {
        mask.transform.localPosition = new Vector3(0,-0.4f+(currentValue/maxValue)*VALUE_FOR_MAX_SCALE, 0);
        //mask.transform.localScale = new Vector3(mask.transform.localScale.x, currentValue);
    }
}
