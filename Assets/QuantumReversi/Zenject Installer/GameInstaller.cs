using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .BindInterfacesTo<Stone>()
            .AsTransient();

    }
}