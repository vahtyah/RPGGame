using TMPro;
using UnityEngine;

namespace Tyah.Scripts
{
    public class Log : MonoBehaviour
    {
        public static Log Create(Transform parent, string text, float fontSize = 36)
        {
            var prefab = Resources.Load<Log>("Log");
            var mess = Instantiate(prefab, parent);
            var messScript = mess.GetComponent<Log>();
            messScript.text.text = Time.time.ToString("F") +": " + text;
            messScript.text.fontSize = fontSize;
            return messScript;
        }
        
        private TextMeshProUGUI text => GetComponentInChildren<TextMeshProUGUI>();
    }
}