using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private string mainMenuScene;
        [SerializeField] private string gameplayScene;

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(mainMenuScene);
        }

        public AsyncOperation LoadGameplayScene()
        {
            return SceneManager.LoadSceneAsync(gameplayScene);
        }
    }
}