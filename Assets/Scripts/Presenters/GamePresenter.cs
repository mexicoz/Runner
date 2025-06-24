using System;
using System.Collections;
using GamePlay;
using Models;
using Player;
using Scenes;
using UnityEngine;
using View;

namespace Presenters
{
    [RequireComponent(typeof(SceneLoader), typeof(ViewHandler))]
    public class GamePresenter : MonoBehaviour
    {
        [SerializeField] private float pointsSetDelay = 2f;
        [SerializeField] private PlayerCollide playerPrefab;
        [SerializeField] private PausedHandler pause;
        
        private static GamePresenter _instance;
        
        private GameModel _gameModel;
        private ViewHandler _viewHandler;
        private PlayerCollide _player;
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
            pause.IsPause = false;
            _gameModel = new GameModel(pointsSetDelay);
            _viewHandler = GetComponent<ViewHandler>();
            _viewHandler.InstantiateMainMenu();
            _viewHandler.InstantiateHud();
            _viewHandler.GetMainMenu().OnPlay += LoadGameplayScene;
        }

        private void GameOver()
        {
            StartCoroutine(GameOverRoutine());
        }

        private void Update()
        {
            if (!_isGameplay || pause.IsPause) return;
            
            _gameModel.Update(Time.deltaTime, (v) =>
            {
                _viewHandler.GetHud().SetScoreToHud(v);
            });
        }

        private void LoadGameplayScene()
        {
            StartCoroutine(WaitForSceneLoad(GetComponent<SceneLoader>().LoadGameplayScene()));
        }
        
        IEnumerator WaitForSceneLoad(AsyncOperation op)
        {
            yield return new WaitUntil(() => op.isDone);
            UpdateUiToGameScene();
            _player = Instantiate(playerPrefab);
            _player.GetComponent<PlayerCollide>().OnGameOver += GameOver;
            _isGameplay = true;
        }

        IEnumerator GameOverRoutine()
        {
            pause.IsPause = true;
            _viewHandler.PlayGameOver();
            yield return new WaitForSeconds(3);
            LoadMainMenuScene();
        }
        private void LoadMainMenuScene()
        {
            _player = null;
            GetComponent<SceneLoader>().LoadMainMenu();
            UpdateUiToMenuScene();
            pause.IsPause = false;
        }

        private void UpdateUiToGameScene()
        {
            _viewHandler.GetMainMenu().gameObject.SetActive(false);
            _viewHandler.GetHud().gameObject.SetActive(true);
            _viewHandler.GetHud().DisableGameOverText();
        }
        
        private void UpdateUiToMenuScene()
        {
            _gameModel.ResetScore((v) =>
            {
                _viewHandler.GetHud().SetScoreToHud(v);
            });
            _viewHandler.GetHud().gameObject.SetActive(false);
            _viewHandler.GetMainMenu().gameObject.SetActive(true);
        }
    }
}