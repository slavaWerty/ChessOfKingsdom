using BaCon;
using GamePlay;
using GameRoot;
using R3;
using UnityEngine;

public class FightEntryPoint : MonoBehaviour
{
    [SerializeField] private UIFightRootBinder _sceneUIRootPrefap;

    public Observable<FightExitParams> Run(DIContainer container, FightEnterParams enterParams)
    {
        FightRegistrations.Regiter(container, enterParams);
        var gameplayViewModelsContainer = new DIContainer(container);
        FightViewModelsRegistrations.Register(gameplayViewModelsContainer);

        // gameplayViewModelsContainer.Resolve<UICastleRootViewModel>();
        // gameplayViewModelsContainer.Resolve<WorldCastleRootViewModel>();

        var uiRoot = container.Resolve<UIViewRoot>();
        var uiScene = Instantiate(_sceneUIRootPrefap);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        var exitSceneSignalsubj = new Subject<Unit>();
        uiScene.Bind(exitSceneSignalsubj);

        Debug.Log(enterParams.TestNumber);

        var mainMenuenterParams = new CastleEnterParams();
        var exitParams = new FightExitParams(mainMenuenterParams);
        var exitTiMainMenuSceneSignal = exitSceneSignalsubj.Select(_ => exitParams);

        return exitTiMainMenuSceneSignal;
    }
}
