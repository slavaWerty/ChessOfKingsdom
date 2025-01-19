using BaCon;
using Fight.View;
using Fight.View.UI;
using GamePlay;
using GameRoot;
using mBuilding.Scripts.Game.Common;
using R3;
using UnityEngine;

public class FightEntryPoint : MonoBehaviour
{
    [SerializeField] private UIFightRootBinder _sceneUIRootPrefap;
    [SerializeField] private WorldFightRootBinder _worldBinder;

    public Observable<FightExitParams> Run(DIContainer container, FightEnterParams enterParams)
    {
        FightRegistrations.Regiter(container, enterParams);
        var gameplayViewModelsContainer = new DIContainer(container);
        FightViewModelsRegistrations.Register(gameplayViewModelsContainer);

        InitWorld(gameplayViewModelsContainer);
        InitUI(gameplayViewModelsContainer);

        var mainMenuEnterParams = new CastleEnterParams();
        var exitParams = new FightExitParams(mainMenuEnterParams);
        var exitSceneSignalsubj = container.Resolve<Subject<Unit>>(AppConstants.EXIT_IN_CASTLE_TO_FIGHT_SCENE_REQUEST_TAG);
        var exitToMainMenuSceneSignal = exitSceneSignalsubj.Select(_ => exitParams);

        return exitToMainMenuSceneSignal;
    }

    private void InitWorld(DIContainer container)
    {
        _worldBinder.Bind(container.Resolve<WorldFightRootViewModel>());
    }

    private void InitUI(DIContainer container)
    {
        var uiRoot = container.Resolve<UIViewRoot>();
        var sceneRootBinder = Instantiate(_sceneUIRootPrefap);
        uiRoot.AttachSceneUI(sceneRootBinder.gameObject);

        sceneRootBinder.Bind(container.Resolve<UIFightRootViewModel>());

        var uiManager = container.Resolve<FightUIManager>();
        uiManager.OpenScreenFight();
        uiManager.OpenLevelsPopup();
    }
}
