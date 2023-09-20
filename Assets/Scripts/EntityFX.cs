using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    [Header("Flash FX")]
    [SerializeField] private Material hitMat;
    [SerializeField] private float flashDuration;
    private Material oriMat;
    private SpriteRenderer sr;
    
    [Header("Ailment colors")]
    [SerializeField] private Color[] chillColor;
    [SerializeField] private Color[] igniteColor;
    [SerializeField] private Color[] shockColor;
    
    [Header("Slashing FX")]
    [SerializeField] private Color chillSlashColor;
    [SerializeField] private Color igniteSlashColor;
    [SerializeField] private Color shockSlashColor;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        oriMat = sr.material;
        chillSlashColor = 50 * chillSlashColor;
        igniteSlashColor = 50 * igniteSlashColor;
        shockSlashColor = 50 * shockSlashColor;
    }
    
    public void MakeTransparent(bool transparent)
    {
        if(transparent)
            sr.color = Color.clear;
        else sr.color = Color.white;
    }

    private IEnumerator FlashFX()
    {
        sr.material = hitMat;
        var currentColor = sr.color;
        sr.color = Color.white;
        yield return new WaitForSeconds(flashDuration);
        sr.color = currentColor;
        sr.material = oriMat;
    }

    private void RedColorBlink() { sr.color = sr.color != Color.white ? Color.white : Color.red; }

    private void CancelColorChange()
    {
        CancelInvoke();
        sr.color = Color.white;
    }

    public void IgniteFxFor(float seconds)
    {
        InvokeRepeating("IgniteColorFx",0,.3f);
        Invoke("CancelColorChange",seconds);
    }

    private void IgniteColorFx()
    {
        sr.color = sr.color != igniteColor[0] ? igniteColor[0] : igniteColor[1];
    }

    public void ChillFxFor(float seconds)
    {
        InvokeRepeating("ChillColorFx",0,.3f);
        Invoke("CancelColorChange", seconds);
    }
    
    public void ShockFxFor(float seconds)
    {
        InvokeRepeating("ShockColorFx",0,.3f);
        Invoke("CancelColorChange",seconds);
    }
    
    private void ShockColorFx()
    {
        sr.color = sr.color != shockColor[0] ? shockColor[0] : shockColor[1];
    }
    
    private void ChillColorFx()
    {
        sr.color = sr.color != chillColor[0] ? chillColor[0] : chillColor[1];
    }
    
    public Color GetNextColorSplash()
    {
        var stats = GetComponent<CharacterStats>();
        var nextAilment = stats.GetNextAilment;
        switch (nextAilment)
        {
            case NextAilment.ignite:
                return igniteSlashColor;
            case NextAilment.chill:
                return chillSlashColor;
            case NextAilment.shock:
                return shockSlashColor;
            default:
                return Color.white;
        }
    }

    public Material GetMaterial => sr.material;

}