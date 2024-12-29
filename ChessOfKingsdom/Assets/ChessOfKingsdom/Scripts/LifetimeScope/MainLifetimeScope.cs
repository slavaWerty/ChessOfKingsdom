using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainLifetimeScope : LifetimeScope
{
    [SerializeField] private MonoBehaviour _mono;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_mono);

        builder.RegisterEntryPoint<MainEntryPoint>();
    }
}
