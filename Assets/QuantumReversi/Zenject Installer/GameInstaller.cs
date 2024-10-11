using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject headMass;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject cp;

    [SerializeField]
    private GameObject board;

    [SerializeField]
    private GameObject positioner;

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

        Container
            .Bind<IPosJudge>()
            .To<PosJudge>()
            .AsSingle();

        Container
            .Bind<IPlayer>()
            .To<Player>()
            .FromComponentOn(player)
            .AsTransient();

        Container
            .Bind<ICP>()
            .To<CP>()
            .FromComponentOn(cp)
            .AsTransient();

        Container
            .BindInterfacesTo<Board>()
            .FromComponentOn(board)
            .AsSingle();

        Container
            .Bind<IStonePositioner>()
            .To<StonePositioner>()
            .FromComponentOn(positioner)
            .AsTransient();

        Container
            .Bind<IAI>()
            .To<AI>()
            .AsSingle();
    }
}