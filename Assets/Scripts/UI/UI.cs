using UnityEngine;

namespace UI
{
    public class UI : MonoBehaviour
    {
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