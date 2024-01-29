using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChuongCustom
{
    public class SceneLoader : PersistentSingleton<SceneLoader>
    {
        [HideInInspector] public string sceneName;

        public void LoadScene(string nameScene)
        {
            sceneName = nameScene;
            SceneManager.LoadScene("FakeLoading");
        }

        public void Play()
        {
            LoadScene($"GameScene");
        }
        
        public void LoadHome()
        {
            LoadScene("Home");
        }
    }
}