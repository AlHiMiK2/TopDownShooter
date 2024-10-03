using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindPlayerInput();
    }

    private void BindPlayerInput()
    {
        PlayerInput input = Container
            .InstantiateComponentOnNewGameObject<PlayerInput>();

        Container
            .Bind<PlayerInput>()
            .FromInstance(input)
            .AsSingle();
    }
}
