using System.Collections;
using Models;
using Scenes;
using UnityEngine;
using View;

namespace Presenters
{
    [RequireComponent(typeof(SceneLoader), typeof(ViewHandler))]
    public class GamePresenter : MonoBehaviour
    {
        [SerializeField] private float pointsSetDelay = 2f;
        
        private static GamePresenter _instance;
        
        private GameModel _gameModel;
        private ViewHandler _viewHandler;
       
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
            _viewHandler = GetComponent<ViewHandler>();
            _viewHandler.InstantiateMainMenu();
            _viewHandler.InstantiateHud();
            _viewHandler.GetMainMenu().OnPlay += LoadGameplayScene;
        }

        private void Update()
        {
            if (!_isGameplay) return;
            
            _gameModel.Update(Time.deltaTime, (v) =>
            {
                _viewHandler.GetHud().SetScoreToHud(v);
            });
        }

        private void OnDisable()
        {
            _viewHandler.GetMainMenu().OnPlay -= LoadGameplayScene;
        }

        private void LoadGameplayScene()
        {
            StartCoroutine(WaitForSceneLoad(GetComponent<SceneLoader>().LoadGameplayScene()));
        }
        
        IEnumerator WaitForSceneLoad(AsyncOperation op)
        {
            yield return new WaitUntil(() => op.isDone);
            _viewHandler.GetMainMenu().gameObject.SetActive(false);
            _viewHandler.GetHud().gameObject.SetActive(true);
            _isGameplay = true;
        }

        private void LoadMainMenuScene()
        {
            GetComponent<SceneLoader>().LoadMainMenu();
            _viewHandler.GetHud().gameObject.SetActive(false);
            _viewHandler.GetMainMenu().gameObject.SetActive(true);
        }
    }
}