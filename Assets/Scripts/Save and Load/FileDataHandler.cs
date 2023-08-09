using System;
using System.IO;
using UnityEngine;

namespace Save_and_Load
{
    public class FileDataHandler
    {
        private string dataDirPath = "";
        private string dataFileName = "";

        private bool encryptData;
        private string codeWord = "vahtyah";

        public FileDataHandler(string dataDirPath, string dataFileName, bool encryptData)
        {
            this.dataDirPath = dataDirPath;
            this.dataFileName = dataFileName;
            this.encryptData = encryptData;
        }

        public void Save(GameData gameData)
        {
            var fullPath = Path.Combine(dataDirPath, dataFileName);

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                var dataToStore = JsonUtility.ToJson(gameData, true);
                if (encryptData) dataToStore = EncryptDecrypt(dataToStore);
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
            Debug.Log(fullPath);
            GameData gameData = null;

            if (File.Exists(fullPath))
            {
                try
                {
                    var dataToLoad = "";
                    using var stream = new FileStream(fullPath, FileMode.Open);
                    using var reader = new StreamReader(stream);
                    dataToLoad = reader.ReadToEnd();

                    if (encryptData) dataToLoad = EncryptDecrypt(dataToLoad);
                    
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

        public void Delete()
        {
            var fullPath = Path.Combine(dataDirPath, dataFileName);
            if(File.Exists(fullPath))
                File.Delete(fullPath);
        }

        private string EncryptDecrypt(string data)
        {
            var modifiedData = "";
            for (int i = 0; i < data.Length; i++)
            {
                modifiedData += (char)(data[i] ^ codeWord[i % codeWord.Length]);
            }

            return modifiedData;
        }
    }
}