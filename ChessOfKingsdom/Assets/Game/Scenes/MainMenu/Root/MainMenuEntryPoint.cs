using Assets.Game.Scenes.MainMenu.View;
using BaCon;
using GamePlay;
using GameRoot;
using MainMenu.View;
using mBuilding.Scripts.Game.Common;
using R3;
using UnityEngine;

namespace MainMenu
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefap;

        public Observable<MainMenuExitParams> Run(DIContainer container, MenuEnterParams enterParams)
        {
            MainMenuRegistations.Register(container, enterParams);
            var mainMenuViewModelsContainer = new DIContainer(container);
            MainMenuViewModelsRegistrations.Register(mainMenuViewModelsContainer);

            InitUI(mainMenuViewModelsContainer);

            Debug.Log(enterParams?.Result);

            var gameplayEnterParams = new CastleEnterParams();
            var mainMenuExitParams = new MainMenuExitParams(gameplayEnterParams);
            var exitSceneSignalSubj = container.Resolve<Subject<Unit>>(AppConstants.EXIT_IN_CASTLE_SCENE_REQUEST_TAG);
            var exitToGameplayScenesSignal = exitSceneSignalSubj.Select(_ => mainMenuExitParams);

            return exitToGameplayScenesSignal;
        }

        private void InitUI(DIContainer container)
        {
            var uiRoot = container.Resolve<UIViewRoot>();
            var sceneRootBinder = Instantiate(_sceneUIRootPrefap);
            uiRoot.AttachSceneUI(sceneRootBinder.gameObject);

            sceneRootBinder.Bind(container.Resolve<UIMainMenuRootViewModel>());

            var uiManager = container.Resolve<MenuUIManager>();
            uiManager.OpenScreenGameplay();
        }
    }
}
