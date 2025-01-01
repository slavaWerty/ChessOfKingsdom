using BaCon;
using GamePlay.Commands;
using GamePlay.Services;
using GamePlay.View;
using GameRoot;
using MainMenu;
using ObservableCollections;
using R3;
using States;
using UnityEngine;

namespace GamePlay
{
    public class CastleEntryPoint : MonoBehaviour
    {
        [SerializeField] private UICastleRootBinder _sceneUIRootPrefap;
        [SerializeField] private WorldCastleRootBinder _worldBinder;

        public Observable<CastleExitParams> Run(DIContainer container, CastleEnterParams enterParams)
        {
            CastleRegistrations.Regiter(container, enterParams);
            var gameplayViewModelsContainer = new DIContainer(container);
            CastleViewModelsRegistrations.Register(gameplayViewModelsContainer);

            _worldBinder.Bind(gameplayViewModelsContainer.Resolve<WorldCastleRootViewModel>());

            gameplayViewModelsContainer.Resolve<UICastleRootViewModel>();

            var buildingsService = container.Resolve<BuildingsService>();
            buildingsService.PlaceBuilding("dummy", GetRandomPosition());
            buildingsService.PlaceBuilding("dummy", GetRandomPosition());
            buildingsService.PlaceBuilding("dummy", GetRandomPosition());

            var uiRoot = container.Resolve<UIViewRoot>();
            var uiScene = Instantiate(_sceneUIRootPrefap);
            uiRoot.AttachSceneUI(uiScene.gameObject);

            var exitSceneSignalsubj = new Subject<Unit>();
            uiScene.Bind(exitSceneSignalsubj);

            Debug.Log(enterParams.SaveFileName + " " + enterParams.LevelNumber);

            var mainMenuenterParams = new MenuEnterParams("fatality");
            var exitParams = new CastleExitParams(mainMenuenterParams);
            var exitTiMainMenuSceneSignal = exitSceneSignalsubj.Select(_ => exitParams);

            return exitTiMainMenuSceneSignal;
        }

        private Vector2Int GetRandomPosition()
        {
            var x = Random.Range(-10, 10);
            var y = Random.Range(-10, 10);
            var Position = new Vector2Int(x, y);

            return Position;
        }
    }
}
