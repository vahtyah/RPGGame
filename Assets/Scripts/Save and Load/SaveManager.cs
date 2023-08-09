using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = System.Object;

namespace Save_and_Load
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance { get; private set; }

        [SerializeField] private string fileName;
        private List<ISaveManager> saveManagers;
        private GameData gameData;
        private FileDataHandler dataHandler;
        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else Instance = this;
        }

        private void Start()
        {
            dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
            saveManagers = FindAllSaveManagers();
            LoadGame();
        }

        public void NewGame()
        {
            gameData = new GameData();
        }

        private void LoadGame()
        {
            gameData = dataHandler.Load();
            if (gameData == null)
            {
                NewGame();
            }

            foreach (var saveManager in saveManagers)
            {
                saveManager.LoadData(gameData);
            }
        }

        public void SaveGame()
        {
            foreach (var saveManager in saveManagers)
            {
                saveManager.SaveData(ref gameData);
            }
            
            dataHandler.Save(gameData);
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }
        private List<ISaveManager> FindAllSaveManagers()
        {
            // var saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();
            // var saveManagers = FindObjectsOfType<MonoBehaviour>().Where(mb => mb is ISaveManager).Cast<ISaveManager>();
            var saveManagers = Resources.FindObjectsOfTypeAll<MonoBehaviour>().Where(mb => mb is ISaveManager).Cast<ISaveManager>();
            return new List<ISaveManager>(saveManagers);
        }

    }
}