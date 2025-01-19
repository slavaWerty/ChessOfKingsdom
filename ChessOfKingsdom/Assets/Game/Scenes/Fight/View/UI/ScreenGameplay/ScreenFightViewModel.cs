using Fight.Services;
using Fight.View.UI;
using mBuilding.Scripts.MVVM.UI;
using R3;
using UnityEngine;

public class ScreenFightViewModel : WindowViewModel
{
    private FightUIManager _uIManager;
    private Subject<Unit> _exitToCastleSceneRequestSubj;

    public override string Id =>  "ScreenFight";
    public MilitaryService MilitaryService;

    public ScreenFightViewModel(FightUIManager uIManager, 
        Subject<Unit> exitToCastleSceneRequestSubj,
        MilitaryService militaryService)
    {
        _uIManager = uIManager;
        _exitToCastleSceneRequestSubj = exitToCastleSceneRequestSubj;
        MilitaryService = militaryService;
    }

    public void RequestGoToMenu()
    {
        _exitToCastleSceneRequestSubj.OnNext(Unit.Default);
    }

    public void RequestStartFight()
    {
        Debug.Log("Не забудь добавить");
    }
}
