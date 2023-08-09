using System;
using DefaultNamespace;
using Save_and_Load;
using UnityEditor;
using UnityEngine;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject continueBtn;

        private void Start()
        {
            if(!SaveManager.Instance.HasSavedData()) continueBtn.SetActive(false);
        }

        public void OnContinueButtonClick()
        {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        }

        public void OnNewGameButtonClick()
        {
            SaveManager.Instance.DeleteSavedData();
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        }

        public void OnExitGameButtonClick()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}