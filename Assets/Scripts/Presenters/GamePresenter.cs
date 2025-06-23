using System.Collections;
using Models;
using Scenes;
using UnityEngine;
using View;

namespace Presenters
{
    [RequireComponent(typeof(SceneLoader))]
    public class GamePresenter : MonoBehaviour
    {
        [SerializeField] private MainMenu mainMenuPrefab;
        [SerializeField] private Hud hudPrefab;
        [SerializeField] private float pointsSetDelay = 2f;
        
        private static GamePresenter _instance;
        
        private GameModel _gameModel;
        private MainMenu _mainMenu;
        private Hud _hud;
        private bool _isGameplay;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }
            Destroy(gameObject);
        }

        private void Start()
        {
            _gameModel = new GameModel(pointsSetDelay);
            InstantiateMainMenu();
            InstantiateHud();
            _mainMenu.OnPlay += LoadGameplayScene;
        }

        private void InstantiateHud()
        {
            _hud = Instantiate(hudPrefab, gameObject.transform);
            _hud.gameObject.SetActive(false);
        }

        private void InstantiateMainMenu()
        {
            _mainMenu = Instantiate(mainMenuPrefab, gameObject.transform);
        }

        private void Update()
        {
            if (!_isGameplay) return;
            
            _gameModel.Update(Time.deltaTime, (v) =>
            {
                _hud.SetScoreToHud(v);
            });
        }

        private void OnDisable()
        {
            _mainMenu.OnPlay -= LoadGameplayScene;
        }

        private void LoadGameplayScene()
        {
            StartCoroutine(WaitForSceneLoad(GetComponent<SceneLoader>().LoadGameplayScene()));
        }
        
        IEnumerator WaitForSceneLoad(AsyncOperation op)
        {
            yield return new WaitUntil(() => op.isDone);
            _mainMenu.gameObject.SetActive(false);
            _hud.gameObject.SetActive(true);
            _isGameplay = true;
        }

        private void LoadMainMenuScene()
        {
            GetComponent<SceneLoader>().LoadMainMenu();
            _hud.gameObject.SetActive(false);
            _mainMenu.gameObject.SetActive(true);
        }
    }
}