using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

public class MenuLifetimeScope : LifetimeScope
{
    [SerializeField] private Button _playButton;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_playButton);

        builder.Register<ButtonChangeScene>(Lifetime.Singleton).WithParameter(2);

        builder.RegisterEntryPoint<MenuEntryPoint>();
    }
}
