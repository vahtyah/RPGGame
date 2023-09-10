using System;
using System.Collections;
using DefaultNamespace;
using Save_and_Load;
using UnityEditor;
using UnityEngine;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject continueBtn;
        [SerializeField] private LoadScreen loadScreen;
        

        private void Start() { continueBtn.SetActive(SaveManager.Instance.HasSavedData()); }

        public void OnContinueButtonClick()
        {
            StartCoroutine(LoadSceneWithFadeEffect(1f));
        }

        public void OnNewGameButtonClick()
        {
            SaveManager.Instance.DeleteSavedData();
            StartCoroutine(LoadSceneWithFadeEffect(1f));
        }

        public void OnExitGameButtonClick()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        private IEnumerator LoadSceneWithFadeEffect(float delay)
        {
            loadScreen.FadeOut();
            
            yield return new WaitForSeconds(delay);
            
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        }
    }
}