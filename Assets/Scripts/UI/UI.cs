using UnityEngine;

namespace UI
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private GameObject characterUI;
        public ToolTipUI toolTipUI;
        private void Start() { toolTipUI = GetComponentInChildren<ToolTipUI>(); }

        public void SwitchTo(GameObject menu)
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            
            if(menu) menu.gameObject.SetActive(true);
        }
    }
}