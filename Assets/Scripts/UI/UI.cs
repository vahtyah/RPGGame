using System;
using System.Collections;
using DefaultNamespace;
using Item_and_Inventory.Test;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UI : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject menuUI;
        [SerializeField] private GameObject dieScreen;
        public ToolTipUI toolTipUI;

        private InventoryManager inventory;

        private void Start()
        {
            toolTipUI = GetComponentInChildren<ToolTipUI>(); 
            inventory = InventoryManager.Instance;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                menuUI.gameObject.SetActive(!menuUI.gameObject.activeSelf);
                Time.timeScale = menuUI.gameObject.activeSelf ? 0 : 1;
                inventory.inventory.SelectItem(null);
            }
        }

        public void SwitchTo(GameObject elementMenu)
        {
            for (var i = 0; i < menuUI.transform.childCount - 1; i++)
            {
                menuUI.transform.GetChild(i).gameObject.SetActive(false);
            }

            inventory.inventory.SelectItem(null);
            elementMenu.gameObject.SetActive(true);
        }

        public void SwitchOnEndScreen()
        {
            LoadScreen.Instance.FadeOut();
            StartCoroutine(EndScreenWithDelay());
        }

        private IEnumerator EndScreenWithDelay()
        {
            yield return new WaitForSeconds(1);
            dieScreen.SetActive(true);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            inventory.inventory.SelectItem(null);
        }

        public void ResetGameButtonOnClick() => GameSceneManager.Load(GameSceneManager.Scene.GameScene);

        public void QuitGameButtonOnClick()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        private IEnumerator LoadScreenWithFade()
        {
            LoadScreen.Instance.FadeIn();
            dieScreen.SetActive(false);
            yield return new WaitForSeconds(1f);
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        }
    }
}