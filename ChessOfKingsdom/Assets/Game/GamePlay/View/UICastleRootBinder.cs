using R3;
using UnityEngine;

public class UICastleRootBinder : MonoBehaviour
{
    private Subject<Unit> _exitSceneSignalSubj;

    public void HandleGoToGameplayButtonClick()
    {
        _exitSceneSignalSubj?.OnNext(Unit.Default);
    }

    public void Bind(Subject<Unit> exitSceneSignalSubj)
    {
        _exitSceneSignalSubj = exitSceneSignalSubj;
    }
}
