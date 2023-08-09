using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class GameSceneManager
    {
        public enum Scene
        {
            MainMenu,
            GameScene
        }
        
        public static void Load(Scene scene)
        {
            SceneManager.LoadScene(scene.ToString());
        }
    }
}