using mBuilding.Scripts.MVVM.UI;
using R3;
using States.GameResources;

public class ScreenCastleViewModel : WindowViewModel
{
    private readonly CastleUIManager _uiManager;
    private readonly Subject<Unit> _exitMenuSceneRequest;
    private readonly Subject<Unit> _exitFightSceneRequest;
    private readonly ResourcesService _resourcesService;

    public override string Id => "ScreenCastle";

    public ScreenCastleViewModel(CastleUIManager uiManager, 
        Subject<Unit> exitMenuSceneRequest,
        Subject<Unit> exitFightSceneRequest,
        ResourcesService resourcesService)
    {
        _resourcesService = resourcesService;
        _uiManager = uiManager;
        _exitMenuSceneRequest = exitMenuSceneRequest;
        _exitFightSceneRequest = exitFightSceneRequest;
    }

    public void RequestOpenSpawnPopup()
    {
        _uiManager.OpenSpawnPopup();
    }

    public Observable<int> RequestChangeMoneyText()
    {
        return _resourcesService.ObserveResource(ResourceType.SoftCurrency);
    }

    public Observable<int> RequestChangeQueen()
    {
        return _resourcesService.ObserveResource(ResourceType.Queen);
    }

    public void RequestGoToMainMenu()
    {
        _exitMenuSceneRequest.OnNext(Unit.Default);
    }

    public void RequestGoToFight()
    {
        _exitFightSceneRequest.OnNext(Unit.Default);
    }
}
