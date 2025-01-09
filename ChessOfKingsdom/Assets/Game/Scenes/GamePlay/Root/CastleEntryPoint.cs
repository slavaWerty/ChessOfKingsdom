using BaCon;
using GamePlay.Services;
using GamePlay.View;
using GameRoot;
using MainMenu;
using mBuilding.Scripts.Game.Common;
using R3;
using States;
using UnityEngine;

namespace GamePlay
{
    public class CastleEntryPoint : MonoBehaviour
    {
        [SerializeField] private UICastleRootBinder _sceneUIRootPrefap;
        [SerializeField] private WorldCastleRootBinder _worldBinder;

        public Observable<CastleExitParams>[] Run(DIContainer container, CastleEnterParams enterParams)
        {
            CastleRegistrations.Regiter(container, enterParams);
            var gameplayViewModelsContainer = new DIContainer(container);
            CastleViewModelsRegistrations.Register(gameplayViewModelsContainer, _worldBinder);

            InitUI(gameplayViewModelsContainer);
            InitWorld(gameplayViewModelsContainer);

            var buildingsService = container.Resolve<BuildingsService>();

            var cmd = container.Resolve<ICommandProcessor>();
            var command = new CmdCreateBuildingState();
            cmd.Process(command);

            var transitionToMenuSceneSignalsubj = container.Resolve<Subject<Unit>>(AppConstants.EXIT_IN_MENU_SCENE_REQUEST_TAG);
            var transitionToFightSceneSignalsubj = container.Resolve<Subject<Unit>>(AppConstants.EXIT_IN_FIGHT_SCENE_REQUEST_TAG);

            var mainMenuenterParams = new MenuEnterParams("fatality");
            var resources = container.Resolve<IGameStateProvider>().GameState.Resources;
            var fightEnterParams = new FightEnterParams(resources);
            var exitParams = new CastleExitParams(mainMenuenterParams, fightEnterParams);
            var exitTiMainMenuSceneSignal = transitionToMenuSceneSignalsubj.Select(_ => exitParams);
            var exitToFightSceneSignal = transitionToFightSceneSignalsubj.Select(_ => exitParams);

            return new[] { exitTiMainMenuSceneSignal, exitToFightSceneSignal };
        }

        private void InitWorld(DIContainer container)
        {
            _worldBinder.Bind(container.Resolve<WorldCastleRootViewModel>());
        }

        private void InitUI(DIContainer container)
        {
            var uiRoot = container.Resolve<UIViewRoot>();
            var sceneRootBinder = Instantiate(_sceneUIRootPrefap);
            uiRoot.AttachSceneUI(sceneRootBinder.gameObject);

            sceneRootBinder.Bind(container.Resolve<UICastleRootViewModel>());

            var uiManager = container.Resolve<CastleUIManager>();
            uiManager.OpenScreenGameplay();
        }
    }
}
