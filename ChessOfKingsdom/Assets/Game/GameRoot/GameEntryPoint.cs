using GamePlay;
using MainMenu;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using R3;
using BaCon;
using States;
using Game.Settings;
using System.ComponentModel;

namespace GameRoot
{
    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;
        private Coroutines _coroutines;
        private UIViewRoot _uiRoot;
        private readonly DIContainer _rootContainer = new();
        private DIContainer _cachedSceneContainer;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutoStartGame()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            _instance = new GameEntryPoint();
            _instance.RunGame();
        }

        private GameEntryPoint()
        {
            _coroutines = new GameObject("[Coroutines]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines.gameObject);

            var prefapUIRoot = Resources.Load<UIViewRoot>("UIRoot");
            _uiRoot = Object.Instantiate(prefapUIRoot);
            Object.DontDestroyOnLoad(_uiRoot.gameObject);
            _rootContainer.RegisterInstance(_uiRoot);

            var settingsProvider = new SettingsProvider();
            _rootContainer.RegisterInstance<ISettingsProvider>(settingsProvider);

            var gameStateProvider = new PlayerPrefsGameStateProvider();
            // здесь должна быть команда загруски настроек
            _rootContainer.RegisterInstance<IGameStateProvider>(gameStateProvider);
        }

        private async void RunGame()
        {
            await _rootContainer.Resolve<ISettingsProvider>().LoadGameSettings();

#if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == Utils.Scenes.GASTLE)
            {
                var enterParams = new CastleEnterParams();
                _coroutines.StartCoroutine(LoadAndStartCastle(enterParams));
                return;
            }

            if(sceneName == Utils.Scenes.Fight)
            {
                var gameState = _rootContainer.Resolve<IGameStateProvider>().GameState;
                var resources = gameState.Resources;

                var enterParams = new FightEnterParams(resources);
                _coroutines.StartCoroutine(LoadAndStartFight(enterParams));
                return;
            }

            if (sceneName == Utils.Scenes.MENU)
            {
                _coroutines.StartCoroutine(LoadAndStartMainMenu());
                return;
            }

            if (sceneName != Utils.Scenes.BOOTSTRAP)
            {
                return;
            }
#endif

            _coroutines.StartCoroutine(LoadAndStartMainMenu());
        }

        private IEnumerator LoadAndStartCastle(CastleEnterParams enterParams)
        {
            _uiRoot.ShowLoadingScreen();
            _cachedSceneContainer?.Dispose();

            yield return LoadScene(Utils.Scenes.BOOTSTRAP);
            yield return LoadScene(Utils.Scenes.GASTLE);

            yield return new WaitForSeconds(1f);

            var isGameStateLoaded = false;
            _rootContainer.Resolve<IGameStateProvider>().LoadGameState()
                .Subscribe(_ => isGameStateLoaded = true);
            yield return new WaitUntil(() => isGameStateLoaded);

            var sceneEntryPoint = Object.FindObjectOfType<CastleEntryPoint>();
            var gamePlayContainer = _cachedSceneContainer = new DIContainer(_rootContainer);

            var exitSignals = sceneEntryPoint.Run(gamePlayContainer, enterParams);

            exitSignals[0].Subscribe(gameplayEnterParams =>
            {
                _coroutines.StartCoroutine(LoadAndStartMainMenu(gameplayEnterParams.MainMenuEnterParams));
            });

            exitSignals[1].Subscribe(gameplayEnterParams =>
            {
                _coroutines.StartCoroutine(LoadAndStartFight(gameplayEnterParams.FightEnterParams));
            });

            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadAndStartFight(FightEnterParams enterParams)
        {
            _uiRoot.ShowLoadingScreen();
            _cachedSceneContainer?.Dispose();

            yield return LoadScene(Utils.Scenes.BOOTSTRAP);
            yield return LoadScene(Utils.Scenes.Fight);

            yield return new WaitForSeconds(1f);

            var isGameStateLoaded = false;
            _rootContainer.Resolve<IGameStateProvider>().LoadGameState()
                .Subscribe(_ => isGameStateLoaded = true);
            yield return new WaitUntil(() => isGameStateLoaded);

            var sceneEntryPoint = Object.FindObjectOfType<FightEntryPoint>();
            var gamePlayContainer = _cachedSceneContainer = new DIContainer(_rootContainer);

            sceneEntryPoint.Run(gamePlayContainer, enterParams).Subscribe(gameplayEnterParams =>
            {
                _coroutines.StartCoroutine(LoadAndStartCastle(gameplayEnterParams.MainMenuEnterParams));
            });

            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadAndStartMainMenu(MenuEnterParams enterParams = null)
        {
            _uiRoot.ShowLoadingScreen();
            _cachedSceneContainer?.Dispose();

            yield return LoadScene(Utils.Scenes.BOOTSTRAP);
            yield return LoadScene(Utils.Scenes.MENU);

            yield return new WaitForSeconds(1f);

            var sceneEntryPoint = Object.FindObjectOfType<MainMenuEntryPoint>();
            var mainMenuContainer = _cachedSceneContainer = new DIContainer(_rootContainer);
            sceneEntryPoint.Run(mainMenuContainer, enterParams).Subscribe(mainMenuExitParams =>
            {
                var targetSceneName = mainMenuExitParams.TargetSceneEnterParams.SceneName;

                if (targetSceneName == Utils.Scenes.GASTLE)
                {
                    _coroutines.StartCoroutine(LoadAndStartCastle(mainMenuExitParams.TargetSceneEnterParams.As<CastleEnterParams>()));
                }
            });

            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
