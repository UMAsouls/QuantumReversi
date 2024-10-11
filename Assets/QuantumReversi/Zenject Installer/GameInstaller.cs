using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject headMass;

    public override void InstallBindings()
    {
        Container
            .BindInterfacesTo<Stone>()
            .AsTransient();

        Container
            .Bind<HeadMass>()
            .To<Mass>()
            .FromComponentOn(headMass)
            .AsTransient();
    }
}