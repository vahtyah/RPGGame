using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Transform fillHP;
    private Entity entity;
    private RectTransform rectTransform;
    private CharacterStats characterStats;

    private void Start()
    {
        characterStats = GetComponentInParent<CharacterStats>();
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
        var healthNormalized = Mathf.Clamp(characterStats.GetHealthAmountNormalized, 0, 1);
        fillHP.localScale = new Vector3(healthNormalized, 1, 1);
    }

    private void FlipUI()
    {
        rectTransform.Rotate(0,180,0);
    }
}