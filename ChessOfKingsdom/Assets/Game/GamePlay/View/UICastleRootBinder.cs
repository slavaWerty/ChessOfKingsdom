using GamePlay.Services;
using R3;
using UnityEngine;

public class UICastleRootBinder : MonoBehaviour
{
    private Subject<Unit> _transitionToMenuSceneSignalSubj;
    private Subject<Unit> _transitionToFightSceneSignalSubj;
    private Subject<Unit> _placingBuilding;

    public void HandlePlacingBuildingButtonClick()
    {
        _placingBuilding?.OnNext(Unit.Default);
    }

    public void HandleGoToMenuSceneButtonClick()
    {
        _transitionToMenuSceneSignalSubj?.OnNext(Unit.Default);
    }

    public void HandleGoToFightSceneButtonClick()
    {
        _transitionToFightSceneSignalSubj?.OnNext(Unit.Default);
    }

    public void Bind(Subject<Unit> transitionToMenuSceneSignalSubj, Subject<Unit> transitionToFightSceneSignalSubj)
    {
        _transitionToMenuSceneSignalSubj = transitionToMenuSceneSignalSubj;
        _transitionToFightSceneSignalSubj = transitionToFightSceneSignalSubj;
    }
}
