using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ArrowLineUI : MonoBehaviour
    {
        [SerializeField] private Transform fillImage;
        private bool isFilling;

        private void Update()
        {
            if (isFilling)
            {
                fillImage.transform.localScale =
                    Vector3.Lerp(fillImage.transform.localScale, new Vector3(1, 1, 1), Time.deltaTime);
            }

            if (fillImage.transform.localScale.y >= 1) enabled = false;
        }

        public void StartFilling()
        {
            isFilling = true;
        }

        public void SetFill()
        {
            fillImage.transform.localScale = Vector3.one;
        }
    }
}