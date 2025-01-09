using BaCon;
using GamePlay.Services;
using GamePlay.View;
using GamePlay.View.UI.SpawnPopup;
using mBuilding.Scripts.Game.Common;
using mBuilding.Scripts.MVVM.UI;
using R3;
using StateRoot;
using States;
using UnityEngine;

public class CastleUIManager : UIManager
{
    private readonly Subject<Unit> _exitMenuSceneRequest;
    private readonly Subject<Unit> _exitFightSceneRequest;
    private readonly ResourcesService _resourcesService;
    private readonly BuildingsService _buildingsService;
    private readonly ICommandProcessor _cmd;
    private readonly WorldCastleRootBinder _rootBinder;
    private readonly GameStateProxy _gameState;

    public CastleUIManager(DIContainer container, 
        WorldCastleRootBinder rootBinder) : base(container)
    {
        _exitMenuSceneRequest = container.Resolve<Subject<Unit>>(AppConstants.EXIT_IN_MENU_SCENE_REQUEST_TAG);
        _exitFightSceneRequest = container.Resolve<Subject<Unit>>(AppConstants.EXIT_IN_FIGHT_SCENE_REQUEST_TAG);
        _resourcesService = container.Resolve<ResourcesService>();
        _buildingsService = container.Resolve<BuildingsService>();
        _cmd = container.Resolve<ICommandProcessor>();
        _rootBinder = rootBinder;
        _gameState = container.Resolve<IGameStateProvider>().GameState;
    }

    public ScreenCastleViewModel OpenScreenGameplay()
    {
        if (_resourcesService == null)
            Debug.Log("ResourcesService Null");

        var viewModel = new ScreenCastleViewModel(this, _exitMenuSceneRequest, _exitFightSceneRequest, _resourcesService);
        var rootUI = Container.Resolve<UICastleRootViewModel>();

        rootUI.OpenScreen(viewModel);

        return viewModel;
    }

    public SpawnPopupViewModel OpenSpawnPopup()
    {
        var a = new SpawnPopupViewModel(_buildingsService,
            _rootBinder,
            _cmd,
            _gameState,
            _resourcesService
            );

        var rootUI = Container.Resolve<UICastleRootViewModel>();

        rootUI.OpenPopup(a);

        return a;
    }
}
