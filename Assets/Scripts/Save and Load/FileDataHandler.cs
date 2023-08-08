using System;
using System.IO;
using UnityEngine;

namespace Save_and_Load
{
    public class FileDataHandler
    {
        private string dataDirPath = "";
        private string dataFileName = "";

        public FileDataHandler(string dataDirPath, string dataFileName)
        {
            this.dataDirPath = dataDirPath;
            this.dataFileName = dataFileName;
        }

        public void Save(GameData gameData)
        {
            var fullPath = Path.Combine(dataDirPath, dataFileName);

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                var dataToStore = JsonUtility.ToJson(gameData, true);

                using var stream = new FileStream(fullPath, FileMode.Create);
                using var writer = new StreamWriter(stream);

                writer.Write(dataToStore);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e);
            }
        }

        public GameData Load()
        {
            var fullPath = Path.Combine(dataDirPath, dataFileName);
            GameData gameData = null;

            if (File.Exists(fullPath))
            {
                try
                {
                    var dataToLoad = "";
                    using var stream = new FileStream(fullPath, FileMode.Open);
                    using var reader = new StreamReader(stream);
                    dataToLoad = reader.ReadToEnd();

                    gameData = JsonUtility.FromJson<GameData>(dataToLoad);
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.Log(e);
                    throw;
                }
            }

            return gameData;
        }
    }
}