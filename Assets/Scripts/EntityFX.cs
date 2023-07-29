using System.Collections;
using TMPro.EditorUtilities;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    [Header("Flash FX")]
    [SerializeField] private Material hitMat;
    [SerializeField] private float flashDuration;
    private Material oriMat;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        oriMat = sr.material;
    }

    private IEnumerator FlashFX()
    {
        sr.material = hitMat;
        yield return new WaitForSeconds(flashDuration);
        sr.material = oriMat;
    }

    private void RedColorBlink() { sr.color = sr.color != Color.white ? Color.white : Color.red; }

    private void CancelRedBlink()
    {
        CancelInvoke();
        sr.color = Color.white;
    }
}