using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private Entity entity;
    private RectTransform rectTransform;
    private CharacterStats characterStats;
    private Slider slider;

    private void Start()
    {
        characterStats = GetComponentInParent<CharacterStats>();
        slider = GetComponentInChildren<Slider>();
        rectTransform = GetComponent<RectTransform>();
        entity = GetComponentInParent<Entity>();
        
        entity.onFlipped += (sender, args) =>
        {
            FlipUI();
        };
        characterStats.onHealthChanged += (sender, args) =>
        {
            UpdateHealthUI();
        };

    }


    private void UpdateHealthUI()
    {
        slider.value = characterStats.GetHealthAmountNormalized;
        Debug.Log("slider.value = " + slider.value);
    }

    private void FlipUI()
    {
        rectTransform.Rotate(0,180,0);
    }
}