using VContainer;
using VContainer.Unity;

public class MenuEntryPoint : IInitializable
{
    private ButtonChangeScene _buttonChangeScene;

    [Inject]
    public MenuEntryPoint(ButtonChangeScene buttonSceneChanger)
    {
        _buttonChangeScene = buttonSceneChanger;
    }

    public void Initialize()
    {
        _buttonChangeScene.SetSceneByIndex();
    }
}

