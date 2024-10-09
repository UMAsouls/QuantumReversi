using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, IPlayer
{
    [Inject]
    IStonePositioner positioner;

    private int stone90;
    private int stone70;

    public async UniTask PlayerTurn()
    {
        if (await positioner.PutStone() == StoneType.NINETY) stone90++;
        else stone70++;
    }

    // Use this for initialization
    void Start()
    {
        stone90 = 9;
        stone70 = 9;
    }

    // Update is called once per frame
    void Update()
    {

    }
}