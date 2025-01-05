using BaCon;
using GamePlay.Services;
using GamePlay.View;
using GameRoot;
using MainMenu;
using R3;
using UnityEngine;

namespace GamePlay
{
    public class CastleEntryPoint : MonoBehaviour
    {
        [SerializeField] private UICastleRootBinder _sceneUIRootPrefap;
        [SerializeField] private WorldCastleRootBinder _worldBinder;

        [SerializeField] private GameObject _testObject;

        public Observable<CastleExitParams>[] Run(DIContainer container, CastleEnterParams enterParams)
        {
            CastleRegistrations.Regiter(container, enterParams);
            var gameplayViewModelsContainer = new DIContainer(container);
            CastleViewModelsRegistrations.Register(gameplayViewModelsContainer);

            _worldBinder.Bind(gameplayViewModelsContainer.Resolve<WorldCastleRootViewModel>());

            gameplayViewModelsContainer.Resolve<UICastleRootViewModel>();

            var buildingsService = container.Resolve<BuildingsService>();

            var cmd = container.Resolve<ICommandProcessor>();
            var command = new CmdCreateBuildingState();
            cmd.Process(command);

            var uiRoot = container.Resolve<UIViewRoot>();
            var uiScene = Instantiate(_sceneUIRootPrefap);
            uiRoot.AttachSceneUI(uiScene.gameObject);

            var transitionToMenuSceneSignalsubj = new Subject<Unit>();
            var transitionToFightSignalsubj = new Subject<Unit>();
            uiScene.Bind(transitionToMenuSceneSignalsubj, transitionToFightSignalsubj);

            var mainMenuenterParams = new MenuEnterParams("fatality");
            var fightEnterParams = new FightEnterParams(1);
            var exitParams = new CastleExitParams(mainMenuenterParams, fightEnterParams);
            var exitTiMainMenuSceneSignal = transitionToMenuSceneSignalsubj.Select(_ => exitParams);
            var exitToFightSceneSignal = transitionToFightSignalsubj.Select(_ => exitParams);

            return new[] { exitTiMainMenuSceneSignal, exitToFightSceneSignal };
        }
    }
}
